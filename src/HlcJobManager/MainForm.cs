﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using HlcJobCommon;
using HlcJobCommon.Wcf;
using HlcJobManager.Wcf;
using HLC.Common.Utils;
using NLog;

namespace HlcJobManager
{
    public partial class MainForm : Form
    {
        private const int LOG_SIZE = 300;

        private readonly JobManagerProxy _jobManagerProxy;
        private readonly Dictionary<string, Queue<string>> _logDict = new Dictionary<string, Queue<string>>();
        private string _title;
        private ILogger _logger;
        private bool _disableJobEdit = false;

        /// <summary>
        /// 当前选中的任务
        /// </summary>
        public ManageJob SelectedJob
        {
            get
            {
                if (dgv_data.SelectedRows.Count <= 0)
                {
                    return null;
                }

                var row = dgv_data.SelectedRows[0];

                if (!(row.Tag is ManageJob job))
                {
                    return null;
                }

                return job;
            }
        }

        /// <summary>
        /// HlcJob 宿主服务
        /// </summary>
        public ServiceController JobService
        {
            get { return ServiceController.GetServices().FirstOrDefault(s => s.ServiceName.Equals(Constant.ServiceName)); }
        }

        public MainForm()
        {
            InitializeComponent();
            _title = Text;
            lb_version.Text = ProductVersion;

            _jobManagerProxy = new JobManagerProxy();
            _logger = NLog.LogManager.GetCurrentClassLogger();
            
            TimerUtil.StartTimer("status_monitor", 5000, RefreshServerStatus);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btn_installSvc.Enabled = JobService == null;
            btn_startSvc.Enabled = JobService != null && JobService.Status == ServiceControllerStatus.Stopped;
            btn_stopSvc.Enabled = JobService != null && JobService.Status == ServiceControllerStatus.Running;
            btn_uninstallSvc.Enabled = !btn_installSvc.Enabled;
            gbx_jobTool.Enabled = JobService != null && JobService.Status == ServiceControllerStatus.Running;

            try
            {
                RefreshServerStatus();
                if (JobService != null && JobService.Status == ServiceControllerStatus.Running)
                {
                    RefreshJobView();
                }
            }
            catch (EndpointNotFoundException ex)
            {
                _logger.Error(ex, "未找到服务");
                MessageBox.Show("未找到服务");
            }

            JobManagerCallback.WriteLogHandler += AddLog;
            JobManagerCallback.UpdateClientJobHander += UpdateJob;
        }

        #region Server Button Event
        
        private void btn_installSvc_Click(object sender, EventArgs e)
        {
            if (JobService != null)
            {
                MessageBox.Show("服务已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AsyncUtil.Run(() =>
            {
                RefreshStatusMsgByServerStatus(ServerStatus.Installing);
                RefreshServerControlBtn(ServerStatus.Installing);
                DynamicUtil.InvokeCmd("HlcJobService install");
            }, RefreshServerStatus, exception =>
            {
                _logger.Error(exception, "安装服务出错");
            });
        }

        private void btn_startSvc_Click(object sender, EventArgs e)
        {
            if (JobService == null)
            {
                MessageBox.Show("请先安装服务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.Running)
            {
                MessageBox.Show("服务正在运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AsyncUtil.Run(() =>
            {
                RefreshStatusMsgByServerStatus(ServerStatus.StartPending);
                RefreshServerControlBtn(ServerStatus.StartPending);
                DynamicUtil.InvokeCmd("HlcJobService start");
            }, () =>
            {
                RefreshServerStatus();
                RefreshJobView();
            }, exception =>
            {
                _logger.Error(exception, "启动服务出错");
            });
        }

        private void btn_stopSvc_Click(object sender, EventArgs e)
        {
            if (JobService == null)
            {
                MessageBox.Show("未发现服务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (JobService.Status == ServiceControllerStatus.Stopped)
            {
                MessageBox.Show("服务已停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("确定要停止宿主服务（将会关闭所有任务）？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            AsyncUtil.Run(() =>
            {
                RefreshStatusMsgByServerStatus(ServerStatus.StopPending);
                RefreshServerControlBtn(ServerStatus.StopPending);
                DynamicUtil.InvokeCmd("HlcJobService stop");
            }, RefreshServerStatus, exception =>
            {
                _logger.Error(exception, "停止服务出错");
            });
        }

        private void btn_uninstallSvc_Click(object sender, EventArgs e)
        {
            if (JobService == null)
            {
                MessageBox.Show("服务不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (MessageBox.Show("确定要卸载宿主服务（将会关闭所有任务）？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            AsyncUtil.Run(() =>
            {
                RefreshStatusMsgByServerStatus(ServerStatus.Uninstalling);
                RefreshServerControlBtn(ServerStatus.Uninstalling);
                DynamicUtil.InvokeCmd("HlcJobService uninstall");
            }, RefreshServerStatus, exception =>
            {
                _logger.Error(exception, "卸载服务出错");
            });
        }


        #endregion Server Button Event

        #region Job Button Event

        private void btn_addJob_Click(object sender, EventArgs e)
        {
            var jobEditForm = new JobEditForm();
            jobEditForm.ShowDialog();
            RefreshJobView(jobEditForm.JobId);
        }

        private void btn_editJob_Click(object sender, EventArgs e)
        {
            if (dgv_data.SelectedRows.Count <= 0)
            {
                return;
            }

            var selectedRow = dgv_data.SelectedRows[0];

            EditJobRow(selectedRow);
        }

        private void btn_refreshJobs_Click(object sender, EventArgs e)
        {
            if (JobService == null)
            {
                MessageBox.Show("请先安装服务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (JobService.Status != ServiceControllerStatus.Running)
            {
                MessageBox.Show("请先启动服务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            RefreshJobView(SelectedJob?.Id);
        }

        private void btn_jobEnable_Click(object sender, EventArgs e)
        {
            SwitchJobEnable(SelectedJob);
        }

        #endregion Job Button Event

        #region Log Button Event

        private void btn_viewLog_Click(object sender, EventArgs e)
        {
            spl_main.Panel2Collapsed = !spl_main.Panel2Collapsed;

            if (spl_main.Panel2Collapsed)
            {
                btn_viewLog.Text = "查看日志";
            }
            else
            {
                btn_viewLog.Text = "隐藏日志";
            }
        }

        private void btn_clearLog_Click(object sender, EventArgs e)
        {
            txt_log.Text = "";
            var selectedJob = SelectedJob;
            if (selectedJob == null || !_logDict.ContainsKey(selectedJob.Id))
            {
                return;
            }

            _logDict[selectedJob.Id].Clear();
        }

        #endregion Log Button Event

        #region DataGridView Event
        private void dgv_data_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_disableJobEdit)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                var cellLocation = dgv_data.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;

                var rowIndex = e.RowIndex;
                var row = dgv_data.Rows[rowIndex];
                var job = row.Tag as ManageJob;
                if (job != null)
                {
                    var menu = new ContextMenu();

                    menu.MenuItems.Add(job.Enable ? "禁用" : "启用", (o, args) =>
                    {
                        SwitchJobEnable(job);
                    });
                    menu.MenuItems.Add("删除", (o, args) =>
                    {
                        delJob();
                    });

                    menu.Show(dgv_data, new Point(cellLocation.X + e.X, cellLocation.Y + e.Y));
                }

            }
        }

        private void dgv_data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_disableJobEdit)
            {
                return;
            }

            var rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }

            var row = dgv_data.Rows[rowIndex];

            EditJobRow(row);
        }

        private void dgv_data_SelectionChanged(object sender, EventArgs e)
        {
            var selectedJob = SelectedJob;
            ShowLog(selectedJob);
        }

        private bool swapLock = false;

        private void dgv_data_KeyDown(object sender, KeyEventArgs e)
        {
            if (!swapLock)
            {
                swapLock = true;

                var selectRowIndex = dgv_data.SelectedRows[0].Index;

                if (e.Control && e.KeyCode == Keys.Up && selectRowIndex > 0)
                {
                    var result = _jobManagerProxy.SwapJobRank(dgv_data.Rows[selectRowIndex].Cells["cln_id"].Value.ToString(),
                        dgv_data.Rows[selectRowIndex - 1].Cells["cln_id"].Value.ToString());
                    if (result)
                    {
                        RefreshJobView(dgv_data.Rows[selectRowIndex].Cells["cln_id"].Value.ToString());
                    }
                    e.Handled = true;
                }

                if (e.Control && e.KeyCode == Keys.Down && selectRowIndex < dgv_data.RowCount - 1)
                {
                    var result = _jobManagerProxy.SwapJobRank(dgv_data.Rows[selectRowIndex].Cells["cln_id"].Value.ToString(),
                        dgv_data.Rows[selectRowIndex + 1].Cells["cln_id"].Value.ToString());
                    if (result)
                    {
                        RefreshJobView(dgv_data.Rows[selectRowIndex].Cells["cln_id"].Value.ToString());
                    }
                    e.Handled = true;
                }

                swapLock = false;
            }

            
        }


        private void delJob()
        {
            if (dgv_data.SelectedRows.Count <= 0)
            {
                return;
            }

            var selectedJob = SelectedJob;

            var result = MessageBox.Show($"确定要删除任务【{selectedJob.Name}】吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            AsyncUtil.Run(() => _jobManagerProxy.RemoveJob(selectedJob.Id), removeResult =>
            {
                RefreshJobView();
            }, exception =>
            {
                _logger.Error(exception, "删除任务出错");
                RefreshJobView();
            });
        }

        #endregion DataGridView Event

        #region DataGridView

        private void RefreshJobView(string selectedId = null)
        {
            lock (this)
            {
                AsyncUtil.Run(() => _jobManagerProxy.GetAllJobs(), jobs =>
                {
                    RefreshJobView(jobs, selectedId);
                }, exception =>
                {
                    _logger.Error(exception, "刷新任务出错");
                });
            }
        }

        private void RefreshJobView(List<ManageJob> jobs, string selectedId = null)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => RefreshJobView(jobs, selectedId)));
                return;
            }

            dgv_data.Rows.Clear();

            DataGridViewRow selectedRow = null;

            foreach (var job in jobs.OrderBy(j=>j.Rank))
            {
                var rowIndex = dgv_data.Rows.Add();
                var row = dgv_data.Rows[rowIndex];
                UpdateJobRow(row, job);

                if (job.Id.Equals(selectedId))
                {
                    selectedRow = row;
                }

                row.Selected = false;
            }

            if (selectedRow != null)
            {
                selectedRow.Selected = true;
            }
        }
        
        private void EditJobRow(DataGridViewRow row)
        {
            if (!(row.Tag is ManageJob job))
            {
                return;
            }

            var jobEditForm = new JobEditForm(job);
            jobEditForm.ShowDialog();
            RefreshJobView(job.Id);
        }

        private void UpdateJob(ManageJob job)
        {
            foreach (DataGridViewRow row in dgv_data.Rows)
            {
                if (row.Cells["cln_id"].Value.Equals(job.Id))
                {
                    UpdateJobRow(row, job);
                    return;
                }
            }
            var rowIndex = dgv_data.Rows.Add();
            var newRow = dgv_data.Rows[rowIndex];
            UpdateJobRow(newRow, job);
        }

        private void UpdateJobRow(DataGridViewRow row, ManageJob job)
        {
            row.Cells["cln_id"].Value = job.Id;
            row.Cells["cln_name"].Value = job.Name;
            row.Cells["cln_type"].Value = job.Type;
            row.Cells["cln_cron"].Value = job.Cron;
            row.Cells["cln_preTime"].Value = job.PreviousFireTime?.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["cln_nextTime"].Value = job.NextFireTime?.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["cln_enable"].Value = JobEnable2Str(job.Enable);
            row.Cells["cln_state"].Value = JobState2Str(job.State);
            row.Tag = job;
        }

        private string JobEnable2Str(bool jobEnable)
        {
            return jobEnable ? "启用" : "禁用";
        }

        private string JobState2Str(JobState jobState)
        {
            switch (jobState)
            {
                case JobState.Normal:
                    return "正常";
                case JobState.Paused:
                    return "暂停";
                case JobState.Complete:
                    return "完成";
                case JobState.Error:
                    return "出错";
                case JobState.Blocked:
                    return "锁定";
                case JobState.None:
                    return "无";
                default:
                    return "无";
            }
        }

        #endregion DataGridView

        #region Log Status

        private void AddLog(string jobId, string message)
        {
            if (!_logDict.ContainsKey(jobId))
            {
                _logDict[jobId] = new Queue<string>(LOG_SIZE);
            }

            if (_logDict[jobId].Count >= LOG_SIZE)
            {
                _logDict[jobId].Dequeue();
            }

            _logDict[jobId].Enqueue(message);

            var selectedJob = SelectedJob;
            if (selectedJob != null && selectedJob.Id.Equals(jobId))
            {
                ShowLog(selectedJob);
            }
        }

        private void ShowLog(ManageJob job)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => ShowLog(job)));
                return;
            }

            if (job == null)
            {
                txt_log.Text = "";
                return;
            }

            var jobId = job.Id;

            if (!_logDict.ContainsKey(jobId))
            {
                AsyncUtil.Run(() =>
                {
                    var chacheLog = _jobManagerProxy.GetChacheLog(jobId);
                    _logDict[jobId] = new Queue<string>();
                    foreach (var log in chacheLog)
                    {
                        _logDict[jobId].Enqueue(log);
                    }
                }, () => ShowLog(job), exception =>
                {
                    _logger.Error(exception, "获取缓存日志出错");
                });
                return;
            }

            var logs = _logDict[jobId];
            if (logs == null)
            {
                txt_log.Text = "";
                return;
            }

            StringBuilder sb = new StringBuilder();

            foreach (var log in logs)
            {
                sb.AppendLine(log);
            }
            txt_log.Text = sb.ToString();
            txt_log.Select(txt_log.Text.Length, 0);
            txt_log.ScrollToCaret();
        }
        
        private void RefreshServerStatus()
        {
            if (JobService == null)
            {
                RefreshStatusMsgByServerStatus(ServerStatus.Null);
                RefreshServerControlBtn(ServerStatus.Null);

                ClearDgv();
                return;
            }

            if (JobService.Status == ServiceControllerStatus.StartPending)
            {
                RefreshStatusMsgByServerStatus(ServerStatus.StartPending);
                RefreshServerControlBtn(ServerStatus.StartPending);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.Running)
            {
                RefreshStatusMsgByServerStatus(ServerStatus.Running);
                RefreshServerControlBtn(ServerStatus.Running);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.StopPending)
            {
                RefreshStatusMsgByServerStatus(ServerStatus.StopPending);
                RefreshServerControlBtn(ServerStatus.StopPending);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.Stopped)
            {
                RefreshStatusMsgByServerStatus(ServerStatus.Stoped);
                RefreshServerControlBtn(ServerStatus.Stoped);

                ClearDgv();
                return;
            }

            if (JobService.Status == ServiceControllerStatus.ContinuePending)
            {
                RefreshStatusMsgByServerStatus(ServerStatus.ContinuePending);
                RefreshServerControlBtn(ServerStatus.ContinuePending);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.Paused)
            {
                RefreshStatusMsgByServerStatus(ServerStatus.Stoped);
                RefreshServerControlBtn(ServerStatus.Stoped);

                ClearDgv();
                return;
            }

            if (JobService.Status == ServiceControllerStatus.PausePending)
            {
                RefreshStatusMsgByServerStatus(ServerStatus.PausePending);
                RefreshServerControlBtn(ServerStatus.PausePending);

                ClearDgv();
                return;
            }
        }

        private void RefreshServerControlBtn(ServerStatus status)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => RefreshServerControlBtn(status)));
                return;
            }

            switch (status)
            {
                case ServerStatus.Null:
                    btn_installSvc.Enabled = true;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = false;
                    gbx_jobTool.Enabled = false;
                    _disableJobEdit = true;
                    break;
                case ServerStatus.StartPending:
                case ServerStatus.StopPending:
                case ServerStatus.PausePending:
                case ServerStatus.ContinuePending:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = false;
                    gbx_jobTool.Enabled = false;
                    _disableJobEdit = true;
                    break;
                case ServerStatus.Running:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = true;
                    btn_uninstallSvc.Enabled = true;
                    gbx_jobTool.Enabled = true;
                    _disableJobEdit = false;
                    break;
                case ServerStatus.Stoped:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = true;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = true;
                    gbx_jobTool.Enabled = false;
                    _disableJobEdit = true;
                    break;
                case ServerStatus.Error:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = false;
                    gbx_jobTool.Enabled = false;
                    _disableJobEdit = true;
                    break;
                default:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = false;
                    gbx_jobTool.Enabled = false;
                    _disableJobEdit = true;
                    break;
            }
        }

        private void RefreshStatusMsgByServerStatus(ServerStatus status)
        {
            switch (status)
            {
                case ServerStatus.Null:
                    RefreshStatusMsg("未发现服务");
                    break;
                case ServerStatus.Installing:
                    RefreshStatusMsg("服务安装中");
                    break;
                case ServerStatus.Uninstalling:
                    RefreshStatusMsg("服务卸载中");
                    break;
                case ServerStatus.StartPending:
                    RefreshStatusMsg("服务正在启动");
                    break;
                case ServerStatus.StopPending:
                    RefreshStatusMsg("服务正在停止");
                    break;
                case ServerStatus.PausePending:
                    RefreshStatusMsg("服务正在暂停");
                    break;
                case ServerStatus.ContinuePending:
                    RefreshStatusMsg("服务被挂起");
                    break;
                case ServerStatus.Running:
                    RefreshStatusMsg("服务正在运行");
                    break;
                case ServerStatus.Stoped:
                    RefreshStatusMsg("服务未运行");
                    break;
                case ServerStatus.Error:
                    RefreshStatusMsg("未知错误");
                    break;
                default:
                    RefreshStatusMsg("未知状态");
                    break;
            }

        }

        private void RefreshStatusMsg(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => RefreshStatusMsg(msg)));
                return;
            }
            Text = $"{_title}（{msg}）";
        }

        private void ClearDgv()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(ClearDgv));
                return;
            }

            dgv_data.Rows.Clear();
        }

        private void SwitchJobEnable(ManageJob job)
        {
            if (job == null)
            {
                return;
            }

            AsyncUtil.Run(() => job.Enable ? _jobManagerProxy.DisableJob(job.Id) : _jobManagerProxy.EnableJob(job.Id),
                result => { }, exception =>
                {
                    _logger.Error(exception, "切换任务状态出错");
                });
        }
        #endregion

     }

    enum ServerStatus
    {
        Null,
        Installing,
        Uninstalling,
        StartPending,
        Running,
        StopPending,
        Stoped,
        Error,
        ContinuePending,
        PausePending
    }
}

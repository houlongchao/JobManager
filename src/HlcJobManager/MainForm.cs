using System;
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

namespace HlcJobManager
{
    public partial class MainForm : Form
    {
        private readonly JobManagerProxy _jobManagerProxy;
        private readonly Dictionary<string, Queue<string>> _logDict = new Dictionary<string, Queue<string>>();
        private string _title;

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
            _jobManagerProxy = new JobManagerProxy();

            dgv_data.SelectionChanged += (sender, args) =>
            {
                var selectedJob = SelectedJob;
                if (selectedJob == null)
                {
                    txt_log.Text = "";
                    return;
                }
                ShowLog(selectedJob.Id);
            };

            TimerUtil.StartTimer("status_monitor", 5000, refreshServerStatus);
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
                refreshServerStatus();
                if (JobService != null && JobService.Status == ServiceControllerStatus.Running)
                {
                    RefreshJobView();
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("未找到服务");
            }

            JobManagerCallback.WriteLogHandler += AddLog;
            JobManagerCallback.UpdateClientJobHander += UpdateJob;
        }

        private void btn_addJob_Click(object sender, EventArgs e)
        {
            var jobEditForm = new JobEditForm();
            var dialogResult = jobEditForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                RefreshJobView(jobEditForm.JobName);
            }
        }

        private void RefreshJobView(string selectedId = null)
        {
            lock (this)
            {
                var allJobs = _jobManagerProxy.GetAllJobs();

                dgv_data.Rows.Clear();

                DataGridViewRow selectedRow = null;

                foreach (var job in allJobs)
                {
                    var rowIndex = dgv_data.Rows.Add();
                    var row = dgv_data.Rows[rowIndex];
                    UpdateJobRow(row, job);

                    if (job.Name.Equals(selectedId))
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
            

        }

        private object JobEnable2Str(bool jobEnable)
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

            var selectedJob = SelectedJob;

            if (selectedJob == null)
            {
                return;
            }

            ShowLog(selectedJob.Id);
        }

        private void dgv_data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            if (rowIndex < 0)
            {
                return;
            }

            var row = dgv_data.Rows[rowIndex];

            EditJobRow(row);
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

        private void EditJobRow(DataGridViewRow row)
        {
            if (!(row.Tag is ManageJob job))
            {
                return;
            }

            var jobEditForm = new JobEditForm(job);

            if (jobEditForm.ShowDialog() == DialogResult.OK)
            {
                RefreshJobView(jobEditForm.JobName);
            }
        }

        private void btn_delJob_Click(object sender, EventArgs e)
        {
            if (dgv_data.SelectedRows.Count <= 0)
            {
                return;
            }

            var selectedJob = SelectedJob;

            var result = MessageBox.Show($"确定要删除任务【{selectedJob.Name}】吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes )
            {
                return;
            }
            
            var removeResult = _jobManagerProxy.RemoveJob(selectedJob.Id);

            if (removeResult)
            {
                RefreshJobView();
            }
        }

        private void gbx_jobTool_Enter(object sender, EventArgs e)
        {

        }

        private void btn_refreshJobs_Click(object sender, EventArgs e)
        {
            //if (JobService == null)
            //{
            //    MessageBox.Show("请先安装服务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            //if (JobService.Status != ServiceControllerStatus.Running)
            //{
            //    MessageBox.Show("请先启动服务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            try
            {
                RefreshJobView();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("刷新出错", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btn_installSvc_Click(object sender, EventArgs e)
        {
            if (JobService != null)
            {
                MessageBox.Show("服务已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AsyncUtil.Run(() =>
            {
                refreshStatusMsgByServerStatus(ServerStatus.Installing);
                DynamicUtil.InvokeCmd("HlcJobService install");
            }, refreshServerStatus);
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
                refreshStatusMsgByServerStatus(ServerStatus.StartPending);
                DynamicUtil.InvokeCmd("HlcJobService start");
            }, () =>
            {
                refreshServerStatus();
                RefreshJobView();
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

            AsyncUtil.Run(() =>
            {
                refreshStatusMsgByServerStatus(ServerStatus.StopPending);
                DynamicUtil.InvokeCmd("HlcJobService stop");
            }, refreshServerStatus);
        }

        private void btn_uninstallSvc_Click(object sender, EventArgs e)
        {
            if (JobService == null)
            {
                MessageBox.Show("服务不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            AsyncUtil.Run(() =>
            {
                refreshStatusMsgByServerStatus(ServerStatus.Uninstalling);
                DynamicUtil.InvokeCmd("HlcJobService uninstall");
            }, refreshServerStatus);
        }

        public void AddLog(string jobId, string message)
        {
            int maxLogSize = 100;

            if (!_logDict.ContainsKey(jobId))
            {
                _logDict[jobId] = new Queue<string>(maxLogSize);
            }

            if (_logDict[jobId].Count >= maxLogSize)
            {
                _logDict[jobId].Dequeue();
            }

            _logDict[jobId].Enqueue(message);

            var selectedJob = SelectedJob;
            if (selectedJob != null && selectedJob.Id.Equals(jobId))
            {
                ShowLog(jobId);
            }
        }

        public void ShowLog(string jobId)
        {
            Invoke(new MethodInvoker(() =>
            {
                if (!_logDict.ContainsKey(jobId))
                {
                    var chacheLog = _jobManagerProxy.GetChacheLog(jobId);
                    _logDict[jobId] = new Queue<string>();
                    foreach (var log in chacheLog)
                    {
                        _logDict[jobId].Enqueue(log);
                    }
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
            }));
        }

        public void UpdateJob(ManageJob job)
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

        public void UpdateJobRow(DataGridViewRow row, ManageJob job)
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

        private void refreshServerStatus()
        {
            if (JobService == null)
            {
                refreshStatusMsgByServerStatus(ServerStatus.Null);
                refreshServerControlBtn(ServerStatus.Null);

                dgv_data.Rows.Clear();
                return;
            }

            if (JobService.Status == ServiceControllerStatus.StartPending)
            {
                refreshStatusMsgByServerStatus(ServerStatus.StartPending);
                refreshServerControlBtn(ServerStatus.StartPending);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.Running)
            {
                refreshStatusMsgByServerStatus(ServerStatus.Running);
                refreshServerControlBtn(ServerStatus.Running);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.StopPending)
            {
                refreshStatusMsgByServerStatus(ServerStatus.StopPending);
                refreshServerControlBtn(ServerStatus.StopPending);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.Stopped)
            {
                refreshStatusMsgByServerStatus(ServerStatus.Stoped);
                refreshServerControlBtn(ServerStatus.Stoped);

                dgv_data.Rows.Clear();
                return;
            }

            if (JobService.Status == ServiceControllerStatus.ContinuePending)
            {
                refreshStatusMsgByServerStatus(ServerStatus.ContinuePending);
                refreshServerControlBtn(ServerStatus.ContinuePending);
                return;
            }

            if (JobService.Status == ServiceControllerStatus.Paused)
            {
                refreshStatusMsgByServerStatus(ServerStatus.Stoped);
                refreshServerControlBtn(ServerStatus.Stoped);

                dgv_data.Rows.Clear();
                return;
            }

            if (JobService.Status == ServiceControllerStatus.PausePending)
            {
                refreshStatusMsgByServerStatus(ServerStatus.PausePending);
                refreshServerControlBtn(ServerStatus.PausePending);

                dgv_data.Rows.Clear();
                return;
            }
        }

        private void refreshServerControlBtn(ServerStatus status)
        {
            switch (status)
            {
                case ServerStatus.Null:
                    btn_installSvc.Enabled = true;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = false;
                    gbx_jobTool.Enabled = false;
                    pl_main.Enabled = false;
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
                    pl_main.Enabled = false;
                    break;
                case ServerStatus.Running:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = true;
                    btn_uninstallSvc.Enabled = true;
                    gbx_jobTool.Enabled = true;
                    pl_main.Enabled = true;
                    break;
                case ServerStatus.Stoped:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = true;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = true;
                    gbx_jobTool.Enabled = false;
                    pl_main.Enabled = false;
                    break;
                case ServerStatus.Error:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = false;
                    gbx_jobTool.Enabled = false;
                    pl_main.Enabled = false;
                    break;
                default:
                    btn_installSvc.Enabled = false;
                    btn_startSvc.Enabled = false;
                    btn_stopSvc.Enabled = false;
                    btn_uninstallSvc.Enabled = false;
                    gbx_jobTool.Enabled = false;
                    pl_main.Enabled = false;
                    break;
            }
        }

        private void refreshStatusMsgByServerStatus(ServerStatus status)
        {
            switch (status)
            {
                case ServerStatus.Null:
                    refreshStatusMsg("未发现服务");
                    break;
                case ServerStatus.Installing:
                    refreshStatusMsg("服务安装中");
                    break;
                case ServerStatus.Uninstalling:
                    refreshStatusMsg("服务安装中");
                    break;
                case ServerStatus.StartPending:
                    refreshStatusMsg("服务正在启动");
                    break;
                case ServerStatus.StopPending:
                    refreshStatusMsg("服务正在停止");
                    break;
                case ServerStatus.PausePending:
                    refreshStatusMsg("服务正在暂停");
                    break;
                case ServerStatus.ContinuePending:
                    refreshStatusMsg("服务被挂起");
                    break;
                case ServerStatus.Running:
                    refreshStatusMsg("服务正在运行");
                    break;
                case ServerStatus.Stoped:
                    refreshStatusMsg("服务未运行");
                    break;
                case ServerStatus.Error:
                    refreshStatusMsg("未知错误");
                    break;
                default:
                    refreshStatusMsg("未知状态");
                    break;
            }

        }

        private void refreshStatusMsg(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => { refreshStatusMsg(msg); }));
                return;
            }
            Text = $"{_title}（{msg}）";
        }

        private void dgv_data_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
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
                        AsyncUtil.Run(() =>
                        {
                            var b = job.Enable ? _jobManagerProxy.DisableJob(job.Id) : _jobManagerProxy.EnableJob(job.Id);
                        });
                        
                    });

                    menu.Show(dgv_data, new Point(cellLocation.X + e.X, cellLocation.Y + e.Y));
                }
                
            }
        }
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

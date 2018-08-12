using System;
using System.IO;
using System.Windows.Forms;
using HlcJobCommon;
using HlcJobCommon.Wcf;
using HlcJobManager.Wcf;
using HLC.Common.Utils;
using NLog;

namespace HlcJobManager
{
    public partial class JobEditForm : Form
    {
        private JobManagerProxy _jobManagerProxy;
        private ManageJob _job;
        private ILogger _logger;

        public string JobId { get; set; }

        public JobEditForm()
        {
            InitializeComponent();

            _logger = NLog.LogManager.GetCurrentClassLogger();

            cmb_type.Items.Add(JobType.DLL);
            cmb_type.Items.Add(JobType.EXE);
            cmb_type.Items.Add(JobType.CMD);

            cmb_enable.Items.Add("启用");
            cmb_enable.Items.Add("禁用");


            _jobManagerProxy = new JobManagerProxy();
        }

        public JobEditForm(ManageJob job) : this()
        {
            _job = job;
            txt_name.Text = job.Name;
            cmb_type.Text = job.Type.ToString();
            txt_cron.Text = job.Cron;
            cmb_enable.Text = job.Enable ? "启用" : "禁用";
            txt_filePath.Text = job.FilePath;
            txt_workPath.Text = job.WorkPath;
            txt_className.Text = job.ClassName;
            txt_methodName.Text = job.MethodName;
            foreach (var param in job.Params)
            {
                dgv_params.Rows.Add(param);
            }
        }

        private void JobEditForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (!CheckFormData())
            {
                return;
            }
            
            var job = GetJob();

            AsyncUtil.Run(() =>
            {
                if (_job == null ? _jobManagerProxy.AddJob(job) : _jobManagerProxy.UpdateJob(job))
                {
                    JobId = job.Id;

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }, exception: ex =>
            {
                _logger.Error(ex, "添加或修改任务失败");
                DialogResult = DialogResult.Cancel;
            });
        }

        private bool CheckFormData()
        {
            var cron = txt_cron.Text;
            var workDir = txt_workPath.Text;

            var cronItems = cron.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (!(cron.Equals(Constant.ServerCron)  || cronItems.Length == 6 || cronItems.Length == 7))
            {
                MessageBox.Show("调度计划格式有误，必须为6位或7位,或一次性服务任务'-'");
                return false;
            }

            if (txt_workPath.Visible && !string.IsNullOrEmpty(workDir) && !Directory.Exists(workDir))
            {
                MessageBox.Show("工作目录不存在");
                return false;
            }

            return true;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private ManageJob GetJob()
        {
            var job = _job;
            if (job == null)
            {
                job = new ManageJob();
            }
            
            job.Name = txt_name.Text;

            Enum.TryParse(cmb_type.Text, out JobType jobType);
            job.Type = jobType;

            job.Cron = txt_cron.Text;

            job.Enable = "启用".Equals(cmb_enable.Text);

            job.FilePath = txt_filePath.Text;

            job.WorkPath = txt_workPath.Text;

            job.ClassName = txt_className.Text;

            job.MethodName = txt_methodName.Text;

            job.Params.Clear();

            foreach (DataGridViewRow row in dgv_params.Rows)
            {
                var value = row.Cells["cln_param"].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    job.Params.Add(value);
                }
            }

            return job;
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedType = cmb_type.Text;
            if (JobType.DLL.ToString().Equals(selectedType))
            {
                lb_filePath.Text = "dll路径：";
                txt_filePath.Text = "";

                dgv_params.Rows.Clear();
                dgv_params.Enabled = true;
                dgv_params.Visible = true;

                txt_className.Text = "";
                txt_className.Enabled = true;
                txt_className.Visible = true;

                txt_methodName.Text = "";
                txt_methodName.Enabled = true;
                txt_methodName.Visible = true;

                lb_className.Visible = true;
                lb_methodName.Visible = true;

                txt_cron.Enabled = true;
                txt_cron.Text = "";

                lb_workPath.Visible = false;
                txt_workPath.Text = "";
                txt_workPath.Visible = false;

                Height = 280;
            }
            else if (JobType.EXE.ToString().Equals(selectedType))
            {
                lb_filePath.Text = "exe路径：";
                txt_filePath.Text = "";

                dgv_params.Rows.Clear();
                dgv_params.Enabled = true;
                dgv_params.Visible = true;

                txt_className.Text = "";
                txt_className.Enabled = false;
                txt_className.Visible = false;

                txt_methodName.Text = "";
                txt_methodName.Enabled = false;
                txt_methodName.Visible = false;

                lb_className.Visible = false;
                lb_methodName.Visible = false;

                txt_cron.Enabled = true;
                txt_cron.Text = "";

                lb_workPath.Visible = false;
                txt_workPath.Text = "";
                txt_workPath.Visible = false;

                Height = 280;
            }
            else if(JobType.CMD.ToString().Equals(selectedType))
            {
                lb_filePath.Text = "cmd命令：";
                txt_filePath.Text = "";

                dgv_params.Rows.Clear();
                dgv_params.Enabled = false;
                dgv_params.Visible = false;

                txt_className.Text = "";
                txt_className.Enabled = false;
                txt_className.Visible = false;

                txt_methodName.Text = "";
                txt_methodName.Enabled = false;
                txt_methodName.Visible = false;

                lb_className.Visible = false;
                lb_methodName.Visible = false;

                txt_cron.Enabled = true;
                txt_cron.Text = "";

                lb_workPath.Visible = true;
                txt_workPath.Text = "";
                txt_workPath.Visible = true;

                Height = 200;
            }
        }
    }
}

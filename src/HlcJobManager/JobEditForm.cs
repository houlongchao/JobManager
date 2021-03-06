﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HlcJobCommon;
using HlcJobCommon.Wcf;
using HlcJobManager.Properties;
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

            cmb_enable.Items.Add(Resources.Enable);
            cmb_enable.Items.Add(Resources.Disable);


            _jobManagerProxy = new JobManagerProxy();
        }

        public JobEditForm(ManageJob job) : this()
        {
            _job = job;

            FillJob(job);
        }

        private void JobEditForm_Load(object sender, EventArgs e)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(img_help, Resources.CronHelp);
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedType = cmb_type.Text;
            if (JobType.DLL.ToString().Equals(selectedType))
            {
                lb_workPath.Text = Resources.JobEditForm_Lable_dllPath;
                txt_workPath.Text = "";

                dgv_params.Rows.Clear();
                dgv_params.Enabled = true;
                dgv_params.Visible = true;

                cmb_classes.Text = "";
                cmb_classes.Enabled = true;
                cmb_classes.Visible = true;

                cmb_methods.Text = "";
                cmb_methods.Enabled = true;
                cmb_methods.Visible = true;

                lb_className.Visible = true;
                lb_methodName.Visible = true;

                txt_cron.Enabled = true;
                txt_cron.Text = "";

                lb_cmd.Visible = false;
                txt_cmd.Text = "";
                txt_cmd.Visible = false;

                Height = 280;
            }
            else if (JobType.EXE.ToString().Equals(selectedType))
            {
                lb_workPath.Text = Resources.JobEditForm_Lable_exePath;
                txt_workPath.Text = "";

                dgv_params.Rows.Clear();
                dgv_params.Enabled = true;
                dgv_params.Visible = true;

                cmb_classes.Text = "";
                cmb_classes.Enabled = false;
                cmb_classes.Visible = false;

                cmb_methods.Text = "";
                cmb_methods.Enabled = false;
                cmb_methods.Visible = false;

                lb_className.Visible = false;
                lb_methodName.Visible = false;

                txt_cron.Enabled = true;
                txt_cron.Text = "";

                lb_cmd.Visible = false;
                txt_cmd.Text = "";
                txt_cmd.Visible = false;

                Height = 280;
            }
            else if(JobType.CMD.ToString().Equals(selectedType))
            {
                lb_workPath.Text = Resources.JobEditForm_Lable_workPath;
                txt_workPath.Text = "";

                dgv_params.Rows.Clear();
                dgv_params.Enabled = false;
                dgv_params.Visible = false;

                cmb_classes.Text = "";
                cmb_classes.Enabled = false;
                cmb_classes.Visible = false;

                cmb_methods.Text = "";
                cmb_methods.Enabled = false;
                cmb_methods.Visible = false;

                lb_className.Visible = false;
                lb_methodName.Visible = false;

                txt_cron.Enabled = true;
                txt_cron.Text = "";

                lb_cmd.Visible = true;
                txt_cmd.Text = "";
                txt_cmd.Visible = true;

                Height = 200;
            }
        }

        private void btn_selectPath_Click(object sender, EventArgs e)
        {
            var selectedType = cmb_type.Text;
            if (JobType.DLL.ToString().Equals(selectedType))
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "*.dll|*.dll";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txt_workPath.Text = ofd.FileName;
                        InitCmbClasses();
                    }
                }
            }
            else if (JobType.EXE.ToString().Equals(selectedType))
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "*.exe|*.exe";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txt_workPath.Text = ofd.FileName;
                    }
                }
            }
            else if (JobType.CMD.ToString().Equals(selectedType))
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.Description = Resources.JobEditForm_PleaseSelectWorkPath;
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        txt_workPath.Text = fbd.SelectedPath;
                    }
                }
            }
        }

        private void cmb_classes_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCmbMethods();
        }
        
        private bool CheckFormData()
        {
            var cron = txt_cron.Text;
            var workDir = txt_workPath.Text;

            var cronItems = cron.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (!(cron.Equals(Constant.ServerCron) || cronItems.Length == 6 || cronItems.Length == 7))
            {
                MessageBox.Show(Resources.JobEditForm_CronErrorMessage);
                return false;
            }

            if (txt_cmd.Visible && !string.IsNullOrEmpty(workDir) && !Directory.Exists(workDir))
            {
                MessageBox.Show(Resources.JobEditForm_WorkPathNotExist);
                return false;
            }

            if (!txt_cmd.Visible && !File.Exists(workDir))
            {
                MessageBox.Show(Resources.JobEditForm_WorkFileNotExist);
                return false;
            }

            if (txt_cmd.Visible && string.IsNullOrEmpty(txt_cmd.Text))
            {
                MessageBox.Show(Resources.JobEditForm_NoCmd);
                return false;
            }

            return true;
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

            job.Enable = Resources.Enable.Equals(cmb_enable.Text);

            job.WorkPath = txt_workPath.Text;

            job.Command = txt_cmd.Text;

            job.ClassName = cmb_classes.Text;

            job.MethodName = cmb_methods.Text;

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

        private void FillJob(ManageJob job)
        {
            txt_name.Text = job.Name;
            cmb_type.Text = job.Type.ToString();
            txt_cron.Text = job.Cron;
            cmb_enable.Text = job.Enable ? Resources.Enable : Resources.Disable;
            txt_workPath.Text = job.WorkPath;
            txt_cmd.Text = job.Command;

            InitCmbClasses();
            cmb_classes.Text = job.ClassName;

            InitCmbMethods();
            cmb_methods.Text = job.MethodName;

            foreach (var param in job.Params)
            {
                dgv_params.Rows.Add(param);
            }
        }
        
        private void InitCmbClasses()
        {
            if (!JobType.DLL.ToString().Equals(cmb_type.Text) || !File.Exists(txt_workPath.Text))
            {
                return;
            }
            cmb_classes.Items.Clear();
            var proxy = DynamicUtil.LoadDomain(txt_workPath.Text);
            var allExportTypes = proxy.GetAllExportTypes().ToList();
            allExportTypes.Sort();
            cmb_classes.Items.AddRange(allExportTypes.ToArray());
            DynamicUtil.UnloadDomain(proxy);
        }
        
        private void InitCmbMethods()
        {
            if (!JobType.DLL.ToString().Equals(cmb_type.Text) || !File.Exists(txt_workPath.Text))
            {
                return;
            }
            cmb_methods.Items.Clear();
            var proxy = DynamicUtil.LoadDomain(txt_workPath.Text);
            var allMethodes = proxy.GetMethodes(cmb_classes.Text).ToList();
            allMethodes.Sort();
            cmb_methods.Items.AddRange(allMethodes.ToArray());
            DynamicUtil.UnloadDomain(proxy);
        }
    }
}

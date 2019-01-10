namespace HlcJobManager
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.spl_main = new System.Windows.Forms.SplitContainer();
            this.dgv_data = new System.Windows.Forms.DataGridView();
            this.cln_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cln_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cln_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cln_cron = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cln_preTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cln_nextTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cln_enable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cln_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.pl_main = new System.Windows.Forms.Panel();
            this.gbx_jobTool = new System.Windows.Forms.GroupBox();
            this.btn_refreshJobs = new System.Windows.Forms.Button();
            this.btn_addJob = new System.Windows.Forms.Button();
            this.btn_viewLog = new System.Windows.Forms.Button();
            this.gbx_logTool = new System.Windows.Forms.GroupBox();
            this.cbx_wordWrap = new System.Windows.Forms.CheckBox();
            this.btn_clearLog = new System.Windows.Forms.Button();
            this.gbx_serviceTool = new System.Windows.Forms.GroupBox();
            this.btn_uninstallSvc = new System.Windows.Forms.Button();
            this.btn_stopSvc = new System.Windows.Forms.Button();
            this.btn_startSvc = new System.Windows.Forms.Button();
            this.btn_installSvc = new System.Windows.Forms.Button();
            this.lb_version = new System.Windows.Forms.Label();
            this.lb_author = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spl_main)).BeginInit();
            this.spl_main.Panel1.SuspendLayout();
            this.spl_main.Panel2.SuspendLayout();
            this.spl_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).BeginInit();
            this.pl_main.SuspendLayout();
            this.gbx_jobTool.SuspendLayout();
            this.gbx_logTool.SuspendLayout();
            this.gbx_serviceTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // spl_main
            // 
            resources.ApplyResources(this.spl_main, "spl_main");
            this.spl_main.Name = "spl_main";
            // 
            // spl_main.Panel1
            // 
            resources.ApplyResources(this.spl_main.Panel1, "spl_main.Panel1");
            this.spl_main.Panel1.Controls.Add(this.dgv_data);
            this.toolTip.SetToolTip(this.spl_main.Panel1, resources.GetString("spl_main.Panel1.ToolTip"));
            // 
            // spl_main.Panel2
            // 
            resources.ApplyResources(this.spl_main.Panel2, "spl_main.Panel2");
            this.spl_main.Panel2.Controls.Add(this.txt_log);
            this.toolTip.SetToolTip(this.spl_main.Panel2, resources.GetString("spl_main.Panel2.ToolTip"));
            this.spl_main.TabStop = false;
            this.toolTip.SetToolTip(this.spl_main, resources.GetString("spl_main.ToolTip"));
            // 
            // dgv_data
            // 
            resources.ApplyResources(this.dgv_data, "dgv_data");
            this.dgv_data.AllowUserToAddRows = false;
            this.dgv_data.AllowUserToDeleteRows = false;
            this.dgv_data.AllowUserToResizeRows = false;
            this.dgv_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cln_id,
            this.cln_name,
            this.cln_type,
            this.cln_cron,
            this.cln_preTime,
            this.cln_nextTime,
            this.cln_enable,
            this.cln_state});
            this.dgv_data.MultiSelect = false;
            this.dgv_data.Name = "dgv_data";
            this.dgv_data.ReadOnly = true;
            this.dgv_data.RowHeadersVisible = false;
            this.dgv_data.RowTemplate.Height = 23;
            this.dgv_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip.SetToolTip(this.dgv_data, resources.GetString("dgv_data.ToolTip"));
            this.dgv_data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_data_CellDoubleClick);
            this.dgv_data.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_data_CellMouseClick);
            this.dgv_data.SelectionChanged += new System.EventHandler(this.dgv_data_SelectionChanged);
            this.dgv_data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_data_KeyDown);
            // 
            // cln_id
            // 
            resources.ApplyResources(this.cln_id, "cln_id");
            this.cln_id.Name = "cln_id";
            this.cln_id.ReadOnly = true;
            // 
            // cln_name
            // 
            this.cln_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.cln_name, "cln_name");
            this.cln_name.Name = "cln_name";
            this.cln_name.ReadOnly = true;
            // 
            // cln_type
            // 
            resources.ApplyResources(this.cln_type, "cln_type");
            this.cln_type.Name = "cln_type";
            this.cln_type.ReadOnly = true;
            // 
            // cln_cron
            // 
            this.cln_cron.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.cln_cron, "cln_cron");
            this.cln_cron.Name = "cln_cron";
            this.cln_cron.ReadOnly = true;
            // 
            // cln_preTime
            // 
            resources.ApplyResources(this.cln_preTime, "cln_preTime");
            this.cln_preTime.Name = "cln_preTime";
            this.cln_preTime.ReadOnly = true;
            // 
            // cln_nextTime
            // 
            resources.ApplyResources(this.cln_nextTime, "cln_nextTime");
            this.cln_nextTime.Name = "cln_nextTime";
            this.cln_nextTime.ReadOnly = true;
            // 
            // cln_enable
            // 
            resources.ApplyResources(this.cln_enable, "cln_enable");
            this.cln_enable.Name = "cln_enable";
            this.cln_enable.ReadOnly = true;
            // 
            // cln_state
            // 
            resources.ApplyResources(this.cln_state, "cln_state");
            this.cln_state.Name = "cln_state";
            this.cln_state.ReadOnly = true;
            // 
            // txt_log
            // 
            resources.ApplyResources(this.txt_log, "txt_log");
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.TabStop = false;
            this.toolTip.SetToolTip(this.txt_log, resources.GetString("txt_log.ToolTip"));
            // 
            // pl_main
            // 
            resources.ApplyResources(this.pl_main, "pl_main");
            this.pl_main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pl_main.Controls.Add(this.spl_main);
            this.pl_main.Name = "pl_main";
            this.toolTip.SetToolTip(this.pl_main, resources.GetString("pl_main.ToolTip"));
            // 
            // gbx_jobTool
            // 
            resources.ApplyResources(this.gbx_jobTool, "gbx_jobTool");
            this.gbx_jobTool.Controls.Add(this.btn_refreshJobs);
            this.gbx_jobTool.Controls.Add(this.btn_addJob);
            this.gbx_jobTool.Name = "gbx_jobTool";
            this.gbx_jobTool.TabStop = false;
            this.toolTip.SetToolTip(this.gbx_jobTool, resources.GetString("gbx_jobTool.ToolTip"));
            // 
            // btn_refreshJobs
            // 
            resources.ApplyResources(this.btn_refreshJobs, "btn_refreshJobs");
            this.btn_refreshJobs.Name = "btn_refreshJobs";
            this.btn_refreshJobs.TabStop = false;
            this.toolTip.SetToolTip(this.btn_refreshJobs, resources.GetString("btn_refreshJobs.ToolTip"));
            this.btn_refreshJobs.UseVisualStyleBackColor = true;
            this.btn_refreshJobs.Click += new System.EventHandler(this.btn_refreshJobs_Click);
            // 
            // btn_addJob
            // 
            resources.ApplyResources(this.btn_addJob, "btn_addJob");
            this.btn_addJob.Name = "btn_addJob";
            this.btn_addJob.TabStop = false;
            this.toolTip.SetToolTip(this.btn_addJob, resources.GetString("btn_addJob.ToolTip"));
            this.btn_addJob.UseVisualStyleBackColor = true;
            this.btn_addJob.Click += new System.EventHandler(this.btn_addJob_Click);
            // 
            // btn_viewLog
            // 
            resources.ApplyResources(this.btn_viewLog, "btn_viewLog");
            this.btn_viewLog.Name = "btn_viewLog";
            this.btn_viewLog.TabStop = false;
            this.toolTip.SetToolTip(this.btn_viewLog, resources.GetString("btn_viewLog.ToolTip"));
            this.btn_viewLog.UseVisualStyleBackColor = true;
            this.btn_viewLog.Click += new System.EventHandler(this.btn_viewLog_Click);
            // 
            // gbx_logTool
            // 
            resources.ApplyResources(this.gbx_logTool, "gbx_logTool");
            this.gbx_logTool.Controls.Add(this.cbx_wordWrap);
            this.gbx_logTool.Controls.Add(this.btn_clearLog);
            this.gbx_logTool.Controls.Add(this.btn_viewLog);
            this.gbx_logTool.Name = "gbx_logTool";
            this.gbx_logTool.TabStop = false;
            this.toolTip.SetToolTip(this.gbx_logTool, resources.GetString("gbx_logTool.ToolTip"));
            // 
            // cbx_wordWrap
            // 
            resources.ApplyResources(this.cbx_wordWrap, "cbx_wordWrap");
            this.cbx_wordWrap.Name = "cbx_wordWrap";
            this.toolTip.SetToolTip(this.cbx_wordWrap, resources.GetString("cbx_wordWrap.ToolTip"));
            this.cbx_wordWrap.UseVisualStyleBackColor = true;
            this.cbx_wordWrap.CheckedChanged += new System.EventHandler(this.cbx_wordWrap_CheckedChanged);
            // 
            // btn_clearLog
            // 
            resources.ApplyResources(this.btn_clearLog, "btn_clearLog");
            this.btn_clearLog.Name = "btn_clearLog";
            this.btn_clearLog.TabStop = false;
            this.toolTip.SetToolTip(this.btn_clearLog, resources.GetString("btn_clearLog.ToolTip"));
            this.btn_clearLog.UseVisualStyleBackColor = true;
            this.btn_clearLog.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // gbx_serviceTool
            // 
            resources.ApplyResources(this.gbx_serviceTool, "gbx_serviceTool");
            this.gbx_serviceTool.Controls.Add(this.btn_uninstallSvc);
            this.gbx_serviceTool.Controls.Add(this.btn_stopSvc);
            this.gbx_serviceTool.Controls.Add(this.btn_startSvc);
            this.gbx_serviceTool.Controls.Add(this.btn_installSvc);
            this.gbx_serviceTool.Name = "gbx_serviceTool";
            this.gbx_serviceTool.TabStop = false;
            this.toolTip.SetToolTip(this.gbx_serviceTool, resources.GetString("gbx_serviceTool.ToolTip"));
            // 
            // btn_uninstallSvc
            // 
            resources.ApplyResources(this.btn_uninstallSvc, "btn_uninstallSvc");
            this.btn_uninstallSvc.Name = "btn_uninstallSvc";
            this.btn_uninstallSvc.TabStop = false;
            this.toolTip.SetToolTip(this.btn_uninstallSvc, resources.GetString("btn_uninstallSvc.ToolTip"));
            this.btn_uninstallSvc.UseVisualStyleBackColor = true;
            this.btn_uninstallSvc.Click += new System.EventHandler(this.btn_uninstallSvc_Click);
            // 
            // btn_stopSvc
            // 
            resources.ApplyResources(this.btn_stopSvc, "btn_stopSvc");
            this.btn_stopSvc.Name = "btn_stopSvc";
            this.btn_stopSvc.TabStop = false;
            this.toolTip.SetToolTip(this.btn_stopSvc, resources.GetString("btn_stopSvc.ToolTip"));
            this.btn_stopSvc.UseVisualStyleBackColor = true;
            this.btn_stopSvc.Click += new System.EventHandler(this.btn_stopSvc_Click);
            // 
            // btn_startSvc
            // 
            resources.ApplyResources(this.btn_startSvc, "btn_startSvc");
            this.btn_startSvc.Name = "btn_startSvc";
            this.btn_startSvc.TabStop = false;
            this.toolTip.SetToolTip(this.btn_startSvc, resources.GetString("btn_startSvc.ToolTip"));
            this.btn_startSvc.UseVisualStyleBackColor = true;
            this.btn_startSvc.Click += new System.EventHandler(this.btn_startSvc_Click);
            // 
            // btn_installSvc
            // 
            resources.ApplyResources(this.btn_installSvc, "btn_installSvc");
            this.btn_installSvc.Name = "btn_installSvc";
            this.btn_installSvc.TabStop = false;
            this.toolTip.SetToolTip(this.btn_installSvc, resources.GetString("btn_installSvc.ToolTip"));
            this.btn_installSvc.UseVisualStyleBackColor = true;
            this.btn_installSvc.Click += new System.EventHandler(this.btn_installSvc_Click);
            // 
            // lb_version
            // 
            resources.ApplyResources(this.lb_version, "lb_version");
            this.lb_version.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_version.Name = "lb_version";
            this.toolTip.SetToolTip(this.lb_version, resources.GetString("lb_version.ToolTip"));
            this.lb_version.Click += new System.EventHandler(this.about_click);
            this.lb_version.MouseEnter += new System.EventHandler(this.about_enter);
            // 
            // lb_author
            // 
            resources.ApplyResources(this.lb_author, "lb_author");
            this.lb_author.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_author.Name = "lb_author";
            this.toolTip.SetToolTip(this.lb_author, resources.GetString("lb_author.ToolTip"));
            this.lb_author.Click += new System.EventHandler(this.about_click);
            this.lb_author.MouseEnter += new System.EventHandler(this.about_enter);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_author);
            this.Controls.Add(this.lb_version);
            this.Controls.Add(this.gbx_serviceTool);
            this.Controls.Add(this.gbx_logTool);
            this.Controls.Add(this.gbx_jobTool);
            this.Controls.Add(this.pl_main);
            this.Name = "MainForm";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.spl_main.Panel1.ResumeLayout(false);
            this.spl_main.Panel2.ResumeLayout(false);
            this.spl_main.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spl_main)).EndInit();
            this.spl_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).EndInit();
            this.pl_main.ResumeLayout(false);
            this.gbx_jobTool.ResumeLayout(false);
            this.gbx_logTool.ResumeLayout(false);
            this.gbx_logTool.PerformLayout();
            this.gbx_serviceTool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_main;
        private System.Windows.Forms.GroupBox gbx_jobTool;
        private System.Windows.Forms.SplitContainer spl_main;
        private System.Windows.Forms.DataGridView dgv_data;
        private System.Windows.Forms.Button btn_viewLog;
        private System.Windows.Forms.Button btn_addJob;
        private System.Windows.Forms.GroupBox gbx_logTool;
        private System.Windows.Forms.GroupBox gbx_serviceTool;
        private System.Windows.Forms.Button btn_installSvc;
        private System.Windows.Forms.Button btn_uninstallSvc;
        private System.Windows.Forms.Button btn_stopSvc;
        private System.Windows.Forms.Button btn_startSvc;
        private System.Windows.Forms.Button btn_refreshJobs;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.Label lb_version;
        private System.Windows.Forms.Button btn_clearLog;
        private System.Windows.Forms.Label lb_author;
        private System.Windows.Forms.CheckBox cbx_wordWrap;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_cron;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_preTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_nextTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_enable;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_state;
    }
}


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pl_main = new System.Windows.Forms.Panel();
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
            this.gbx_jobTool = new System.Windows.Forms.GroupBox();
            this.btn_refreshJobs = new System.Windows.Forms.Button();
            this.btn_addJob = new System.Windows.Forms.Button();
            this.btn_viewLog = new System.Windows.Forms.Button();
            this.gbx_logTool = new System.Windows.Forms.GroupBox();
            this.btn_clearLog = new System.Windows.Forms.Button();
            this.gbx_serviceTool = new System.Windows.Forms.GroupBox();
            this.btn_uninstallSvc = new System.Windows.Forms.Button();
            this.btn_stopSvc = new System.Windows.Forms.Button();
            this.btn_startSvc = new System.Windows.Forms.Button();
            this.btn_installSvc = new System.Windows.Forms.Button();
            this.lb_version = new System.Windows.Forms.Label();
            this.lb_author = new System.Windows.Forms.Label();
            this.pl_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spl_main)).BeginInit();
            this.spl_main.Panel1.SuspendLayout();
            this.spl_main.Panel2.SuspendLayout();
            this.spl_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).BeginInit();
            this.gbx_jobTool.SuspendLayout();
            this.gbx_logTool.SuspendLayout();
            this.gbx_serviceTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pl_main
            // 
            this.pl_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pl_main.Controls.Add(this.spl_main);
            this.pl_main.Location = new System.Drawing.Point(9, 7);
            this.pl_main.Name = "pl_main";
            this.pl_main.Size = new System.Drawing.Size(643, 478);
            this.pl_main.TabIndex = 0;
            // 
            // spl_main
            // 
            this.spl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spl_main.Location = new System.Drawing.Point(0, 0);
            this.spl_main.Name = "spl_main";
            this.spl_main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spl_main.Panel1
            // 
            this.spl_main.Panel1.Controls.Add(this.dgv_data);
            // 
            // spl_main.Panel2
            // 
            this.spl_main.Panel2.Controls.Add(this.txt_log);
            this.spl_main.Size = new System.Drawing.Size(641, 476);
            this.spl_main.SplitterDistance = 245;
            this.spl_main.TabIndex = 0;
            // 
            // dgv_data
            // 
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
            this.dgv_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_data.Location = new System.Drawing.Point(0, 0);
            this.dgv_data.MultiSelect = false;
            this.dgv_data.Name = "dgv_data";
            this.dgv_data.ReadOnly = true;
            this.dgv_data.RowHeadersVisible = false;
            this.dgv_data.RowTemplate.Height = 23;
            this.dgv_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_data.Size = new System.Drawing.Size(641, 245);
            this.dgv_data.TabIndex = 0;
            this.dgv_data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_data_CellDoubleClick);
            this.dgv_data.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_data_CellMouseClick);
            this.dgv_data.SelectionChanged += new System.EventHandler(this.dgv_data_SelectionChanged);
            this.dgv_data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_data_KeyDown);
            // 
            // cln_id
            // 
            this.cln_id.HeaderText = "Id";
            this.cln_id.Name = "cln_id";
            this.cln_id.ReadOnly = true;
            this.cln_id.Visible = false;
            // 
            // cln_name
            // 
            this.cln_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cln_name.HeaderText = "名称";
            this.cln_name.Name = "cln_name";
            this.cln_name.ReadOnly = true;
            // 
            // cln_type
            // 
            this.cln_type.HeaderText = "类型";
            this.cln_type.Name = "cln_type";
            this.cln_type.ReadOnly = true;
            this.cln_type.Width = 60;
            // 
            // cln_cron
            // 
            this.cln_cron.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cln_cron.HeaderText = "调度计划";
            this.cln_cron.Name = "cln_cron";
            this.cln_cron.ReadOnly = true;
            // 
            // cln_preTime
            // 
            this.cln_preTime.HeaderText = "上次调用时间";
            this.cln_preTime.Name = "cln_preTime";
            this.cln_preTime.ReadOnly = true;
            this.cln_preTime.Width = 130;
            // 
            // cln_nextTime
            // 
            this.cln_nextTime.HeaderText = "下次调用时间";
            this.cln_nextTime.Name = "cln_nextTime";
            this.cln_nextTime.ReadOnly = true;
            this.cln_nextTime.Width = 130;
            // 
            // cln_enable
            // 
            this.cln_enable.HeaderText = "启用";
            this.cln_enable.Name = "cln_enable";
            this.cln_enable.ReadOnly = true;
            this.cln_enable.Width = 60;
            // 
            // cln_state
            // 
            this.cln_state.HeaderText = "状态";
            this.cln_state.Name = "cln_state";
            this.cln_state.ReadOnly = true;
            this.cln_state.Width = 60;
            // 
            // txt_log
            // 
            this.txt_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_log.Location = new System.Drawing.Point(0, 0);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_log.Size = new System.Drawing.Size(641, 227);
            this.txt_log.TabIndex = 0;
            // 
            // gbx_jobTool
            // 
            this.gbx_jobTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbx_jobTool.Controls.Add(this.btn_refreshJobs);
            this.gbx_jobTool.Controls.Add(this.btn_addJob);
            this.gbx_jobTool.Location = new System.Drawing.Point(661, 8);
            this.gbx_jobTool.Name = "gbx_jobTool";
            this.gbx_jobTool.Size = new System.Drawing.Size(115, 124);
            this.gbx_jobTool.TabIndex = 1;
            this.gbx_jobTool.TabStop = false;
            this.gbx_jobTool.Text = "任务控制栏";
            // 
            // btn_refreshJobs
            // 
            this.btn_refreshJobs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_refreshJobs.Location = new System.Drawing.Point(21, 37);
            this.btn_refreshJobs.Name = "btn_refreshJobs";
            this.btn_refreshJobs.Size = new System.Drawing.Size(75, 23);
            this.btn_refreshJobs.TabIndex = 0;
            this.btn_refreshJobs.Text = "刷新任务";
            this.btn_refreshJobs.UseVisualStyleBackColor = true;
            this.btn_refreshJobs.Click += new System.EventHandler(this.btn_refreshJobs_Click);
            // 
            // btn_addJob
            // 
            this.btn_addJob.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_addJob.Location = new System.Drawing.Point(22, 82);
            this.btn_addJob.Name = "btn_addJob";
            this.btn_addJob.Size = new System.Drawing.Size(75, 23);
            this.btn_addJob.TabIndex = 0;
            this.btn_addJob.Text = "添加任务";
            this.btn_addJob.UseVisualStyleBackColor = true;
            this.btn_addJob.Click += new System.EventHandler(this.btn_addJob_Click);
            // 
            // btn_viewLog
            // 
            this.btn_viewLog.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_viewLog.Location = new System.Drawing.Point(22, 20);
            this.btn_viewLog.Name = "btn_viewLog";
            this.btn_viewLog.Size = new System.Drawing.Size(75, 23);
            this.btn_viewLog.TabIndex = 0;
            this.btn_viewLog.Text = "隐藏日志";
            this.btn_viewLog.UseVisualStyleBackColor = true;
            this.btn_viewLog.Click += new System.EventHandler(this.btn_viewLog_Click);
            // 
            // gbx_logTool
            // 
            this.gbx_logTool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbx_logTool.Controls.Add(this.btn_clearLog);
            this.gbx_logTool.Controls.Add(this.btn_viewLog);
            this.gbx_logTool.Location = new System.Drawing.Point(660, 290);
            this.gbx_logTool.Name = "gbx_logTool";
            this.gbx_logTool.Size = new System.Drawing.Size(115, 144);
            this.gbx_logTool.TabIndex = 2;
            this.gbx_logTool.TabStop = false;
            this.gbx_logTool.Text = "日志控制栏";
            // 
            // btn_clearLog
            // 
            this.btn_clearLog.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_clearLog.Location = new System.Drawing.Point(22, 49);
            this.btn_clearLog.Name = "btn_clearLog";
            this.btn_clearLog.Size = new System.Drawing.Size(75, 23);
            this.btn_clearLog.TabIndex = 0;
            this.btn_clearLog.Text = "清空日志";
            this.btn_clearLog.UseVisualStyleBackColor = true;
            this.btn_clearLog.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // gbx_serviceTool
            // 
            this.gbx_serviceTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbx_serviceTool.Controls.Add(this.btn_uninstallSvc);
            this.gbx_serviceTool.Controls.Add(this.btn_stopSvc);
            this.gbx_serviceTool.Controls.Add(this.btn_startSvc);
            this.gbx_serviceTool.Controls.Add(this.btn_installSvc);
            this.gbx_serviceTool.Location = new System.Drawing.Point(661, 138);
            this.gbx_serviceTool.Name = "gbx_serviceTool";
            this.gbx_serviceTool.Size = new System.Drawing.Size(115, 146);
            this.gbx_serviceTool.TabIndex = 2;
            this.gbx_serviceTool.TabStop = false;
            this.gbx_serviceTool.Text = "服务控制栏";
            // 
            // btn_uninstallSvc
            // 
            this.btn_uninstallSvc.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_uninstallSvc.Location = new System.Drawing.Point(23, 107);
            this.btn_uninstallSvc.Name = "btn_uninstallSvc";
            this.btn_uninstallSvc.Size = new System.Drawing.Size(75, 23);
            this.btn_uninstallSvc.TabIndex = 0;
            this.btn_uninstallSvc.Text = "卸载服务";
            this.btn_uninstallSvc.UseVisualStyleBackColor = true;
            this.btn_uninstallSvc.Click += new System.EventHandler(this.btn_uninstallSvc_Click);
            // 
            // btn_stopSvc
            // 
            this.btn_stopSvc.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_stopSvc.Location = new System.Drawing.Point(23, 78);
            this.btn_stopSvc.Name = "btn_stopSvc";
            this.btn_stopSvc.Size = new System.Drawing.Size(75, 23);
            this.btn_stopSvc.TabIndex = 0;
            this.btn_stopSvc.Text = "停止服务";
            this.btn_stopSvc.UseVisualStyleBackColor = true;
            this.btn_stopSvc.Click += new System.EventHandler(this.btn_stopSvc_Click);
            // 
            // btn_startSvc
            // 
            this.btn_startSvc.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_startSvc.Location = new System.Drawing.Point(22, 49);
            this.btn_startSvc.Name = "btn_startSvc";
            this.btn_startSvc.Size = new System.Drawing.Size(75, 23);
            this.btn_startSvc.TabIndex = 0;
            this.btn_startSvc.Text = "启动服务";
            this.btn_startSvc.UseVisualStyleBackColor = true;
            this.btn_startSvc.Click += new System.EventHandler(this.btn_startSvc_Click);
            // 
            // btn_installSvc
            // 
            this.btn_installSvc.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_installSvc.Location = new System.Drawing.Point(22, 20);
            this.btn_installSvc.Name = "btn_installSvc";
            this.btn_installSvc.Size = new System.Drawing.Size(75, 23);
            this.btn_installSvc.TabIndex = 0;
            this.btn_installSvc.Text = "安装服务";
            this.btn_installSvc.UseVisualStyleBackColor = true;
            this.btn_installSvc.Click += new System.EventHandler(this.btn_installSvc_Click);
            // 
            // lb_version
            // 
            this.lb_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_version.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_version.Location = new System.Drawing.Point(659, 462);
            this.lb_version.Name = "lb_version";
            this.lb_version.Size = new System.Drawing.Size(117, 20);
            this.lb_version.TabIndex = 0;
            this.lb_version.Text = "1.0.0.0";
            this.lb_version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_author
            // 
            this.lb_author.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_author.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_author.Location = new System.Drawing.Point(659, 440);
            this.lb_author.Name = "lb_author";
            this.lb_author.Size = new System.Drawing.Size(117, 20);
            this.lb_author.TabIndex = 0;
            this.lb_author.Text = "By:侯龙超";
            this.lb_author.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 491);
            this.Controls.Add(this.lb_author);
            this.Controls.Add(this.lb_version);
            this.Controls.Add(this.gbx_serviceTool);
            this.Controls.Add(this.gbx_logTool);
            this.Controls.Add(this.gbx_jobTool);
            this.Controls.Add(this.pl_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(804, 525);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务调度管理 | ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pl_main.ResumeLayout(false);
            this.spl_main.Panel1.ResumeLayout(false);
            this.spl_main.Panel2.ResumeLayout(false);
            this.spl_main.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spl_main)).EndInit();
            this.spl_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).EndInit();
            this.gbx_jobTool.ResumeLayout(false);
            this.gbx_logTool.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_cron;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_preTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_nextTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_enable;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_state;
        private System.Windows.Forms.Label lb_version;
        private System.Windows.Forms.Button btn_clearLog;
        private System.Windows.Forms.Label lb_author;
    }
}


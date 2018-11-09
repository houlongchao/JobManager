namespace HlcJobManager
{
    partial class JobEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobEditForm));
            this.lb_name = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lb_cron = new System.Windows.Forms.Label();
            this.txt_cron = new System.Windows.Forms.TextBox();
            this.lb_type = new System.Windows.Forms.Label();
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.lb_enable = new System.Windows.Forms.Label();
            this.cmb_enable = new System.Windows.Forms.ComboBox();
            this.lb_workPath = new System.Windows.Forms.Label();
            this.txt_workPath = new System.Windows.Forms.TextBox();
            this.lb_className = new System.Windows.Forms.Label();
            this.lb_methodName = new System.Windows.Forms.Label();
            this.dgv_params = new System.Windows.Forms.DataGridView();
            this.cln_param = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.lb_cmd = new System.Windows.Forms.Label();
            this.txt_cmd = new System.Windows.Forms.TextBox();
            this.img_help = new System.Windows.Forms.PictureBox();
            this.btn_selectPath = new System.Windows.Forms.Button();
            this.cmb_classes = new System.Windows.Forms.ComboBox();
            this.cmb_methods = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_params)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_help)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(20, 26);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(65, 12);
            this.lb_name.TabIndex = 0;
            this.lb_name.Text = "任务名称：";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(91, 22);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(162, 21);
            this.txt_name.TabIndex = 1;
            // 
            // lb_cron
            // 
            this.lb_cron.AutoSize = true;
            this.lb_cron.Location = new System.Drawing.Point(20, 53);
            this.lb_cron.Name = "lb_cron";
            this.lb_cron.Size = new System.Drawing.Size(65, 12);
            this.lb_cron.TabIndex = 0;
            this.lb_cron.Text = "调度计划：";
            // 
            // txt_cron
            // 
            this.txt_cron.Location = new System.Drawing.Point(91, 49);
            this.txt_cron.Name = "txt_cron";
            this.txt_cron.Size = new System.Drawing.Size(162, 21);
            this.txt_cron.TabIndex = 3;
            // 
            // lb_type
            // 
            this.lb_type.AutoSize = true;
            this.lb_type.Location = new System.Drawing.Point(278, 26);
            this.lb_type.Name = "lb_type";
            this.lb_type.Size = new System.Drawing.Size(65, 12);
            this.lb_type.TabIndex = 0;
            this.lb_type.Text = "任务类型：";
            // 
            // cmb_type
            // 
            this.cmb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Location = new System.Drawing.Point(349, 22);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Size = new System.Drawing.Size(141, 20);
            this.cmb_type.TabIndex = 2;
            this.cmb_type.SelectedIndexChanged += new System.EventHandler(this.cmb_type_SelectedIndexChanged);
            // 
            // lb_enable
            // 
            this.lb_enable.AutoSize = true;
            this.lb_enable.Location = new System.Drawing.Point(278, 53);
            this.lb_enable.Name = "lb_enable";
            this.lb_enable.Size = new System.Drawing.Size(65, 12);
            this.lb_enable.TabIndex = 0;
            this.lb_enable.Text = "是否启用：";
            // 
            // cmb_enable
            // 
            this.cmb_enable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_enable.FormattingEnabled = true;
            this.cmb_enable.Location = new System.Drawing.Point(349, 49);
            this.cmb_enable.Name = "cmb_enable";
            this.cmb_enable.Size = new System.Drawing.Size(141, 20);
            this.cmb_enable.TabIndex = 4;
            // 
            // lb_workPath
            // 
            this.lb_workPath.AutoSize = true;
            this.lb_workPath.Location = new System.Drawing.Point(20, 80);
            this.lb_workPath.Name = "lb_workPath";
            this.lb_workPath.Size = new System.Drawing.Size(65, 12);
            this.lb_workPath.TabIndex = 0;
            this.lb_workPath.Text = "工作路径：";
            // 
            // txt_workPath
            // 
            this.txt_workPath.Location = new System.Drawing.Point(91, 76);
            this.txt_workPath.Name = "txt_workPath";
            this.txt_workPath.Size = new System.Drawing.Size(366, 21);
            this.txt_workPath.TabIndex = 5;
            // 
            // lb_className
            // 
            this.lb_className.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lb_className.AutoSize = true;
            this.lb_className.Location = new System.Drawing.Point(278, 135);
            this.lb_className.Name = "lb_className";
            this.lb_className.Size = new System.Drawing.Size(41, 12);
            this.lb_className.TabIndex = 0;
            this.lb_className.Text = "类名：";
            // 
            // lb_methodName
            // 
            this.lb_methodName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lb_methodName.AutoSize = true;
            this.lb_methodName.Location = new System.Drawing.Point(278, 167);
            this.lb_methodName.Name = "lb_methodName";
            this.lb_methodName.Size = new System.Drawing.Size(53, 12);
            this.lb_methodName.TabIndex = 0;
            this.lb_methodName.Text = "方法名：";
            // 
            // dgv_params
            // 
            this.dgv_params.AllowUserToResizeColumns = false;
            this.dgv_params.AllowUserToResizeRows = false;
            this.dgv_params.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dgv_params.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_params.BackgroundColor = System.Drawing.Color.White;
            this.dgv_params.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_params.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cln_param});
            this.dgv_params.Location = new System.Drawing.Point(22, 135);
            this.dgv_params.MultiSelect = false;
            this.dgv_params.Name = "dgv_params";
            this.dgv_params.RowHeadersVisible = false;
            this.dgv_params.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgv_params.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_params.RowTemplate.Height = 23;
            this.dgv_params.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_params.Size = new System.Drawing.Size(231, 119);
            this.dgv_params.TabIndex = 0;
            // 
            // cln_param
            // 
            this.cln_param.HeaderText = "参数";
            this.cln_param.Name = "cln_param";
            this.cln_param.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Location = new System.Drawing.Point(415, 230);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 9;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.Location = new System.Drawing.Point(312, 230);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 10;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // lb_cmd
            // 
            this.lb_cmd.AutoSize = true;
            this.lb_cmd.Location = new System.Drawing.Point(20, 107);
            this.lb_cmd.Name = "lb_cmd";
            this.lb_cmd.Size = new System.Drawing.Size(59, 12);
            this.lb_cmd.TabIndex = 0;
            this.lb_cmd.Text = "cmd命令：";
            // 
            // txt_cmd
            // 
            this.txt_cmd.Location = new System.Drawing.Point(91, 103);
            this.txt_cmd.Name = "txt_cmd";
            this.txt_cmd.Size = new System.Drawing.Size(399, 21);
            this.txt_cmd.TabIndex = 6;
            // 
            // img_help
            // 
            this.img_help.Image = global::HlcJobManager.Properties.Resources.help_16px;
            this.img_help.Location = new System.Drawing.Point(254, 51);
            this.img_help.Name = "img_help";
            this.img_help.Size = new System.Drawing.Size(16, 16);
            this.img_help.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.img_help.TabIndex = 11;
            this.img_help.TabStop = false;
            // 
            // btn_selectPath
            // 
            this.btn_selectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_selectPath.Location = new System.Drawing.Point(463, 76);
            this.btn_selectPath.Name = "btn_selectPath";
            this.btn_selectPath.Size = new System.Drawing.Size(27, 23);
            this.btn_selectPath.TabIndex = 10;
            this.btn_selectPath.TabStop = false;
            this.btn_selectPath.Text = "..";
            this.btn_selectPath.UseVisualStyleBackColor = true;
            this.btn_selectPath.Click += new System.EventHandler(this.btn_selectPath_Click);
            // 
            // cmb_classes
            // 
            this.cmb_classes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmb_classes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_classes.FormattingEnabled = true;
            this.cmb_classes.Location = new System.Drawing.Point(349, 132);
            this.cmb_classes.Name = "cmb_classes";
            this.cmb_classes.Size = new System.Drawing.Size(141, 20);
            this.cmb_classes.TabIndex = 7;
            this.cmb_classes.SelectedIndexChanged += new System.EventHandler(this.cmb_classes_SelectedIndexChanged);
            // 
            // cmb_methods
            // 
            this.cmb_methods.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmb_methods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_methods.FormattingEnabled = true;
            this.cmb_methods.Location = new System.Drawing.Point(349, 164);
            this.cmb_methods.Name = "cmb_methods";
            this.cmb_methods.Size = new System.Drawing.Size(141, 20);
            this.cmb_methods.TabIndex = 8;
            // 
            // JobEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 265);
            this.Controls.Add(this.cmb_methods);
            this.Controls.Add(this.cmb_classes);
            this.Controls.Add(this.img_help);
            this.Controls.Add(this.btn_selectPath);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.dgv_params);
            this.Controls.Add(this.cmb_enable);
            this.Controls.Add(this.cmb_type);
            this.Controls.Add(this.txt_cmd);
            this.Controls.Add(this.txt_workPath);
            this.Controls.Add(this.txt_cron);
            this.Controls.Add(this.lb_enable);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.lb_cmd);
            this.Controls.Add(this.lb_methodName);
            this.Controls.Add(this.lb_workPath);
            this.Controls.Add(this.lb_className);
            this.Controls.Add(this.lb_type);
            this.Controls.Add(this.lb_cron);
            this.Controls.Add(this.lb_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JobEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "任务调度编辑";
            this.Load += new System.EventHandler(this.JobEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_params)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_help)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lb_cron;
        private System.Windows.Forms.TextBox txt_cron;
        private System.Windows.Forms.Label lb_type;
        private System.Windows.Forms.ComboBox cmb_type;
        private System.Windows.Forms.Label lb_enable;
        private System.Windows.Forms.ComboBox cmb_enable;
        private System.Windows.Forms.Label lb_workPath;
        private System.Windows.Forms.TextBox txt_workPath;
        private System.Windows.Forms.Label lb_className;
        private System.Windows.Forms.Label lb_methodName;
        private System.Windows.Forms.DataGridView dgv_params;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_param;
        private System.Windows.Forms.Label lb_cmd;
        private System.Windows.Forms.TextBox txt_cmd;
        private System.Windows.Forms.PictureBox img_help;
        private System.Windows.Forms.Button btn_selectPath;
        private System.Windows.Forms.ComboBox cmb_classes;
        private System.Windows.Forms.ComboBox cmb_methods;
    }
}
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
            resources.ApplyResources(this.lb_name, "lb_name");
            this.lb_name.Name = "lb_name";
            // 
            // txt_name
            // 
            resources.ApplyResources(this.txt_name, "txt_name");
            this.txt_name.Name = "txt_name";
            // 
            // lb_cron
            // 
            resources.ApplyResources(this.lb_cron, "lb_cron");
            this.lb_cron.Name = "lb_cron";
            // 
            // txt_cron
            // 
            resources.ApplyResources(this.txt_cron, "txt_cron");
            this.txt_cron.Name = "txt_cron";
            // 
            // lb_type
            // 
            resources.ApplyResources(this.lb_type, "lb_type");
            this.lb_type.Name = "lb_type";
            // 
            // cmb_type
            // 
            resources.ApplyResources(this.cmb_type, "cmb_type");
            this.cmb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.SelectedIndexChanged += new System.EventHandler(this.cmb_type_SelectedIndexChanged);
            // 
            // lb_enable
            // 
            resources.ApplyResources(this.lb_enable, "lb_enable");
            this.lb_enable.Name = "lb_enable";
            // 
            // cmb_enable
            // 
            resources.ApplyResources(this.cmb_enable, "cmb_enable");
            this.cmb_enable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_enable.FormattingEnabled = true;
            this.cmb_enable.Name = "cmb_enable";
            // 
            // lb_workPath
            // 
            resources.ApplyResources(this.lb_workPath, "lb_workPath");
            this.lb_workPath.Name = "lb_workPath";
            // 
            // txt_workPath
            // 
            resources.ApplyResources(this.txt_workPath, "txt_workPath");
            this.txt_workPath.Name = "txt_workPath";
            this.txt_workPath.ReadOnly = true;
            this.txt_workPath.TabStop = false;
            // 
            // lb_className
            // 
            resources.ApplyResources(this.lb_className, "lb_className");
            this.lb_className.Name = "lb_className";
            // 
            // lb_methodName
            // 
            resources.ApplyResources(this.lb_methodName, "lb_methodName");
            this.lb_methodName.Name = "lb_methodName";
            // 
            // dgv_params
            // 
            resources.ApplyResources(this.dgv_params, "dgv_params");
            this.dgv_params.AllowUserToResizeColumns = false;
            this.dgv_params.AllowUserToResizeRows = false;
            this.dgv_params.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_params.BackgroundColor = System.Drawing.Color.White;
            this.dgv_params.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_params.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cln_param});
            this.dgv_params.MultiSelect = false;
            this.dgv_params.Name = "dgv_params";
            this.dgv_params.RowHeadersVisible = false;
            this.dgv_params.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgv_params.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_params.RowTemplate.Height = 23;
            this.dgv_params.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // cln_param
            // 
            resources.ApplyResources(this.cln_param, "cln_param");
            this.cln_param.Name = "cln_param";
            this.cln_param.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btn_cancel
            // 
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            resources.ApplyResources(this.btn_ok, "btn_ok");
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // lb_cmd
            // 
            resources.ApplyResources(this.lb_cmd, "lb_cmd");
            this.lb_cmd.Name = "lb_cmd";
            // 
            // txt_cmd
            // 
            resources.ApplyResources(this.txt_cmd, "txt_cmd");
            this.txt_cmd.Name = "txt_cmd";
            // 
            // img_help
            // 
            resources.ApplyResources(this.img_help, "img_help");
            this.img_help.Image = global::HlcJobManager.Properties.Resources.help_16px;
            this.img_help.Name = "img_help";
            this.img_help.TabStop = false;
            // 
            // btn_selectPath
            // 
            resources.ApplyResources(this.btn_selectPath, "btn_selectPath");
            this.btn_selectPath.Name = "btn_selectPath";
            this.btn_selectPath.UseVisualStyleBackColor = true;
            this.btn_selectPath.Click += new System.EventHandler(this.btn_selectPath_Click);
            // 
            // cmb_classes
            // 
            resources.ApplyResources(this.cmb_classes, "cmb_classes");
            this.cmb_classes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_classes.FormattingEnabled = true;
            this.cmb_classes.Name = "cmb_classes";
            this.cmb_classes.SelectedIndexChanged += new System.EventHandler(this.cmb_classes_SelectedIndexChanged);
            // 
            // cmb_methods
            // 
            resources.ApplyResources(this.cmb_methods, "cmb_methods");
            this.cmb_methods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_methods.FormattingEnabled = true;
            this.cmb_methods.Name = "cmb_methods";
            // 
            // JobEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "JobEditForm";
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
        private System.Windows.Forms.Label lb_cmd;
        private System.Windows.Forms.TextBox txt_cmd;
        private System.Windows.Forms.PictureBox img_help;
        private System.Windows.Forms.Button btn_selectPath;
        private System.Windows.Forms.ComboBox cmb_classes;
        private System.Windows.Forms.ComboBox cmb_methods;
        private System.Windows.Forms.DataGridViewTextBoxColumn cln_param;
    }
}
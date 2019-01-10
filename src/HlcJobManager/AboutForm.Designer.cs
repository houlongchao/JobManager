namespace HlcJobManager
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.label1 = new System.Windows.Forms.Label();
            this.link_github = new System.Windows.Forms.LinkLabel();
            this.link_gitee = new System.Windows.Forms.LinkLabel();
            this.lb_title = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目地址：";
            // 
            // link_github
            // 
            this.link_github.AutoSize = true;
            this.link_github.Location = new System.Drawing.Point(49, 231);
            this.link_github.Name = "link_github";
            this.link_github.Size = new System.Drawing.Size(251, 12);
            this.link_github.TabIndex = 1;
            this.link_github.TabStop = true;
            this.link_github.Text = "https://github.com/houlongchao/JobManager";
            this.link_github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_github_LinkClicked);
            // 
            // link_gitee
            // 
            this.link_gitee.AutoSize = true;
            this.link_gitee.Location = new System.Drawing.Point(51, 257);
            this.link_gitee.Name = "link_gitee";
            this.link_gitee.Size = new System.Drawing.Size(149, 12);
            this.link_gitee.TabIndex = 2;
            this.link_gitee.TabStop = true;
            this.link_gitee.Text = "https://gitee.com/hlc/jm";
            this.link_gitee.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_gitee_LinkClicked);
            // 
            // lb_title
            // 
            this.lb_title.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(307, 59);
            this.lb_title.TabIndex = 3;
            this.lb_title.Text = "任务调度管理工具";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "作者：";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(40, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(242, 63);
            this.label3.TabIndex = 5;
            this.label3.Text = "可定时执行exe/dll/cmd\r\n可以服务方式后台托管exe/dll/cmd";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(51, 167);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(131, 12);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "侯龙超 hlc2015@qq.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_mailTo);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 286);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.link_gitee);
            this.Controls.Add(this.link_github);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.Text = "关于";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel link_github;
        private System.Windows.Forms.LinkLabel link_gitee;
        private System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
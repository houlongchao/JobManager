using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HlcJobManager
{
    public partial class AboutForm : Form
    {
        private static AboutForm _instance;

        public AboutForm()
        {
            InitializeComponent();
        }

        public new static void Show()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new AboutForm();
            }
            _instance.StartPosition = FormStartPosition.CenterScreen;
            ((Control) _instance).Show();
        }

        private void link_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/houlongchao/JobManager");
        }

        private void link_gitee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://gitee.com/hlc/jm");
        }

        private void link_mailTo(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto://hlc2015@qq.com");
        }
    }
}

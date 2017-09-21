using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void tsmiSetting_Click(object sender, EventArgs e)
        {
            FormConfig frmSettings = new FormConfig();
            frmSettings.ShowDialog();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            FormAbout frmAbout = new FormAbout();
            frmAbout.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dgvUserInfo.Rows.Add("1", "0000001", "sdfasdf", "已绑定", "张三", "13801231231", "2017-08-19 22:01:11", "编辑");
            dgvUserInfo.Rows.Add("1", "0000001", "sdfasdf", "已绑定", "张三", "13801231231", "2017-08-19 22:01:11", "编辑");
            dgvUserInfo.Rows.Add("1", "0000001", "sdfasdf", "已绑定", "张三", "13801231231", "2017-08-19 22:01:11", "编辑");
            dgvUserInfo.Rows.Add("1", "0000001", "sdfasdf", "已绑定", "张三", "13801231231", "2017-08-19 22:01:11", "编辑");
            dgvUserInfo.Rows.Add("1", "0000001", "sdfasdf", "已绑定", "张三", "13801231231", "2017-08-19 22:01:11", "编辑");
            dgvUserInfo.Rows.Add("1", "0000001", "sdfasdf", "已绑定", "张三", "13801231231", "2017-08-19 22:01:11", "编辑");
        }
    }
}

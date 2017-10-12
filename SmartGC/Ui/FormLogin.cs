using SmartGC.Lib;
using System;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Configs.SaveUserEnable)
            {
                tbxUserName.Text = Configs.LastUser;
                cbxSaveUser.Checked = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (Common.Login(tbxUserName.Text.Trim(), tbxPassword.Text, out msg))
            {
                this.Hide();
                FormMain main = new FormMain();
                main.Show();
                if (cbxSaveUser.Checked)
                {
                    Configs.LastUser = tbxUserName.Text.Trim();
                }
            }
            else
            {
                MessageBox.Show("登录失败:" + msg);
            }
        }
    }
}

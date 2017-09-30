using SmartGC.Lib;
using System;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            tbxServer.Text = Configs.Server;
            tbxToken.Text = Configs.Token;
            cbxLogenable.Checked = Configs.LogEnable;
            cbxExpect100Continue.Checked = Configs.Expect100Continue;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Configs.Server = tbxServer.Text;
            Configs.Token = tbxToken.Text;
            Configs.LogEnable = cbxLogenable.Checked;
            Configs.Expect100Continue = cbxExpect100Continue.Checked;
            Configs.Save();
            this.Close();
        }
    }
}

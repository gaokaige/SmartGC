using SmartGC.Lib;
using System;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormWriteCard : Form
    {
        CardApi api;
        public FormWriteCard(CardApi _api)
        {
            InitializeComponent();
            api = _api;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            string str = tbxCardNo.Text;
            byte[] data = new byte[16];
            string tmp;
            int j = 0;
            for (int i = 0; i < 16; i++)
            {
                tmp = str.Substring(j, 2);
                data[i] = (byte)Convert.ToInt32(tmp, 16);
                j = j + 2;
            }
            if (api.WriteCard(data))
            {
                MessageBox.Show("写卡成功");
            }
            else
            {
                MessageBox.Show("写卡失败，请重新启动软件再次尝试写入");
            }
            this.Close();
        }
    }
}

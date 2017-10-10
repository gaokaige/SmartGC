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
    public partial class FormExchange : Form
    {
        DataRow row;
        public FormExchange(DataRow _row)
        {
            InitializeComponent();
            row = _row;
        }
        int score = 0;
        string gid;
        private void FormExchange_Load(object sender, EventArgs e)
        {
            score = int.Parse(row["Score"].ToString());
            gid = row["id"].ToString();
            tbxCommodity.Text = row["Name"].ToString();
            tbxTotalScore.Text = tbxScorePer.Text = row["Score"].ToString();
            tbxScoreHave.Text = "";
        }

        private void tbxCount_TextChanged(object sender, EventArgs e)
        {
            int value;

            if (!int.TryParse(tbxCount.Text, out value))
            {
                tbxCount.Text = "1";
                tbxTotalScore.Text = tbxScorePer.Text;
                MessageBox.Show("请输入数字");
                return;
            }
            else
            {
                tbxTotalScore.Text = (value * score).ToString();
            }
        }

        private void btnExchage_Click(object sender, EventArgs e)
        {

        }
    }
}

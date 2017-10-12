using SmartGC.Lib;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormExchange : Form
    {
        DataRow row;
        CardApi api;
        bool stop = false;
        int merchantID;//商户id
        int score = 0;//兑换单个的积分
        int merchantCredit;//商户拥有的积分
        string gid;

        public FormExchange(DataRow _row, CardApi _api)
        {
            InitializeComponent();
            api = _api;
            row = _row;
            api.OnConnOK += lib_OnConnOK;
            api.OnDisConn += lib_OnDisConn;
            api.OnSendMessage += lib_OnSendMessage;
            api.OnReadCardNo += lib_OnReadCardNo;

            if (!FormMain.connDev)
            {
                api.ConnectUsbDev();
            }
            Thread th = new Thread(new ThreadStart(ReadCard));
            th.Start();
        }

        private void lib_OnSendMessage(string msg)
        {

        }

        private void lib_OnDisConn()
        {

        }

        private void lib_OnConnOK()
        {
            FormMain.connDev = true;
        }

        private void lib_OnReadCardNo(string cardNo)
        {
            if (tbxCardNo.InvokeRequired)
            {
                tbxCardNo.Invoke(new SmartGC.Lib.CardApi.ReadCardNoHandler(lib_OnReadCardNo), new object[] { cardNo });
            }
            else
            {
                stop = true;
                tbxCardNo.Text = cardNo;
                DataTable dt = Common.GetMerchantByCardNo(cardNo);
                if (dt.Rows.Count > 0)
                {
                    merchantID = Int32.Parse(dt.Rows[0]["id"].ToString());
                    merchantCredit = Int32.Parse(dt.Rows[0]["credit"].ToString());
                }
            }
        }

        void ReadCard()
        {
            while (!stop)
            {
                api.GetCardNo();
                Thread.Sleep(500);
            }
        }

        private void FormExchange_Load(object sender, EventArgs e)
        {
            score = int.Parse(row["Score"].ToString());
            gid = row["id"].ToString();
            tbxCommodity.Text = row["Name"].ToString();
            tbxTotalScore.Text = tbxScorePer.Text = row["Score"].ToString();
            tbxScoreHave.Text = merchantCredit.ToString();
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
            
            int count = int.Parse(tbxCount.Text);
            string msg = string.Empty;
            if (Common.Exchage(merchantID, count, gid, out msg))
            {
                MessageBox.Show("兑换成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("兑换失败:" + msg);
            }
        }

        private void btnReadCard_Click(object sender, EventArgs e)
        {
            stop = false;
            Thread th = new Thread(new ThreadStart(api.GetCardNo));
            th.Start();
        }
    }
}

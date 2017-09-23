using SmartGC.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormMain : Form
    {
        DataTable dtCardInfo, dtCommodityInfo;
        ReadCardLib lib = new ReadCardLib();
        bool connDev = false;
        public FormMain()
        {
            InitializeComponent();
            dgvCardInfo.AutoGenerateColumns = false;
            dgvCommodity.AutoGenerateColumns = false;

            lib.OnConnOK += lib_OnConnOK;
            lib.OnDisConn += lib_OnDisConn;
            lib.OnSendMessage += lib_OnSendMessage;
            lib.OnReadCardNo += lib_OnReadCardNo;
        }

        private void lib_OnDisConn()
        {
            connDev = false;
        }

        private void lib_OnConnOK()
        {
            connDev = true;
        }

        private void lib_OnSendMessage(string msg)
        {
            if (lbMessage.InvokeRequired)
            {
                lbMessage.Invoke(new SmartGC.Lib.ReadCardLib.SendMessageHandler(lib_OnSendMessage), new object[] { msg });
            }
            else
            {
                lbMessage.Text = msg;
            } 
        }

        private void lib_OnReadCardNo(string cardNo)
        {
            if (tbxCardNo.InvokeRequired)
            {
                tbxCardNo.Invoke(new SmartGC.Lib.ReadCardLib.ReadCardNoHandler(lib_OnReadCardNo), new object[] { cardNo });
            }
            else
            {
                tbxCardNo.Text = cardNo;
            } 
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
            if (connDev)
                lib.DisconnectUsbDev();
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            pagerCardInfo.OnPageChanged += new EventHandler(pagerCardInfo_OnPageChanged);
            LoadCardInfo();
        }

        private void pagerCardInfo_OnPageChanged(object sender, EventArgs e)
        {
            LoadCardInfo();
        }

        private void LoadCardInfo()
        {
            dtCardInfo = Common.GetCardInfoData("json");

            DataTable dt = dtCardInfo.Clone();

            DataRow[] rows = dtCardInfo.Select("Index>" + (pagerCardInfo.PageIndex - 1) * pagerCardInfo.PageSize + "and Index<=" + pagerCardInfo.PageIndex * pagerCardInfo.PageSize);

            foreach (DataRow row in rows)
            {
                dt.Rows.Add(row.ItemArray);
            }

            dgvCardInfo.DataSource = dt;
            pagerCardInfo.DrawControl(dtCardInfo.Rows.Count);
        }

        private void dgvCardInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                DataTable dt = dtCardInfo.Clone();
                DataRow[] rows = dtCardInfo.Select("Index=" + dgvCardInfo.Rows[e.RowIndex].Cells[0].Value);
                FormCardInfo frm = new FormCardInfo(rows[0]);
                frm.ShowDialog();
            }
        }

        private void tsmiConnect_Click(object sender, EventArgs e)
        {
            lib.ConnectUsbDev();
        }

        private void tsmiDisconnect_Click(object sender, EventArgs e)
        {
            lib.DisconnectUsbDev();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!connDev)
                lib.ConnectUsbDev();
            lib.GetCardNo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CardInfo card = new CardInfo();
            card.CardNo = tbxCardNo.Text;
        }
    }
}

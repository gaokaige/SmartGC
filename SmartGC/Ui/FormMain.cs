using SmartGC.Lib;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormMain : Form
    {
        #region 变量定义

        /// <summary>
        /// 卡片信息表
        /// </summary>
        DataTable dtCardInfo;
        /// <summary>
        /// 兑换品信息表
        /// </summary>
        DataTable dtCommodityInfo;
        /// <summary>
        /// 读卡器操作实例
        /// </summary>
        CardApi api = new CardApi();
        /// <summary>
        /// 读卡器连接状态
        /// </summary>
        bool connDev = false;
        CardInfo card;
        string cardNoCache;
        #endregion

        #region 窗体事件
        public FormMain()
        {
            InitializeComponent();
            // 定义事件
            api.OnConnOK += lib_OnConnOK;
            api.OnDisConn += lib_OnDisConn;
            api.OnSendMessage += lib_OnSendMessage;
            api.OnReadCardNo += lib_OnReadCardNo;
            // 禁止表格自动获取内容
            dgvCardInfo.AutoGenerateColumns = false;
            dgvCommodity.AutoGenerateColumns = false;
            // 设置页面记录数
            pagerCardInfo.PageSize = Configs.Pagesize;
            pagerCommodity.PageSize = Configs.Pagesize;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            pagerCardInfo.OnPageChanged += new EventHandler(pagerCardInfo_OnPageChanged);
            tbxCardNo.Text = "CA0010000EEEFEEFEFECC0000000000F";
        }

        /// <summary>
        /// 退出软件发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connDev)
                api.DisconnectUsbDev();
            Application.Exit();
        }
        #endregion

        #region 读卡器事件
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
                lbMessage.Invoke(new SmartGC.Lib.CardApi.SendMessageHandler(lib_OnSendMessage), new object[] { msg });
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
                tbxCardNo.Invoke(new SmartGC.Lib.CardApi.ReadCardNoHandler(lib_OnReadCardNo), new object[] { cardNo });
            }
            else
            {
                tbxCardNo.Text = cardNo;
                btnSearch_Click(null, null);
            }
        }
        #endregion

        #region 界面按钮事件

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSetting_Click(object sender, EventArgs e)
        {
            FormConfig frmSettings = new FormConfig();
            frmSettings.ShowDialog();
        }

        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            FormAbout frmAbout = new FormAbout();
            frmAbout.ShowDialog();
        }

        /// <summary>
        /// 读卡按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!connDev)
                api.ConnectUsbDev();
            Thread th = new Thread(new ThreadStart(api.GetCardNo));
            th.Start();
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            card = new CardInfo();
            card.CardNo = tbxCardNo.Text;
            card.Customer = tbxCustomer.Text;
            card.PhoneNo = tbxPhoneNo.Text;
            LoadCardInfo(card);
        }

        /// <summary>
        /// 连接读卡器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiConnect_Click(object sender, EventArgs e)
        {
            if (!connDev)
            {
                //lib.ConnectUsbDev();
                Thread th = new Thread(new ThreadStart(api.ConnectUsbDev));
                th.Start();
            }
        }

        /// <summary>
        /// 断开读卡器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDisconnect_Click(object sender, EventArgs e)
        {
            if (connDev)
            {
                Thread th = new Thread(new ThreadStart(api.DisconnectUsbDev));
                th.Start();
            }
        }
        /// <summary>
        /// 写卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiWriteCard_Click(object sender, EventArgs e)
        {
            FormWriteCard frm = new FormWriteCard(api);
            frm.ShowDialog();
        }
        
        private void tbxCardNo_DoubleClick(object sender, EventArgs e)
        {
            if (tbxCardNo.Text != string.Empty)
            {
                cardNoCache = tbxCardNo.Text;
                tbxCardNo.Text = "";
            }
            else
                tbxCardNo.Text = cardNoCache;
        }

        #endregion

        #region 表格操作

        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerCardInfo_OnPageChanged(object sender, EventArgs e)
        {
            LoadCardInfo(card);
        }

        /// <summary>
        /// 卡片信息数据绑定
        /// </summary>
        private void LoadCardInfo(CardInfo cardInfo)
        {
            int total = 0;
            if (cardInfo.CardNo != string.Empty)
            {
                dtCardInfo = Common.GetCardInfoByCardNo(cardInfo.CardNo);
                total = dtCardInfo.Rows.Count;
            }
            else
            {
                dtCardInfo = Common.GetCardInfoList(cardInfo, pagerCardInfo.PageSize, pagerCardInfo.PageIndex ,out total);
            }

            dgvCardInfo.DataSource = dtCardInfo;
            pagerCardInfo.DrawControl(total);

            if (cardInfo.CardNo != string.Empty && dtCardInfo.Rows.Count == 0)
            {
                // 开卡
                CreateAccount();
                return;
            }
            //dtCardInfo = Common.GetCardInfoByCardNo()
            /*
            dtCardInfo = Common.GetCardInfoByCardNo(cardInfo);

            if (dtCardInfo.Rows.Count == 0)
            { 
                // 开卡
                CreateAccount(cardInfo);
                return;
            }

            DataTable dt = dtCardInfo.Clone();

            DataRow[] rows = dtCardInfo.Select("Index>" + (pagerCardInfo.PageIndex - 1) * pagerCardInfo.PageSize + "and Index<=" + pagerCardInfo.PageIndex * pagerCardInfo.PageSize);

            foreach (DataRow row in rows)
            {
                dt.Rows.Add(row.ItemArray);
            }

            dgvCardInfo.DataSource = dt;
            pagerCardInfo.DrawControl(dtCardInfo.Rows.Count);
             */
        }
        private void pagerCommodity_OnPageChanged(object sender, EventArgs e)
        {
            LoadCommodity();
        }

        private void LoadCommodity()
        {
            int total = 0;
            dtCommodityInfo = Common.GetCommodityInfoList(pagerCommodity.PageSize, pagerCommodity.PageIndex, out total);
            dgvCommodity.DataSource = dtCommodityInfo;
            pagerCommodity.DrawControl(total);
        }
        private void CreateAccount()
        {
            FormCardInfo frm = new FormCardInfo(tbxCardNo.Text);
            frm.ShowDialog();
        }

        /// <summary>
        /// 表格操作事件，比如：编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCardInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 点击编辑
            if (dgvCardInfo.Columns[e.ColumnIndex].HeaderText == "操作")
            {
                DataTable dt = dtCardInfo.Clone();
                DataRow[] rows = dtCardInfo.Select("Index=" + dgvCardInfo.Rows[e.RowIndex].Cells[0].Value);
                FormCardInfo frm = new FormCardInfo(rows[0]);
                frm.ShowDialog();
            }
        }

        #endregion

        private void dgvCommodity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 点击编辑
            if (dgvCardInfo.Columns[e.ColumnIndex].HeaderText == "兑换")
            {
                DataTable dt = dtCommodityInfo.Clone();
                DataRow[] rows = dtCommodityInfo.Select("Index=" + dgvCardInfo.Rows[e.RowIndex].Cells[0].Value);
                FormExchange frm = new FormExchange(rows[0]);
                frm.ShowDialog();
            }
        }
    }
}
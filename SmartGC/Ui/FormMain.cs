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
        /// 商户信息表
        /// </summary>
        DataTable dtMerchant;
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
        public static bool connDev = false;
        /// <summary>
        /// 商户信息对象
        /// </summary>
        Merchant merchant;
        /// <summary>
        /// 卡号缓存
        /// </summary>
        string cardNoCache;
        /// <summary>
        /// 事件暂停开关，防止与其他窗体的事件冲突
        /// </summary>
        bool eventPause = false;
        #endregion

        #region 窗体事件
        public FormMain()
        {
            InitializeComponent();
            // 定义事件
            api.OnConnOK += lib_OnConnOK;
            api.OnDisConn += lib_OnDisConn;
            api.OnSendMessage += lib_OnSendMessage;
            api.OnReadCard += lib_OnReadCardNo;
            // 禁止表格自动获取内容
            dgvMerchant.AutoGenerateColumns = false;
            dgvCommodity.AutoGenerateColumns = false;
            // 设置页面记录数
            pagerMerchant.PageSize = Configs.Pagesize;
            pagerCommodity.PageSize = Configs.Pagesize;
            // 读取商品列表
            LoadCommodity();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            pagerMerchant.OnPageChanged += new EventHandler(pagerMerchant_OnPageChanged);
#if DEBUG
            tbxCardNo.Text = "00000000000000000000000000000000";
#endif
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
            Configs.Save();
            Application.Exit();
        }
        #endregion

        #region 读卡器事件
        private void lib_OnDisConn()
        {
            if (eventPause)
                return;
            connDev = false;
        }

        private void lib_OnConnOK()
        {
            if (eventPause)
                return;
            connDev = true;
        }

        private void lib_OnSendMessage(string msg)
        {
            if (eventPause)
                return;
            if (lbMessage.InvokeRequired)
            {
                lbMessage.Invoke(new SmartGC.Lib.CardApi.SendMessageHandler(lib_OnSendMessage), new object[] { msg });
            }
            else
            {
                lbMessage.Text = msg;
            }
        }

        private void lib_OnReadCardNo(string cardNo, string cardId)
        {
            if (eventPause)
                return;
            if (tbxCardId.InvokeRequired)
            {
                tbxCardId.Invoke(new SmartGC.Lib.CardApi.ReadCardHandler(lib_OnReadCardNo), new object[] { cardNo, cardId });
            }
            else
            {
                tbxCardId.Text = cardId;
                tbxCardId.Tag = cardNo;
                if (cardId == "0000000000")
                {
                    // 开户+绑定界面
                    CreateAccount();
                }
                else
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
            Thread th = new Thread(new ThreadStart(api.GetCardNoAndID));
            th.Start();
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            lbMessage.Text = "查询中...";
            lbMessage.Refresh();
            merchant = new Merchant();
            merchant.CardID = tbxCardId.Text;
            merchant.Name = tbxCustomer.Text;
            merchant.PhoneNo = tbxPhoneNo.Text;
            if (combStatus.SelectedIndex != -1)
            {
                if (combStatus.Items[combStatus.SelectedIndex].ToString() == "未绑定")
                    merchant.Status = CardBindingStatus.N;
                else if (combStatus.Items[combStatus.SelectedIndex].ToString() == "绑定")
                    merchant.Status = CardBindingStatus.Y;
                else
                    merchant.Status = CardBindingStatus.X;
            }
            else
                merchant.Status = CardBindingStatus.X;

            LoadMerchant(merchant);
            lbMessage.Text = "查询完毕";
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
            if (tbxCardId.Text != string.Empty)
            {
                cardNoCache = tbxCardId.Text;
                tbxCardId.Text = "";
            }
            else
                tbxCardId.Text = cardNoCache;
        }

        #endregion

        #region 表格操作

        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerMerchant_OnPageChanged(object sender, EventArgs e)
        {
            LoadMerchant(merchant);
        }

        /// <summary>
        /// 卡片信息数据绑定
        /// </summary>
        private void LoadMerchant(Merchant merchant)
        {
            try
            {
                int total = 0;
                if (merchant.CardID != string.Empty)// 卡号不为空时
                {
                    dtMerchant = Common.GetMerchantByCardNo(merchant.CardID);
                    total = dtMerchant.Rows.Count;
                }
                else
                {
                    dtMerchant = Common.GetMerchantList(merchant, pagerMerchant.PageSize, pagerMerchant.PageIndex, out total);
                }

                dgvMerchant.DataSource = dtMerchant;
                pagerMerchant.DrawControl(total);
                if (dtMerchant.Rows.Count == 0)
                {
                    MessageBox.Show(string.Format("卡号{0}查无商户信息. 请同客服联系", merchant.CardID));
                }
            }
            catch
            {
                MessageBox.Show("读取商户信息失败");
            }
        }
        private void pagerCommodity_OnPageChanged(object sender, EventArgs e)
        {
            LoadCommodity();
        }

        private void LoadCommodity()
        {
            try
            {
                int total = 0;
                dtCommodityInfo = Common.GetCommodityInfoList(pagerCommodity.PageSize, pagerCommodity.PageIndex, out total);
                dgvCommodity.DataSource = dtCommodityInfo;
                pagerCommodity.DrawControl(total);
            }
            catch
            {
                MessageBox.Show("读取商品列表失败");
            }
        }
        /// <summary>
        /// 开户：新增商户+绑定IC卡
        /// </summary>
        private void CreateAccount()
        {
            eventPause = true;
            FormMerchant frm = new FormMerchant(tbxCardId.Text, tbxCardId.Tag, this, api);
            frm.ShowDialog();
            eventPause = false;
        }

        /// <summary>
        /// 表格操作事件:编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCardInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 点击编辑
            if (dgvMerchant.Columns[e.ColumnIndex].HeaderText == "操作")
            {
                DataTable dt = dtMerchant.Clone();
                DataRow[] rows = dtMerchant.Select("Index=" + dgvMerchant.Rows[e.RowIndex].Cells[0].Value);
                eventPause = true;
                FormMerchant frm = new FormMerchant(rows[0], api, this);
                frm.ShowDialog();
                eventPause = false;
            }
        }

        #endregion
        /// <summary>
        /// 表格操作事件:兑换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommodity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 点击编辑
            if (dgvCommodity.Columns[e.ColumnIndex].HeaderText == "兑换")
            {
                DataTable dt = dtCommodityInfo.Clone();
                DataRow[] rows = dtCommodityInfo.Select("Index=" + dgvCommodity.Rows[e.RowIndex].Cells[0].Value);
                eventPause = true;
                FormExchange frm = new FormExchange(rows[0], api);
                frm.ShowDialog();
                eventPause = false;
            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void UpdateRefesh()
        {
            btnSearch_Click(null, null);
        }
        /// <summary>
        /// 刷新商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRefreshCommodity_Click(object sender, EventArgs e)
        {
            LoadCommodity();
        }
    }
}
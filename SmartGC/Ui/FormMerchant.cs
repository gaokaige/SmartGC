﻿using SmartGC.Lib;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SmartGC.Ui
{
    public partial class FormMerchant : Form
    {
        DataRow row = null;
        string cardNo = string.Empty;
        int merchantID;
        Operation op;
        CardApi api;
        FormMain formMain;
        /// <summary>
        /// 事件暂停开关，防止与其他窗体的事件冲突
        /// </summary>
        bool eventPause = false;
        /// <summary>
        /// 开卡
        /// </summary>
        /// <param name="_cardNo"></param>
        public FormMerchant(string _cardNo, FormMain _formMain)
        {
            InitializeComponent();
            formMain = _formMain;
            cardNo = _cardNo;
            label1.Visible = true;
            btnRead.Enabled = false;
            btnUnBinding.Enabled = false;
            btnBinding.Enabled = false;
            op = Operation.开卡;
        }
        /// <summary>
        /// 编辑(包括绑定与解绑)
        /// </summary>
        /// <param name="_row"></param>
        public FormMerchant(DataRow _row, CardApi _api, FormMain _formMain)
        {
            InitializeComponent();
            formMain = _formMain;
            api = _api;
            api.OnConnOK += lib_OnConnOK;
            api.OnDisConn += lib_OnDisConn;
            api.OnSendMessage += lib_OnSendMessage;
            api.OnReadCardNo += lib_OnReadCardNo;

            row = _row;
            merchantID = int.Parse(_row["cardid"].ToString());
            label1.Visible = false;
            btnRead.Enabled = true;
            btnUnBinding.Enabled = true;
            btnBinding.Enabled = true;
            op = Operation.编辑;

            if (!FormMain.connDev)
            {
                api.ConnectUsbDev();
            }
        }

        private void lib_OnReadCardNo(string cardNo)
        {
            if (eventPause)
                return;
            DataTable dt = Common.GetMerchantByCardNo(cardNo);
            if (dt.Rows.Count != 0)
            {
                MessageBox.Show(string.Format("该卡已经绑定了一个商户【{0}】,不能再次绑定", dt.Rows[0]["Customer"].ToString()));
                return;
            }
            if (tbxCardNo.InvokeRequired)
            {
                tbxCardNo.Invoke(new SmartGC.Lib.CardApi.ReadCardNoHandler(lib_OnReadCardNo), new object[] { cardNo });
            }
            else
            {
                tbxCardNo.Text = cardNo;
            }
        }

        private void lib_OnSendMessage(string msg)
        {
            if (eventPause)
                return;
        }

        private void lib_OnDisConn()
        {
            if (eventPause)
                return;
        }

        private void lib_OnConnOK()
        {
            if (eventPause)
                return;
            FormMain.connDev = true;
        }

        private void FormCardBinding_Load(object sender, EventArgs e)
        {
            if (op == Operation.编辑)
            {
                tbxCardNo.Text = row["CardNo"].ToString();// IC卡编号
                tbxCustomer.Text = row["Customer"].ToString(); ;// 商户名称
                tbxAddress.Text = row["Address"].ToString(); ;// 地址
                tbxPersonInCharge.Text = row["PersonInCharge"].ToString(); ;// 负责人
                tbxPhoneNo.Text = row["PhoneNo"].ToString(); ;// 手机号
                tbxCreateDate.Text = row["CreateDate"].ToString(); ;// 开卡时间
                tbxRemark.Text = row["Remark"].ToString(); ;// 备注

                RefreshButtonStatus();
            }

            if (op == Operation.开卡)
            {
                tbxCardNo.Text = cardNo;// IC卡编号
            }
        }
        /// <summary>
        /// 刷新按钮可用状态
        /// </summary>
        void RefreshButtonStatus()
        {
            if (string.IsNullOrEmpty(tbxCardNo.Text))
            {
                btnUnBinding.Enabled = false;
            }
            else
            {
                btnRead.Enabled = false;
            }
        }
        /// <summary>
        /// 保存商户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Merchant merchant = GetMerchantInfo();
            // 修改卡信息：修改已经存在的商户信息
            if (op == Operation.编辑)
            {
                if (CheckInput())
                {
                    string msg = string.Empty;
                    if (Common.ModifyMerchant(merchant, out msg))
                    {
                        formMain.UpdateRefesh();
                        MessageBox.Show("修改信息成功");
                    }
                    else
                        MessageBox.Show("修改信息失败");
                }
                else return;
            }
            // 开卡：商户不存在，创建新的商户并且绑定IC卡
            if (op == Operation.开卡)
            {
                if (CheckInput())
                {
                    string msg = string.Empty;
                    if (Common.CreateAccount(merchant, out msg))
                    {
                        formMain.UpdateRefesh();
                        MessageBox.Show("卡片绑定成功");
                    }
                    else
                        MessageBox.Show("卡片绑定失败:" + msg);
                }
                else return;
            }
            this.Close();
        }
        /// <summary>
        /// 获取商户对象
        /// </summary>
        /// <returns></returns>
        private Merchant GetMerchantInfo()
        {
            Merchant mer = new Merchant();
            mer.ID = merchantID;
            mer.CardNo = tbxCardNo.Text.Trim();
            mer.Name = tbxCustomer.Text.Trim();
            mer.Address = tbxAddress.Text.Trim();
            mer.PhoneNo = tbxPhoneNo.Text.Trim();
            mer.PersonInCharge = tbxPersonInCharge.Text.Trim();
            mer.Remarks = tbxRemark.Text.Trim();
            return mer;
        }
        /// <summary>
        /// 输入检测
        /// </summary>
        /// <returns></returns>
        bool CheckInput()
        {
            bool pass = true;
            if (string.IsNullOrEmpty(tbxCustomer.Text))
            {
                MessageBox.Show("请填写商户名称");
                pass = false;
            }
            else if (string.IsNullOrEmpty(tbxAddress.Text))
            {
                MessageBox.Show("请填写商户地址");
                pass = false;
            }
            else if (string.IsNullOrEmpty(tbxPersonInCharge.Text))
            {
                MessageBox.Show("请填写负责人");
                pass = false;
            }
            else if (string.IsNullOrEmpty(tbxPhoneNo.Text))
            {
                MessageBox.Show("请填写手机号");
                pass = false;
            }
            return pass;
        }
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            op = Operation.绑定;
            Thread th = new Thread(new ThreadStart(api.GetCardNo));
            th.Start();
        }
        /// <summary>
        /// 卡片解绑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnBinding_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxCardNo.Text))
            {
                MessageBox.Show("该商户未绑定IC卡");
                return;
            }
            Merchant merchant = GetMerchantInfo();
            string msg = string.Empty;
            if(Common.UnBindingCard(merchant , out msg))
            {
                formMain.UpdateRefesh();
                MessageBox.Show("卡片解绑成功");
            }
            else
                MessageBox.Show("卡片解绑失败:" + msg);
            this.Close();
        }
        /// <summary>
        /// 卡片绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBinding_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxCardNo.Text))
            {
                MessageBox.Show("请先刷卡");
                return;
            }
            // 绑定:商户已经存在，但还未绑定IC卡
            if (op == Operation.绑定)
            {
                Merchant merchant = GetMerchantInfo();
                if (CheckInput())
                {
                    string msg = string.Empty;
                    if (Common.BindingCard(merchant, out msg))
                    {
                        formMain.UpdateRefesh();
                        MessageBox.Show("卡片绑定成功");
                    }
                    else
                        MessageBox.Show("卡片绑定失败:" + msg);
                }
                else return;
            }
            this.Close();
        }

        private void FormMerchant_FormClosing(object sender, FormClosingEventArgs e)
        {
            eventPause = true;
        }
    }

    enum Operation
    { 
        开卡,
        编辑,
        绑定
    }
}
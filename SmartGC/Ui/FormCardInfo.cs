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
    public partial class FormCardInfo : Form
    {
        DataRow row;
        public FormCardInfo(DataRow _row)
        {
            InitializeComponent();
            row = _row;
        }

        private void FormCardBinding_Load(object sender, EventArgs e)
        {
            textBox1.Text = row["CardNo"].ToString();// IC卡编号
            textBox2.Text = row["Customer"].ToString(); ;// 商户名称
            textBox3.Text = row["Address"].ToString(); ;// 地址
            textBox4.Text = row["PersonInCharge"].ToString(); ;// 负责人
            textBox5.Text = row["PhoneNo"].ToString(); ;// 手机号
            textBox6.Text = row["CreateDate"].ToString(); ;// 开卡时间
            textBox7.Text = row["Remark"].ToString(); ;// 备注
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartGC.Lib
{
    internal class CardInfo
    {
        string cardNo;
        /// <summary>
        /// IC卡编号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        string customer;
        /// <summary>
        /// 商户名称
        /// </summary>
        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        string address;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        string personInCharge;
        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge
        {
            get { return personInCharge; }
            set { personInCharge = value; }
        }
        string phoneNo;
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }
        string createDate;
        /// <summary>
        /// 开卡时间
        /// </summary>
        public string CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        string remarks;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
    }
}

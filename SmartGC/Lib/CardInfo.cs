
namespace SmartGC.Lib
{
    internal class CardInfo
    {
        string cardNo;
        /// <summary>
        /// IC卡编号
        /// </summary>
        internal string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        string customer;
        /// <summary>
        /// 商户名称
        /// </summary>
        internal string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        string address;
        /// <summary>
        /// 地址
        /// </summary>
        internal string Address
        {
            get { return address; }
            set { address = value; }
        }
        string personInCharge;
        /// <summary>
        /// 负责人
        /// </summary>
        internal string PersonInCharge
        {
            get { return personInCharge; }
            set { personInCharge = value; }
        }
        string phoneNo;
        /// <summary>
        /// 手机号
        /// </summary>
        internal string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }
        string createDate;
        /// <summary>
        /// 开卡时间
        /// </summary>
        internal string CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        bool status;
        /// <summary>
        /// 是否绑定
        /// </summary>
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        string remarks;
        /// <summary>
        /// 备注
        /// </summary>
        internal string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
    }
}

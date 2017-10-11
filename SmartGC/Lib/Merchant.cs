namespace SmartGC.Lib
{
    /// <summary>
    /// 商户信息
    /// </summary>
    public class Merchant
    {
        string id;
        /// <summary>
        /// 商户唯一标识6位
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        string cardNo;
        /// <summary>
        /// IC卡编号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        string name;
        /// <summary>
        /// 商户名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
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
        CardBindingStatus status;
        /// <summary>
        /// 是否绑定
        /// </summary>
        public CardBindingStatus Status
        {
            get { return status; }
            set { status = value; }
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

    public enum CardBindingStatus
    { 
        /// <summary>
        /// 绑定
        /// </summary>
        Y,
        /// <summary>
        /// 未绑定
        /// </summary>
        N,
        /// <summary>
        /// 未知
        /// </summary>
        X
    }
}

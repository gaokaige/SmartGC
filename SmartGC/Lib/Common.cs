using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace SmartGC.Lib
{
    public class Common
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="str"></param>
        public static void WriteLog(string str)
        {
            if (!Configs.LogEnable)
                return;
            System.IO.StreamWriter sw = null;
            System.IO.FileStream fs = null;
            try
            {
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Log\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "Log\\");
                string strFilePath = System.AppDomain.CurrentDomain.BaseDirectory + string.Format("log\\{0}.txt", DateTime.Now.Date.ToString("yyyyMMdd"));
                fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Append);
                sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);

                sw.WriteLine(DateTime.Now.ToString() + "\t" + str);
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Close();
                if (fs != null)
                    fs.Close();
            }
        }
        /// <summary>
        /// 通过卡号查询卡信息
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <returns></returns>
        public static DataTable GetMerchantByCardNo(string cardNo)
        {
            DataTable dt = GetMerchantTable();
            // 发送
            //string data={"serviceMethod":"select","serviceName":"com.cygps.dubbo.wasteSorting.ICreditService","serviceBody":{"cardNo":"541215464646546"}}
            JObject json = new JObject();
            JObject jcredit = new JObject();
            jcredit.Add("cardNo", cardNo);

            JToken serviceMethod, serviceName, serviceBody;
            serviceMethod = "select";
            serviceName = "com.cygps.dubbo.wasteSorting.ICreditService";
            serviceBody = jcredit;

            json.Add("serviceMethod", serviceMethod);
            json.Add("serviceName", serviceName);
            json.Add("serviceBody", serviceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);

            // 解析
            json = JObject.Parse(rt);
            if (json.Count == 0 || (int)(json["code"]) == -1)
                return dt;
            JArray jlist = JArray.Parse(json["array"].ToString());

            JObject tempo;
            JToken jRemark, jCredit;
            string ts, remark;
            int credit;
            for (int i = 0; i < jlist.Count; i++)
            {
                tempo = JObject.Parse(jlist[i].ToString());
                ts = tempo["_saveTime"].ToString();// 时间戳
                ts = ts.Substring(0, ts.Length - 3);

                if (!tempo.TryGetValue("remark", out jRemark))
                    remark = string.Empty;
                else
                    remark = jRemark.ToString();

                if (!tempo.TryGetValue("credit", out jCredit))
                    credit = 0;
                else
                    credit = int.Parse(jCredit.ToString());

                dt.Rows.Add(new object[12] 
                {   
                    i + 1
                    , tempo["id"].ToString()
                    , tempo["cardNo"].ToString()
                    , tempo["name"].ToString()
                    , tempo["status"].ToString() == "Y" ? "已绑定" : "未绑定"
                    , tempo["address"].ToString()
                    , tempo["personLiable"].ToString()
                    , tempo["phoneNumber"].ToString()
                    , GetTime(ts)
                    , credit
                    , "编辑"
                    , remark.ToString()
                    });
            }
            return dt;
        }
        /// <summary>
        /// 分页获取卡信息
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public static DataTable GetMerchantList(Merchant cardInfo, int pagesize, int pageindex, out int total)
        {
            DataTable dt = GetMerchantTable();
            // 发送
            //string data={"serviceMethod":"selectByPage","serviceName":"com.cygps.dubbo.wasteSorting.ICreditService","serviceBody":{"name":"哈哈面","status":"Y","_pageSize":10,"_page":1,"_sortField":"credit","_number":-1}} 
            JObject json = new JObject();
            JObject jPamarm = new JObject();
            if (!string.IsNullOrEmpty(cardInfo.Name))
                jPamarm.Add("*name", cardInfo.Name);
            if (cardInfo.Status != CardBindingStatus.X)
                jPamarm.Add("status", cardInfo.Status.ToString());

            if (!string.IsNullOrEmpty(cardInfo.PhoneNo))
                jPamarm.Add("*phoneNumber", cardInfo.PhoneNo);
            jPamarm.Add("_pageSize", pagesize);// 必要
            jPamarm.Add("_page", pageindex);// 必要
            jPamarm.Add("_sortField", "credit");// 排序字段 非必
            jPamarm.Add("_number", -1);//  1正序 -1倒序 非必

            JToken serviceMethod, serviceName, serviceBody;
            serviceMethod = "selectByPage";
            serviceName = "com.cygps.dubbo.wasteSorting.ICreditService";
            serviceBody = jPamarm;

            json.Add("serviceMethod", serviceMethod);
            json.Add("serviceName", serviceName);
            json.Add("serviceBody", serviceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);

            // 接收
            // 解析
            json = JObject.Parse(rt);
            if (json.Count == 0 || (int)(json["code"]) == -1)
            {
                total = 0;
                return dt;
            }
            JArray jlist = JArray.Parse(json["array"].ToString());

            JObject tempo;
            JToken jRemark, jCardNo, jCredit;
            string ts;// 开卡时间戳13位处理成10位
            string remark, cardNo;
            int credit;
            int j = pagesize * (pageindex - 1);

            for (int i = 0; i < jlist.Count; i++)
            {
                tempo = JObject.Parse(jlist[i].ToString());

                if (!tempo.TryGetValue("cardNo", out jCardNo))
                    cardNo = "";
                else
                    cardNo = jCardNo.ToString();

                if (!tempo.TryGetValue("remark", out jRemark))
                    remark = string.Empty;
                else
                    remark = jRemark.ToString();

                if (!tempo.TryGetValue("credit", out jCredit))
                    credit = 0;
                else
                    credit = int.Parse(jCredit.ToString());

                ts = tempo["_saveTime"].ToString();// 时间戳
                ts = ts.Substring(0, ts.Length - 3);

                dt.Rows.Add(new object[12] 
                {   
                    i + j + 1
                    , tempo["id"].ToString()
                    , cardNo
                    , tempo["name"].ToString()
                    , tempo["status"].ToString()=="Y" ? "已绑定" : "未绑定"
                    , tempo["address"].ToString()
                    , tempo["personLiable"].ToString()
                    , tempo["phoneNumber"].ToString()
                    , GetTime(ts)
                    , credit
                    , "编辑"
                    , remark
                });
            }
            total = int.Parse(json["total"].ToString());
            return dt;
        }
        /// <summary>
        /// 分页查询获取商品列表
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable GetCommodityInfoList(int pagesize, int pageindex, out int total)
        {
            DataTable dt = GetCommodityTable();

            JObject jParam = new JObject();
            jParam.Add("isPublish", "Y");
            jParam.Add("_pageSize", pagesize);
            jParam.Add("_page", pageindex);

            JToken serviceMethod, serviceName, serviceBody;
            serviceMethod = "selectByPage";
            serviceName = "com.cygps.dubbo.wasteSorting.IGiftInfoService";
            serviceBody = jParam;

            JObject json = new JObject();
            json.Add("serviceMethod", serviceMethod);
            json.Add("serviceName", serviceName);
            json.Add("serviceBody", serviceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);
            json = JObject.Parse(rt);
            if (json.Count == 0 || (int)(json["code"]) == -1 || (int)(json["total"]) == 0)
            {
                total = 0;
                return dt;
            }

            JArray jlist = JArray.Parse(json["array"].ToString());

            JObject tempo;
            JToken remark;
            string ts;

            int j = pagesize * (pageindex - 1);

            for (int i = 0; i < jlist.Count; i++)
            {
                tempo = JObject.Parse(jlist[i].ToString());
                ts = tempo["_saveTime"].ToString();// 时间戳
                ts = ts.Substring(0, ts.Length - 3);
                remark = tempo.TryGetValue("remark", out remark);
                dt.Rows.Add(new object[7] 
            {   
                tempo["gid"].ToString()
                ,  (i + j + 1)
                , tempo["name"].ToString()
                , tempo["manufacturer"].ToString()
                , tempo["giftType"].ToString()
                , tempo["credit"].ToString()
                , "兑换"
            });
            }
            total = int.Parse(json["total"].ToString());
            return dt;

        }
        /// <summary>
        /// post方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PostData(string url, string data)
        {
            WriteLog("send:" + data);
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(data);

            ServicePointManager.Expect100Continue = Configs.Expect100Continue;
            string srcString = string.Empty;
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            webClient.Headers.Add("token:" + Configs.Token);

            try
            {
                byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
                srcString = Encoding.UTF8.GetString(responseData);//解码
                WriteLog("receive:" + srcString);
            }
            catch (Exception exc)
            {
                WriteLog(string.Format("post error:{0} \r\nurl[{1}]", exc.Message, url));
            }
            return srcString;
        }
        /// <summary>
        /// 获取一个商户的空表
        /// </summary>
        /// <returns></returns>
        private static DataTable GetMerchantTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(Int32));
            dt.Columns.Add("ID", typeof(string));//商户ID
            dt.Columns.Add("CardNo", typeof(string));//绑定卡号
            dt.Columns.Add("Customer", typeof(string));// 名称
            dt.Columns.Add("Status", typeof(string));// 卡绑定状态
            dt.Columns.Add("Address", typeof(string));//地址
            dt.Columns.Add("PersonInCharge", typeof(string));//负责人
            dt.Columns.Add("PhoneNo", typeof(string));//电话
            dt.Columns.Add("CreateDate", typeof(DateTime));//开卡日期
            dt.Columns.Add("Credit", typeof(Int32));//积分
            dt.Columns.Add("Action", typeof(string));
            dt.Columns.Add("Remark", typeof(string));//备注

            return dt;
        }
        /// <summary>
        /// 获取一个商品的空表
        /// </summary>
        /// <returns></returns>
        private static DataTable GetCommodityTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));//GID
            dt.Columns.Add("Index", typeof(Int32));//序号
            dt.Columns.Add("Name", typeof(string));//名称
            dt.Columns.Add("Manufacturer", typeof(string));// 生产厂家
            dt.Columns.Add("Type", typeof(string));//类型
            dt.Columns.Add("Score", typeof(string));//兑换积分
            dt.Columns.Add("Exchange", typeof(string));
            return dt;
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        public static string GetRandomCardNo()
        {
            DateTime now = DateTime.Now;
            string stamp = Math.Round((now - new DateTime(1970, 1, 1)).TotalMilliseconds,0).ToString();
            Random r = new Random();
            int i = r.Next(0,9);
            stamp += i.ToString();
            i = r.Next(0, 9);
            stamp += i.ToString();
            return stamp;
        }
        /// <summary>
        /// 修改商户信息
        /// </summary>
        /// <param name="merchant">商户信息</param>
        /// <returns></returns>
        public static bool ModifyMerchant(Merchant merchant, out string msg)
        {
            bool result = true;

            JObject jID = new JObject();
            jID.Add("id", merchant.ID);

            JObject jBody = new JObject();
            jBody.Add("name", merchant.Name);
            jBody.Add("address", merchant.Address);
            jBody.Add("phoneNumber", merchant.PhoneNo);
            jBody.Add("personLiable", merchant.PersonInCharge);
            jBody.Add("remark", merchant.Remarks);

            JObject jServiceBody = new JObject();
            jServiceBody.Add("where", jID);
            jServiceBody.Add("body", jBody);

            JObject json = new JObject();
            json.Add("serviceMethod", "update");
            json.Add("serviceName", "com.cygps.dubbo.wasteSorting.ICreditService");
            json.Add("serviceBody", jServiceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);
            json = JObject.Parse(rt);
            if (json.Count == 0)
            {
                result = false;
                msg = "内部错误";
            }
            else if (json["code"].ToString() == "-1")
            {
                result = false;
                msg = json["message"].ToString();
            }
            else if (json["code"].ToString() == "0")
            {
                msg = "修改成功";
            }
            else
            {
                result = false;
                msg = "未知的错误";
            }

            return result;
        }
        /// <summary>
        /// 开户并绑定IC卡
        /// </summary>
        /// <param name="merchant">商户信息</param>
        /// <returns></returns>
        public static bool CreateAccount(Merchant merchant, out string msg)
        {
            bool result = true;
            // 创建商户
            JObject jServiceBody = new JObject();
            jServiceBody.Add("name", merchant.Name);
            jServiceBody.Add("address", merchant.Address);
            jServiceBody.Add("phoneNumber", merchant.PhoneNo);
            jServiceBody.Add("personLiable", merchant.PersonInCharge);
            jServiceBody.Add("status", "N");
            jServiceBody.Add("remark", merchant.Remarks);

            JObject json = new JObject();
            json.Add("serviceMethod", "save");
            json.Add("serviceName", "com.cygps.dubbo.wasteSorting.ICreditService");
            json.Add("serviceBody", jServiceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);
            json = JObject.Parse(rt);

            if (json.Count == 0)
            {
                result = false;
                msg = "内部错误";
            }
            else if (json["code"].ToString() == "-1")
            {
                result = false;
                msg = json["message"].ToString();
            }
            else if (json["code"].ToString() == "0")
            {
                msg = json["message"].ToString();
                merchant.ID = json["id"].ToString();
                // 绑定卡
                //{"where":{"id":100000},"body":{"cardNo":"3546521554","status":"Y"}}
                JObject jWhere = new JObject();
                jWhere.Add("id", merchant.ID);

                JObject jBody = new JObject();
                jBody.Add("cardNo", merchant.ID);
                jBody.Add("status", "Y");

                jServiceBody = new JObject();
                jServiceBody.Add("body", jBody);
                jServiceBody.Add("where", jWhere);

                json = new JObject();
                json.Add("serviceMethod", "update");
                json.Add("serviceName", "com.cygps.dubbo.wasteSorting.ICreditService");
                json.Add("serviceBody", jServiceBody);

                postData = "data=" + json.ToString();
                rt = PostData(Configs.Server, postData);
                json = JObject.Parse(rt);

                if (json.Count == 0)
                {
                    result = false;
                    msg = "商户已添加，但绑定IC卡错误";
                }
                if (json["code"].ToString() == "-1")
                {
                    result = false;
                    msg = "商户已添加，但绑定IC卡错误:" + msg;
                }
                else if (json["code"].ToString() == "0")
                {
                    msg = "开卡成功";
                }
            }
            else
            {
                result = false;
                msg = "未知的错误";
            }
            return result;
        }
        /// <summary>
        /// 商户绑定IC卡
        /// </summary>
        /// <param name="merchant"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool BindingCard(Merchant merchant, out string msg)
        {
            bool result = true;
            //{"where":{"id":100000},"body":{"cardNo":"3546521554","status":"Y"}}
            JObject jID = new JObject();
            jID.Add("id", merchant.ID);

            JObject jBody = new JObject();
            jBody.Add("cardNo", merchant.CardID);
            jBody.Add("status", "Y");

            JObject jServiceBody = new JObject();
            jServiceBody.Add("where", jID);
            jServiceBody.Add("body", jBody);

            JObject json = new JObject();
            json.Add("serviceMethod", "update");
            json.Add("serviceName", "com.cygps.dubbo.wasteSorting.ICreditService");
            json.Add("serviceBody", jServiceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);
            json = JObject.Parse(rt);

            if (json.Count == 0)
            {
                result = false;
                msg = "内部错误";
            }
            else if (json["code"].ToString() == "-1")
            {
                result = false;
                msg = json["message"].ToString();
            }
            else if (json["code"].ToString() == "0")
            {
                msg = "绑定成功";
            }
            else
            {
                result = false;
                msg = "未知的错误";
            }
            
            return result;
        }
        /// <summary>
        /// 商户解绑IC卡
        /// </summary>
        /// <param name="merchant"></param>
        /// <returns></returns>
        public static bool UnBindingCard(Merchant merchant, out string msg)
        {
            bool result = true;
            //{"where":{"id":100000},"body":{"cardNo":"3546521554","status":"N"}}
            JObject jID = new JObject();
            jID.Add("id", merchant.ID);

            JObject jBody = new JObject();
            jBody.Add("cardNo", merchant.CardID);
            jBody.Add("status", "N");

            JObject jServiceBody = new JObject();
            jServiceBody.Add("where", jID);
            jServiceBody.Add("body", jBody);

            JObject json = new JObject();
            json.Add("serviceMethod", "update");
            json.Add("serviceName", "com.cygps.dubbo.wasteSorting.ICreditService");
            json.Add("serviceBody", jServiceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);
            json = JObject.Parse(rt);

            if (json.Count == 0)
            {
                result = false;
                msg = "内部错误";
            }
            else if (json["code"].ToString() == "-1")
            {
                result = false;
                msg = json["message"].ToString();
            }
            else if (json["code"].ToString() == "0")
            {
                msg = "解绑成功";
            }
            else
            {
                result = false;
                msg = "未知的错误";
            }

            return result;
        }
        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Login(string userName, string password, out string msg)
        {
            //data={"serviceMethod":"login","serviceName":"com.cygps.dubbo.comm.IUserService","serviceBody":{"account":"yun123!","password":"yun123123"}}
            bool result = true;

            JObject jServiceBody = new JObject();
            jServiceBody.Add("account", userName);
            jServiceBody.Add("password", password);

            JObject json = new JObject();
            json.Add("serviceMethod", "login");
            json.Add("serviceName", "com.cygps.dubbo.comm.IUserService");
            json.Add("serviceBody", jServiceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);
            json = JObject.Parse(rt);

            if (json.Count == 0)
            {
                result = false;
                msg = "内部错误";
            }
            else if (json["code"].ToString() == "-1")
            {
                result = false;
                msg = json["message"].ToString();
            }
            else if (json["code"].ToString() == "0")
            {
                msg = "登录成功";
                Configs.Token = json["token"].ToString();
            }
            else
            {
                result = false;
                msg = "未知的错误";
            }

            return result;
        }
        /// <summary>
        /// 商户积分兑换商品
        /// </summary>
        /// <param name="merchantID">商户ID</param>
        /// <param name="count">兑换数量</param>
        /// <param name="gid">商品ID</param>
        /// <returns></returns>
        internal static bool Exchage(string merchantID, int count, string gid, out string msg)
        {
            bool result = true;
            //{"id":100000,"number":5,"gid":"20170926112201729"}
            JObject jServiceBody = new JObject();
            jServiceBody.Add("id", merchantID);
            jServiceBody.Add("number", count);
            jServiceBody.Add("gid", gid);

            JObject json = new JObject();
            json.Add("serviceMethod", "subtractCredit");
            json.Add("serviceName", "com.cygps.dubbo.wasteSorting.ICreditService");
            json.Add("serviceBody", jServiceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);
            json = JObject.Parse(rt);
            if (json.Count == 0)
            {
                result = false;
                msg = "内部错误";
            }
            else if (json["code"].ToString() == "-1"
                || json["code"].ToString() == "-2"
                || json["code"].ToString() == "-3"
                || json["code"].ToString() == "-4"
                || json["code"].ToString() == "-5"
                || json["code"].ToString() == "-6")
            {
                result = false;
                msg = json["message"].ToString();
            }
            else if (json["code"].ToString() == "0")
            {
                msg = "兑换成功";
            }
            else
            {
                result = false;
                msg = "未知的错误";
            }

            return result;
        }
    }
}
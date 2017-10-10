using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace SmartGC.Lib
{
    internal class Common
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
        public static DataTable GetCardInfoByCardNo(string cardNo)
        {
            DataTable dt = GetCardTable();
            // 发送
            //string data={"serviceMethod":"select","serviceName":"com.cygps.dubbo.creditCard.ICreditService","serviceBody":{"cardNo":"541215464646546"}}
            JObject json = new JObject();
            JObject jcredit = new JObject();
            jcredit.Add("cardNo", cardNo);

            JToken serviceMethod, serviceName, serviceBody;
            serviceMethod = "select";
            serviceName = "com.cygps.dubbo.creditCard.ICreditService";
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
            JToken remark;
            string ts;
            for (int i = 0; i < jlist.Count; i++)
            {
                tempo = JObject.Parse(jlist[i].ToString());  
                ts = tempo["_saveTime"].ToString();// 时间戳
                ts = ts.Substring(0, ts.Length - 3);
                remark = tempo.TryGetValue("remark",out remark);
                dt.Rows.Add(new object[10] 
            {   i + 1
                , tempo["cardNo"].ToString()
                , tempo["name"].ToString()
                , tempo["status"].ToString() == "Y" ? "已绑定" : "未绑定"
                , tempo["address"].ToString()
                , tempo["personLiable"].ToString()
                , tempo["phoneNumber"].ToString()
                , GetTime(ts)
                , "编辑"
                , remark.ToString() == "False" ? "" : remark.ToString()});
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
        public static DataTable GetCardInfoList(CardInfo cardInfo, int pagesize, int pageindex, out int total)
        {
            DataTable dt = GetCardTable();
            // 发送
            //string data={"serviceMethod":"selectByPage","serviceName":"com.cygps.dubbo.creditCard.ICreditService","serviceBody":{"name":"哈哈面","status":"Y","_pageSize":10,"_page":1,"_sortField":"credit","_number":-1}} 
            JObject json = new JObject();
            JObject jPamarm = new JObject();
            if (!string.IsNullOrEmpty(cardInfo.Customer))
                jPamarm.Add("*name", cardInfo.Customer);
            if (cardInfo.Status != CardStatus.X)
                jPamarm.Add("status", cardInfo.Status.ToString());

            if (!string.IsNullOrEmpty(cardInfo.PhoneNo))
                jPamarm.Add("*phoneNumber", cardInfo.PhoneNo);
            jPamarm.Add("_pageSize", pagesize);// 必要
            jPamarm.Add("_page", pageindex);// 必要
            jPamarm.Add("_sortField", "credit");// 排序字段 非必
            jPamarm.Add("_number", -1);//  1正序 -1倒序 非必

            JToken serviceMethod, serviceName, serviceBody;
            serviceMethod = "selectByPage";
            serviceName = "com.cygps.dubbo.creditCard.ICreditService";
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
            JToken jRemark, jCardNo;
            string ts;// 开卡时间戳13位处理成10位
            string remark, cardNo;
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

                ts = tempo["_saveTime"].ToString();// 时间戳
                ts = ts.Substring(0, ts.Length - 3);
                
                dt.Rows.Add(new object[10] 
                {   
                    i + j + 1
                    , cardNo
                    , tempo["name"].ToString()
                    , tempo["status"].ToString()=="Y" ? "已绑定" : "未绑定"
                    , tempo["address"].ToString()
                    , tempo["personLiable"].ToString()
                    , tempo["phoneNumber"].ToString()
                    , GetTime(ts)
                    , "编辑"
                    , remark
                });
            }
            total = int.Parse(json["total"].ToString());
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable GetCommodityInfoList(int pagesize, int pageindex, out int total)
        {
            DataTable dt = GetCommodityTable();
            JObject json = new JObject();
            JObject jParam = new JObject();
            jParam.Add("isPublish", "Y");
            jParam.Add("_pageSize", pagesize);
            jParam.Add("_page", pageindex);

            JToken serviceMethod, serviceName, serviceBody;
            serviceMethod = "selectByPage";
            serviceName = "com.cygps.dubbo.creditGift.IGiftInfoService";
            serviceBody = jParam;

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

        public static string PostData(string url, string data)
        {
            WriteLog("send:" + data);
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(data);

            ServicePointManager.Expect100Continue = false;
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
        /// 获取一个卡信息的空表
        /// </summary>
        /// <returns></returns>
        private static DataTable GetCardTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(Int32));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("PersonInCharge", typeof(string));
            dt.Columns.Add("PhoneNo", typeof(string));
            dt.Columns.Add("CreateDate", typeof(DateTime));
            dt.Columns.Add("Action", typeof(string));
            dt.Columns.Add("Remark", typeof(string));

            return dt;
        }
        /// <summary>
        /// 获取一个兑换物品信息的空表
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
    }
}

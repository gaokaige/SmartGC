using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            // 发送
            //string data={"serviceMethod":"select","serviceName":"com.cygps.dubbo.creditCard.ICreditService","serviceBody":{"cardNo":15,"telNo":100000,"id":100001}} 
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

            // 接收
            JToken jCardNo, jCredit, jName;
            json = (JObject)JsonConvert.DeserializeObject(rt);
            json.TryGetValue("cardNo", out jCardNo);
            json.TryGetValue("credit", out jCredit);
            json.TryGetValue("name", out jName);
            DataTable dt = GetCardTable();
            //demo
            dt.Rows.Add(new object[10] 
            {   1
                , "0000000000001"//jCardNo.ToString()
                , "张三的面馆"
                , "已绑定"
                , "北京市海淀区林翠桥"
                , "张三", "13801012020"
                , "2017-08-10"
                , "编辑"
                , "这里显示备注内容" });

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
            // 发送
            //string data={"serviceMethod":"select","serviceName":"com.cygps.dubbo.creditCard.ICreditService","serviceBody":{"cardNo":15,"telNo":100000,"id":100001}} 
            JObject json = new JObject();
            JObject jcredit = new JObject();
            jcredit.Add("cardNo", cardInfo.CardNo);
            JToken serviceMethod, serviceName, serviceBody;
            serviceMethod = "select";
            serviceName = "com.cygps.dubbo.creditCard.ICreditService";
            serviceBody = jcredit;

            json.Add("serviceMethod", serviceMethod);
            json.Add("serviceName", serviceName);
            json.Add("serviceBody", serviceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);

            // 接收
            JToken jCardNo, jCredit, jName;
            json = (JObject)JsonConvert.DeserializeObject(rt);
            json.TryGetValue("cardNo", out jCardNo);
            json.TryGetValue("credit", out jCredit);
            json.TryGetValue("name", out jName);
            DataTable dt = GetCardTable();

            int j = pagesize * (pageindex - 1);
            for (int i = 0; i < pagesize; i++)
            {
                dt.Rows.Add(new object[10] 
                {   i + j + 1 // 序号
                    , "000000000000"+i//jCardNo.ToString()
                    , "张三的面馆"
                    , "已绑定"
                    , "北京市海淀区林翠桥"
                    , "张三", "13801012020"
                    , "2017-08-12"
                    , "编辑"
                    , "这里显示备注内容" });
            }
            total = 100;
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable GetCommodityInfoList(int pagesize, int pageindex, out int total)
        {
            JObject json = new JObject();
            JObject jcredit = new JObject();
            //jcredit.Add("cardNo", cardInfo.CardNo);
            JToken serviceMethod, serviceName, serviceBody;
            serviceMethod = "select";
            serviceName = "com.cygps.dubbo.creditCard.ICreditService";
            serviceBody = jcredit;

            json.Add("serviceMethod", serviceMethod);
            json.Add("serviceName", serviceName);
            json.Add("serviceBody", serviceBody);

            string postData = "data=" + json.ToString();
            string rt = PostData(Configs.Server, postData);

            DataTable dt = GetCommodityTable();
            for (int i = 0; i < 50; i++)
            {
                dt.Rows.Add(new object[7] { "123", i + 1, "Iphone8" + (i + 1).ToString(), "苹果公司", "状态", "500", "兑换" });
            }
            total = 100;
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
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Index", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Manufacturer", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Score", typeof(string));
            dt.Columns.Add("Exchange", typeof(string));
            return dt;
        }

    }
}

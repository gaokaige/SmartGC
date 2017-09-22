using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace SmartGC.Lib
{
    internal class Common
    {
        
        internal static void WriteLog(string str)
        {
            if (!Config.LogEnable)
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

        internal static DataTable GetCardInfoData(string json)
        {
            DataTable dtCardInfo = new DataTable();
            dtCardInfo.Columns.Add("Index", typeof(Int32));
            dtCardInfo.Columns.Add("CardNo", typeof(string));
            dtCardInfo.Columns.Add("Customer", typeof(string));
            dtCardInfo.Columns.Add("Status", typeof(string));
            dtCardInfo.Columns.Add("Address", typeof(string));
            dtCardInfo.Columns.Add("PersonInCharge", typeof(string));
            dtCardInfo.Columns.Add("PhoneNo", typeof(string));
            dtCardInfo.Columns.Add("CreateDate", typeof(DateTime));
            dtCardInfo.Columns.Add("Action", typeof(string));
            dtCardInfo.Columns.Add("Remark", typeof(string));
            for (int i = 0; i < 50; i++)
            {
                dtCardInfo.Rows.Add(new object[10] { i + 1, "00000" + (i + 1).ToString(), "张三的面馆", "已绑定", "北京市海淀区林翠桥", "张三", "13801012020", "2017-08-10", "编辑", "这里显示备注内容" });
            }

            return dtCardInfo;
        }

        internal static DataTable GetCommodityInfoData(string json)
        {
            DataTable dtCardInfo = new DataTable();
            dtCardInfo.Columns.Add("Index", typeof(Int32));
            dtCardInfo.Columns.Add("Name", typeof(string));
            dtCardInfo.Columns.Add("Manufacturer", typeof(string));
            dtCardInfo.Columns.Add("Status", typeof(string));
            dtCardInfo.Columns.Add("Score", typeof(string));
            dtCardInfo.Columns.Add("Exchange", typeof(string));
            for (int i = 0; i < 50; i++)
            {
                dtCardInfo.Rows.Add(new object[6] { i + 1, "垃圾桶" + (i + 1).ToString(), "苹果公司", "状态", "500", "兑换" });
            }

            return dtCardInfo;
        }
    }
}

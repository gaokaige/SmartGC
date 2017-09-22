using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;

namespace SmartGC.Lib
{
    internal static class Config
    {
        static bool logEnable;
        /// <summary>
        /// 是否保存日志
        /// </summary>
        public static bool LogEnable
        {
            get { return logEnable; }
            set { logEnable = value; }
        }
        static string cardServer;
        /// <summary>
        /// 查询卡详情接口地址
        /// </summary>
        public static string CardServer
        {
            get { return Config.cardServer; }
            set { Config.cardServer = value; }
        }
        static string commodityServer;
        /// <summary>
        /// 查询兑换物品信息接口
        /// </summary>
        public static string CommodityServer
        {
            get { return Config.commodityServer; }
            set { Config.commodityServer = value; }
        }
        static string lastUser;
        /// <summary>
        /// 上次登录用户
        /// </summary>
        public static string LastUser
        {
            get { return Config.lastUser; }
            set { Config.lastUser = value; }
        }
        internal static void Init()
        { 
            
        }
        public static string fileName = System.IO.Path.GetFileName(Application.ExecutablePath);
        public static string GetSetting(string key)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
            string value = config.AppSettings.Settings[key].Value;
            return value;
        }

        public static bool UpdateSetting(string key, string newValue)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
            string value = config.AppSettings.Settings[key].Value = newValue;
            config.Save();
            return true;
        }
    }
}

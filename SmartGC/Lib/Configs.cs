using System;
using System.Configuration;
using System.Windows.Forms;

namespace SmartGC.Lib
{
    internal static class Configs
    {
        static string server;
        /// <summary>
        /// 查询卡详情接口地址
        /// </summary>
        internal static string Server
        {
            get { return Configs.server; }
            set { Configs.server = value; }
        }
        static string token;
        /// <summary>
        /// 服务器令牌
        /// </summary>
        public static string Token
        {
            get { return Configs.token; }
            set { Configs.token = value; }
        }
        static bool logEnable;
        /// <summary>
        /// 是否保存日志
        /// </summary>
        internal static bool LogEnable
        {
            get { return logEnable; }
            set { logEnable = value; }
        }
        static string lastUser;
        /// <summary>
        /// 上次登录用户
        /// </summary>
        internal static string LastUser
        {
            get { return Configs.lastUser; }
            set { Configs.lastUser = value; }
        }
        static bool expect100Continue;
        /// <summary>
        /// 于服务器通讯时，Expect100Continue的设置
        /// </summary>
        public static bool Expect100Continue
        {
            get { return Configs.expect100Continue; }
            set { Configs.expect100Continue = value; }
        }
        static int pagesize;
        /// <summary>
        /// 每个页面记录数
        /// </summary>
        public static int Pagesize
        {
            get { return Configs.pagesize; }
            set { Configs.pagesize = value; }
        }


        public static string configFile = System.IO.Path.GetFileName(Application.ExecutablePath);

        public static void Init()
        {
            try
            {
                Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(configFile);
                server = config.AppSettings.Settings["Server"].Value;
                token = config.AppSettings.Settings["Token"].Value;
                lastUser = config.AppSettings.Settings["LastUser"].Value;
                logEnable = bool.Parse(config.AppSettings.Settings["LogEnable"].Value);
                expect100Continue = bool.Parse(config.AppSettings.Settings["Expect100Continue"].Value);
                pagesize = int.Parse(config.AppSettings.Settings["Pagesize"].Value);
            }
            catch
            {
                MessageBox.Show("读取配置错误");
            }
        }

        public static void Save()
        {
            try
            {
                Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(configFile);
                config.AppSettings.Settings["Server"].Value = server;
                config.AppSettings.Settings["Token"].Value = token;
                config.AppSettings.Settings["LastUser"].Value = lastUser;
                config.AppSettings.Settings["LogEnable"].Value = logEnable.ToString();
                config.AppSettings.Settings["LogEnable"].Value = expect100Continue.ToString();
                config.AppSettings.Settings["Pagesize"].Value = pagesize.ToString();
                config.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show("保存配置错误:"+ex.Message);
            }
        }
    }
}
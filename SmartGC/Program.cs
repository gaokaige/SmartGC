using SmartGC.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SmartGC
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool noInstance;

            System.Threading.Mutex mutex = new System.Threading.Mutex(true, "Global\\" + System.Reflection.Assembly.GetExecutingAssembly().FullName, out noInstance);

            if (noInstance)
            {
                SmartGC.Lib.Configs.Init();
                Application.Run(new FormLogin());
            }
            else
            {
                MessageBox.Show("程序已经启动，请不要同时运行多次。");
                Application.Exit();
            }
         
        }
    }
}

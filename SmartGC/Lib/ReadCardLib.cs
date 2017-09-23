using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SmartGC.Lib
{
    internal class ReadCardLib
    {
        #region CARD API
        /// <summary>
        /// 打开读写器，建立读写器与PC之间的连接
        /// </summary>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_usbopen", SetLastError = true,
               CharSet = CharSet.Auto, ExactSpelling = false,
               CallingConvention = CallingConvention.StdCall)]
        public static extern int rf_usbopen();
        /// <summary>
        /// 关闭读写器，断开读写器与PC 之间的连接
        /// </summary>
        /// <param name="icdev"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_usbclose", SetLastError = true,
               CharSet = CharSet.Auto, ExactSpelling = false,
               CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_usbclose(int icdev);
        /// <summary>
        /// 控制蜂鸣器
        /// </summary>
        /// <param name="icdev">读卡器设备</param>
        /// <param name="msec">鸣叫时长（毫秒）</param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_beep", SetLastError = true,
               CharSet = CharSet.Auto, ExactSpelling = false,
               CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_beep(int icdev, int msec);
        /// <summary>
        /// 获取硬件版本号
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_get_status", SetLastError = true,
              CharSet = CharSet.Auto, ExactSpelling = false,
              CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_get_status(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] state);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_load_key", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_load_key(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_load_key_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_load_key_hex(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);


        [DllImport("mwhrf_bj.dll", EntryPoint = "a_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)]byte[] hex, int len);

        [DllImport("mwhrf_bj.dll", EntryPoint = "hex_a", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 hex_a([MarshalAs(UnmanagedType.LPArray)]byte[] hex, [MarshalAs(UnmanagedType.LPArray)]byte[] asc, int len);
        /// <summary>
        /// 复位 RF（射频）模块
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="msec"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_reset", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_reset(int icdev, int msec);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_request", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_request(int icdev, int mode, out UInt16 tagtype);


        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_anticoll", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_anticoll(int icdev, int bcnt, out uint snr);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_select", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_select(int icdev, uint snr, out byte size);
        /// <summary>
        /// 成功可返回卡的序列号，并且卡片进入激活状态。
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="mode"></param>
        /// <param name="snr"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_card", SetLastError = true,
            CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_card(int icdev, int mode, [MarshalAs(UnmanagedType.LPArray)]byte[] snr);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_authentication", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_authentication(int icdev, int mode, int secnr);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_authentication_2", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_authentication_2(int icdev, int mode, int keynr, int blocknr);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_read(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_read_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_read_hex(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_write_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_write_hex(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_write(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);
        /// <summary>
        /// 结束对该卡的操作
        /// </summary>
        /// <param name="icdev"></param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_halt", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_halt(int icdev);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_changeb3", SetLastError = true,
            CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_changeb3(int icdev, int sector, [MarshalAs(UnmanagedType.LPArray)]byte[] keya, int B0, int B1,
              int B2, int B3, int Bk, [MarshalAs(UnmanagedType.LPArray)]byte[] keyb);
        #endregion

        #region C#
        internal delegate void SendMessageHandler(string msg);
        internal event SendMessageHandler OnSendMessage;

        internal delegate void ConnHandler();
        internal event ConnHandler OnConnOK;

        internal delegate void DisConnHandler();
        internal event DisConnHandler OnDisConn;

        internal delegate void ReadCardNoHandler(string cardNo);
        internal event ReadCardNoHandler OnReadCardNo;


        int icdev;// 读卡器设备id
        byte[] snr = new byte[5];
        int istr;
        string str;

        internal void ConnectUsbDev()
        {
            icdev = rf_usbopen();
            if (icdev > 0)
            {
                byte[] status = new byte[30];
                istr = rf_get_status(icdev, status);
                //lbHardVer.Text=System.Text.Encoding.ASCII.GetString(status);
                str = System.Text.Encoding.Default.GetString(status);
                OnSendMessage("设备连接成功.硬件版本号：" + str);
                OnConnOK();
                rf_beep(icdev, 10);
            }
            else
            {
                OnSendMessage("设备连接失败，请重试.");
                rf_beep(icdev, 10);
                rf_beep(icdev, 10);
            }

        }
        internal string GetCardNo()
        {
            string cardNo = string.Empty;

            istr = rf_card(icdev, 1, snr);
            if (istr != 0)
            {
                OnSendMessage("读卡失败,请将卡片放置于读卡区域.");
            }
            else
            {
                byte[] snr1 = new byte[8];
                
                hex_a(snr, snr1, 4);
                string no = System.Text.Encoding.Default.GetString(snr1);
                rf_beep(icdev, 10);
                OnSendMessage("读卡成功,卡号：" + no);
                OnReadCardNo(no);
            }

            return cardNo;
        }

        internal void DisconnectUsbDev()
        {
            istr = rf_usbclose(icdev);
            if (istr == 0)
            {
                OnDisConn();
                OnSendMessage("成功断开连接！");
            }
        }
        #endregion
    }
}

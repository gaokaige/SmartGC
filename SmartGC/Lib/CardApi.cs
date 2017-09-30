using System;
using System.Runtime.InteropServices;

namespace SmartGC.Lib
{
    public class CardApi
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="mode"></param>
        /// <param name="tagtype"></param>
        /// <returns></returns>
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

        #region C# Function
        public delegate void SendMessageHandler(string msg);
        /// <summary>
        /// 传送消息
        /// </summary>
        public event SendMessageHandler OnSendMessage;
        public delegate void ConnHandler();
        /// <summary>
        /// 读卡器连接成功时发生
        /// </summary>
        public event ConnHandler OnConnOK;
        public delegate void DisConnHandler();
        /// <summary>
        /// 读卡器断开连接成功时发生
        /// </summary>
        public event DisConnHandler OnDisConn;
        public delegate void ReadCardNoHandler(string cardNo);
        /// <summary>
        /// 读取卡片ID成功时发生
        /// </summary>
        public event ReadCardNoHandler OnReadCardNo;
        /// <summary>
        /// 读卡器设备id
        /// </summary>
        int icdev;
        byte[] snr = new byte[5];// 卡序列号5字节
        byte[] cno = new byte[16];// 卡号16字节
        int istr;
        string str;
        /// <summary>
        /// 连接读卡器
        /// </summary>
        public void ConnectUsbDev()
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
        /// <summary>
        /// 读卡获取卡ID
        /// </summary>
        /// <returns></returns>
        public void GetCardNo()
        {
            string cardNo = string.Empty;

            istr = rf_card(icdev, 1, snr);
            if (istr != 0)
            {
                OnSendMessage("卡激活失败,请将卡片放置于读卡区域.");
            }
            else
            {
                //byte[] snr1 = new byte[8];
                //hex_a(snr, snr1, 4);
                //string no = System.Text.Encoding.Default.GetString(snr1);

                byte[] acno = new byte[16];
                rf_read(icdev, 2, cno);
                rf_halt(icdev);
                hex_a(cno, acno, 8);
                string no = System.Text.Encoding.Default.GetString(acno);
                rf_beep(icdev, 10);
                OnSendMessage("读卡成功,卡号：" + no);
                OnReadCardNo(no);
            }
        }
        /// <summary>
        /// 断开读卡器
        /// </summary>
        public void DisconnectUsbDev()
        {
            istr = rf_usbclose(icdev);
            if (istr == 0)
            {
                OnDisConn();
                OnSendMessage("成功断开连接！");
            }
        }

        public void WriteCard(byte[] data)
        {
            istr = rf_card(icdev, 1, snr);
            if (istr != 0)
            {
                OnSendMessage("卡激活失败.");
            }
            else
            {
                rf_write(icdev, 2, data);
                rf_halt(icdev);
                rf_beep(icdev, 50);
            }
        }
        #endregion
    }
}
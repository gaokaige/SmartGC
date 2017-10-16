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
        /// <summary>
        /// 将16进制数转换为ASCII字符
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="asc"></param>
        /// <param name="len"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 验密 大于15扇区用rf_authentication_2
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="mode">0</param>
        /// <param name="secnr">扇区号</param>
        /// <returns></returns>
        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_authentication", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_authentication(int icdev, int mode, int secnr);

        [DllImport("mwhrf_bj.dll", EntryPoint = "rf_authentication_2", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16 rf_authentication_2(int icdev, int mode, int keynr, int blocknr);
        /// <summary>
        /// 读取块的信息
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="blocknr">块号</param>
        /// <param name="databuff"></param>
        /// <returns></returns>
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
        public delegate void ReadCardHandler(string cardNo, string cardId);
        /// <summary>
        /// 读取卡片ID成功时发生
        /// </summary>
        public event ReadCardHandler OnReadCard;
        /// <summary>
        /// 读卡器设备id
        /// </summary>
        int icdev;
        byte[] cno = new byte[16];// 出场编号
        byte[] cid = new byte[16];// ID号
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
                //rf_beep(icdev, 10);
                //rf_beep(icdev, 10);
            }
        }
        /// <summary>
        /// 读卡获取卡ID
        /// </summary>
        /// <returns></returns>
        public void GetCardNoAndID()
        {
            try
            {
                string cardNo = string.Empty;

                istr = rf_card(icdev, 1, cno);
                if (istr != 0)
                {
                    OnSendMessage("卡激活失败,请将卡片放置于读卡区域.");
                }
                else
                {
                    int au = rf_authentication(icdev, 0, 0);
                    Console.WriteLine(au);
                    byte[] ascid = new byte[32];
                    byte[] ascno = new byte[32];

                    int ar = rf_read(icdev, 0, cno);
                    if (ar != 0)
                    {
                        OnSendMessage("读卡No.失败");
                        return;
                    }
                    hex_a(cno, ascno, 16);
                    string no = System.Text.Encoding.Default.GetString(ascno);
                    int br = rf_read(icdev, 2, cid);//读id
                    if (br != 0)
                    {
                        OnSendMessage("读卡ID失败");
                        return;
                    }
                    // 如果不是CA开头，也不是0
                    if (!(cid[0] == 0xCA
                        || CheckValue(cid)))
                    {
                        OnSendMessage("无效卡请重试");
                        return;
                    }

                    hex_a(cid, ascid, 16);
                    string id = System.Text.Encoding.Default.GetString(ascid);
                    id = id.Substring(0, 10);
                    rf_beep(icdev, 10);
                    OnSendMessage("读卡成功,卡ID：" + id);
                    OnReadCard(no, id);
                }
            }
            catch (Exception ex)
            {
                OnSendMessage("读卡失败:" + ex.Message);
            }
            finally
            {
                rf_halt(icdev);
            }
        }
        public string GetCardNo()
        {
            string cardNo = string.Empty;
            try
            {
                istr = rf_card(icdev, 1, cno);
                if (istr != 0)
                {
                    OnSendMessage("卡激活失败,请将卡片放置于读卡区域.");
                }
                else
                {
                    int au = rf_authentication(icdev, 0, 0);
                    Console.WriteLine(au);
                    
                    byte[] ascno = new byte[32];

                    int ar = rf_read(icdev, 0, cno);
                    if (ar != 0)
                    {
                        OnSendMessage("读卡No.失败");
                    }
                    else
                    {
                        hex_a(cno, ascno, 16);
                        cardNo = System.Text.Encoding.Default.GetString(ascno);
                        rf_beep(icdev, 10);
                    }
                }
                return cardNo;

            }
            catch (Exception ex)
            {
                OnSendMessage("读卡失败:" + ex.Message);
                return cardNo;
            }
            finally
            {
                rf_halt(icdev);
            }
        }

        private bool CheckValue(byte[] cid)
        {
            bool result = true;
            foreach (byte item in cid)
            {
                if (item != 0x0)
                    result = false;
            }
                return result;
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

        public bool WriteCard(byte[] data)
        {
            bool result = true;
            istr = rf_card(icdev, 1, cno);
            if (istr != 0)
            {
                result = false;
                OnSendMessage("卡激活失败.");
            }
            else
            {
                int au = rf_authentication(icdev, 0, 0);
                if (au == 0)
                {
                    if (0 != rf_write(icdev, 2, data))
                        result = false;
                    rf_halt(icdev);
                    rf_beep(icdev, 50);
                }
            }
            return result;
        }
        #endregion
    }
}
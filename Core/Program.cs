using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Core.Comm;
using qzxxiEntity;
using System.Net;

namespace Core
{
    static class Program
    {
        public static Win.WinInput MIme = null;
        public static string HVID = "";//硬件id
        public static string ProductVer = "2.0.2";//软件版本
        public static Win.InfoFrm InfoFrm = null;
        public static bool CheckAppOK = true;
        public static System.Threading.Thread CheckTh = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
 
 

            Win.Login login = new Win.Login();
            login.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "login.png"));
            login.Wait = true;
            login.Show();
            int check =  CheckAppLong();
            if (check != 0)
            {
                login = new Win.Login(false, null);
                login.Show();
                if (check == 4)
                {
                    MessageBox.Show("加密狗文件不对!", "速录宝");
                }
                else if (check == 5)
                {
                    MessageBox.Show("加密狗文件内容不对!", "速录宝");
                }
                else
                    MessageBox.Show("加密狗不对,请插入正确的加密狗!", "速录宝");
                Application.Exit();
                return;
            }


            login.Wait = false;
            CheckTh = new System.Threading.Thread(new System.Threading.ThreadStart(CheckLong));
            CheckTh.IsBackground = true;
            CheckTh.Start();
 


 
            int iProcessNum = 0;
            foreach (Process singleProc in Process.GetProcesses())
            {
                if (singleProc.ProcessName == Process.GetCurrentProcess().ProcessName)
                {
                    iProcessNum += 1;
                }
            }

            if (iProcessNum <= 1)
            {
                //不要重复运行程序
                MIme = new Win.WinInput();
                Application.Run(MIme);

            }
           
        }

        [DllImport("kernel32.dll")]
        private static extern int GetVolumeInformation(string lpRootPathName, string lpVolumeNameBuffer, int nVolumeNameSize, ref int lpVolumeSerialNumber, int lpMaximumComponentLength, int lpFileSystemFlags, string lpFileSystemNameBuffer, int nFileSystemNameSize);

        #region lic
        private static string md5Miou(string str)
        {
            MD5 md1 = new MD5CryptoServiceProvider();
            byte[] buffer1 = Encoding.Unicode.GetBytes(str);
            byte[] buffer2 = md1.ComputeHash(buffer1);
            string text1 = null;
            for (int num1 = 0; num1 < buffer2.Length; num1++)
            {
                text1 = text1 + buffer2[num1].ToString("x");
            }
            return text1.ToUpper();

        }
        public static string MD5Crypt(string input)
        {
            HashAlgorithm hash1 = new MD5CryptoServiceProvider();
            hash1.Initialize();
            byte[] rawBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
            byte[] hash = hash1.ComputeHash(rawBytes);
            StringBuilder sb = new StringBuilder(hash.Length * 2 + (hash.Length / 8));
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(BitConverter.ToString(hash, i, 1));
            }
            return sb.ToString().ToUpper();
        }
        private static string GetSysdisMiou()
        {
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            string text1 = null;
            string text2 = null;
            string disp = Environment.GetFolderPath(Environment.SpecialFolder.System).ToUpper().Substring(0, 1) + @":\";
            GetVolumeInformation(disp, text1, 0x100, ref num1, num2, num3, text2, 0x100);
            return num1.ToString("x");
        }
        private static void creafMiou(string fn)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + fn))
            {
                File.Delete(Directory.GetCurrentDirectory() + fn);
            }

            using (StreamWriter writer1 = new StreamWriter(System.IO.Path.Combine(Directory.GetCurrentDirectory(), fn)))
            {
                writer1.WriteLine(md5Miou("MiouSystem") + MD5Crypt(GetSysdisMiou()) + md5Miou(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()));
            }
        }
        private static string LicChe()
        {

            try
            {
                string text1 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "License.lic");
                if (!File.Exists(text1))
                {
                    creafMiou("Reguser.lic");
                    return "未找到授权许可文件,已创建注册文件Reguser.lic,请向供应商申请一个授权许可";
                }
                else
                {
                    string text2 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Reguser.lic");
                    if (File.Exists(text2))
                    {
                        File.Delete(text2);
                    }
                }
                string text3 = MD5Crypt(GetSysdisMiou());
                string sSecretKey = getstr(text3);

                if (DecryptFile(text1, sSecretKey).Substring(md5Miou("MiouSystem").Length, text3.Length) != text3)
                {
                    File.Delete(text1);
                    return "无效许可,请向供应商重新申请有效授权许可";
                }
                else
                {
                    return "000000";
                }
            }
            catch { return "许可认证出错,请向供应商重新申请有效授权许可"; }
        }
        private static string DecryptFile(string sInputFilename, string sKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = Encoding.ASCII.GetBytes(sKey);
            DES.IV = Encoding.ASCII.GetBytes("hjxsgf,v");

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            string v = new StreamReader(cryptostreamDecr).ReadToEnd();
            fsread.Flush();
            fsread.Close();
            return v;


        }
        private static string getstr(string str)
        {
            string rs = "";
            #region get
            for (int i = 0; i < 32; i++)
            {
                if (i == 0)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 1)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 2)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 3)
                {

                }
                if (i == 4)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 5)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 6)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 7)
                {

                }
                if (i == 8)
                {

                }
                if (i == 9)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 10)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 11)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 12)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 13)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 14)
                {

                }
                if (i == 15)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 16)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 17)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 18)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 19)
                {

                }
                if (i == 20)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 21)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 22)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 23)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 24)
                {

                }
                if (i == 25)
                {

                }
                if (i == 26)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 27)
                {

                }
                if (i == 28)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 29)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 30)
                {
                    rs = rs + str.Substring(i, 1);
                }
                if (i == 31)
                {
                    rs = rs + str.Substring(i, 1);
                }

            }
            #endregion
            return rs;
        }
        #endregion
 
        // 函数引用声明
        [DllImport("ViKey")]
        private static extern uint VikeyFind(ref uint pdwCount);
        [DllImport("ViKey")]
        private static extern uint VikeyGetHID(ushort Index, ref uint pdwHID);
 
        private static int CheckAppLong()
        {
      
            int rev = 0;
             CheckAppOK = rev == 0;
             return rev;
            try
            {
                //查找加密狗
                uint Count = 0;
                uint HID = 0;
                string Clic = "";
                #region ini HVID
                if (string.IsNullOrEmpty(HVID))
                {
                    string text1 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "SharpOut.dll");
                    if (!File.Exists(text1))
                    {
                        return 4;
                    }
                    string licst = Comm.Security.DecryptCommon(File.ReadAllText(text1, Encoding.UTF8), "P@ssW@rdqz@sr");
                    HVID = licst.Split('|')[0];
                    Clic = licst.Split('|')[1];
                    if (string.Compare(Comm.Security.GetMD5(Comm.Security.EncryptCommon(HVID)), Clic, true) != 0)
                    {
                        return 5;
                    }
                }
                #endregion
                uint retcode = VikeyFind(ref Count);
                if (retcode != 0)
                {
                    rev= 1;
                }
                for (ushort j = 0; j < Count; j++)
                {
                    //获取加密狗硬件序列号(HID)
                    retcode = VikeyGetHID(j, ref HID);
                    if (retcode != 0)
                    {

                        rev= 2;
                    }
                    if (HID.ToString() != HVID)
                    {

                        rev= 3;
                    }
                }
                if (rev != 0)
                {
                    //网络验证授权
                    var o = CheckLic("http://srapi.qzxxi.com/api/ZQRequest");
                    rev = o.CheckFlag;
                }
                CheckAppOK = rev == 0;
                return rev;
            }
            catch { CheckAppOK = false; rev = 3; return rev; }
        }
        private static void CheckLong()
        {
            System.Threading.Thread.Sleep(1000);
            int checkount = 0;
            int checkflag = 1;
            while (true)
            {
                try
                {
                ce:
                    checkflag = CheckAppLong();
                    if (checkflag != 0)
                    {
                        checkount++;
                        if (checkount < 4)
                        {
                            System.Threading.Thread.Sleep(1000);
                            goto ce;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        checkflag = 0;
                        checkount = 0;
                    }
                }
                catch { checkflag = 3; }
                //System.Threading.Thread.Sleep(3000);
                System.Threading.Thread.Sleep(1000 * 60 * 4);
            }

            if (checkflag != 0)
            {
                if (MIme != null)
                {
                    Win.WinInput.Input.indexComplete = false;
                }
                var login = new Win.Login(false, null);
                login.Show();
                MessageBox.Show("加密狗不对,请插入正确的加密狗!", "速录宝");
                if (MIme != null)
                {
                    Win.WinInput.Input.indexComplete = false;
                    MIme.ExitSelect(null, null);
                }
                Application.Exit();
            }

        }

        /// <summary>
        /// check 授权
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static CheckEntity CheckLic(string url)
        {
            CheckEntity c = new CheckEntity();
            c.HVID = Program.HVID;
            c.CheckFlag = 1;
            c.ProVer = Program.ProductVer;
            DateTime dt = DateTime.Now;
            c.SpanTime = dt;
            try
            {
                RequestEntity r = new RequestEntity();
                r.OptCommand = OptCom.CheckLic.ToString();
                r.Content = ApiClient.ToJson(c);
                r.Content = Security.EncryptCommon(r.Content);
                IAASResponse reponse = IAASRequest.Reauest(url, RequestMethod.POST, "", "", ApiClient.ToJson(r));
                if (reponse.StatusCode == HttpStatusCode.OK)
                {
                    c = ApiClient.JsonToObj<CheckEntity>(reponse.Content);
                    if (((TimeSpan)(dt - c.SpanTime)).TotalMilliseconds > 50)
                        c.CheckFlag = 1;
                }
                else
                    c.CheckFlag = 1;
            }
            catch
            {
                c.CheckFlag = 1;

            }
            return c;
        }
    }

     
}

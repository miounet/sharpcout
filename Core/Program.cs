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
 
        public static string ProductVer = "1.0.1";//软件版本
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
     
    }

     
}

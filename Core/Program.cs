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
 
        public static string ProductVer = "2.3.7";//软件版本
 
  
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
 
            login.Wait = false;
 
 
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
 
 
 
    }

     
}

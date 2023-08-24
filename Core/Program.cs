using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Linq;
using Core.Comm;
using qzxxiEntity;
using System.Net;
using System.Threading.Tasks;

namespace Core
{
    static class Program
    {
        public static Win.WinInput MIme = null;
 
        public static string ProductVer = "3.1.2";//软件版本

        public class vvclase
        {
            public string txt { get; set; }
            public long num { get; set; }
        }
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
                Task stask = new Task(() =>
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(180000);
                        try
                        {
                            if (!string.IsNullOrEmpty(Base.InputMode.AppPath) && Base.InputMode.autodata)
                            {
                                File.WriteAllLines(System.IO.Path.Combine(Base.InputMode.AppPath, "dict", Base.InputMode.CDPath, "mapdatacount.txt")
                                    , new string[] { Win.WinInput.savemapdata() }, Encoding.UTF8);
                            }
                        }
                        catch { }
                    }
                });

                stask.Start();
                //不要重复运行程序
                MIme = new Win.WinInput();
                Application.Run(MIme);

            }


        }
 
    }

     
}

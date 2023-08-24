using Core.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Core.Comm
{
    /// <summary>
    /// 任务线程
    /// </summary>
    public class PlanThread
    {
        private static bool running = true;
        private static Thread updateTh = null;
        private static Thread updateCloudDictTh = null;
        private static Thread getMyInfo = null;
        public static void Start()
        {
            running = true;
#if !DEBUG
 
          

            //updateCloudDictTh = new Thread(new ThreadStart(UpdateCloudDict));
            //updateCloudDictTh.IsBackground = true;
            //updateCloudDictTh.Start();
#endif
            //getMyInfo = new Thread(new ThreadStart(GetMyInfo));
            //getMyInfo.IsBackground = true;
            //getMyInfo.Start();

            updateTh = new Thread(new ThreadStart(UpdateDict));
            updateTh.IsBackground = true;
            updateTh.Start();
        }

        public static void Stop()
        {
            running = false;

            if (updateTh != null)
            {
                try
                {
                    updateTh.Abort();
                }
                catch { }
            }
            if (updateCloudDictTh != null)
            {
                try
                {
                    updateCloudDictTh.Abort();
                }
                catch { }
            }

            if (getMyInfo != null)
            {
                try
                {
                    getMyInfo.Abort();
                }
                catch { }
            }
        }

        private static void UpdateDict()
        {
            while (running)
            {
                Thread.Sleep(10000);
                try
                {
                    if (!Win.WinInput.Input.indexComplete)
                        continue;
                    if (!InputMode.AutoUpdate)
                    {
                        Thread.Sleep(1000 * 60);
                        continue;
                    }
                    UpdataSoft();

                    UpdataDict();

                    Thread.Sleep(1000 * 60 * 60);
                }
                catch { }
                
            }
        }

        public static void UpdataSoft(bool flag = false)
        {
            try
            {
                UpdateSoftEnt data = ApiClient.GetEnt(Win.WinInput.ApiUrl);
                if (Program.ProductVer != data.ProductVer)
                {
                    //下载最新版本
                    #region 实现升级
                    bool updataok = false;
                    string du = data.DownUrl;
                    ///下载
                    if (MessageBox.Show("有新版本,是否现在升级!\r\n", "速录宝-有新版本", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Win.Login info = new Win.Login(false, Core.Win.WinInput.Input, 0);
                        info.TopMost = true;
                        info.Show();

                        try
                        {
                            WebClient client = new WebClient();
                            client = new WebClient();
                            string filename = System.IO.Path.Combine(InputMode.AppPath, "updatesoft.zip");
                            client.DownloadFile(du, filename);
                            Thread.Sleep(200);
                            updataok = true;
                        }
                        catch
                        {
                            updataok = false;
                        }

                    }

                    if (updataok)
                    {
                        //关闭速录软件，打开升级解压程序

                        System.Diagnostics.Process.Start(System.IO.Path.Combine(InputMode.AppPath, "update.exe"));

                        Program.MIme.ExitSelect(null, null);
                    }
                    #endregion
                    return;
                }
                else if (flag)
                {
                    MessageBox.Show("没有检测到要更新的程序!");
                    return;
                }
            }
            catch (Exception ex) { string ss = ex.ToString(); }
        }

        public static void UpdataDict(bool flag = false)
        {
            try
            {

                UpdateEnt data = ApiClient.GetUpdateInfo(Win.WinInput.ApiDictUrl);
                if (Win.WinInput.DictVersion != data.DictVersion)
                {
                    //现在就可升级词库
                    #region 实现升级
                    bool updataok = false;
                    string du = data.DictDownUrl;
                    ///下载新词库
                    if (MessageBox.Show("有新词库,是否下载!\r\n", "有新词库", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Win.Login info = new Win.Login(false, Core.Win.WinInput.Input, 0);
                        info.TopMost = true;
                        info.Show();
                      
                        try
                        {

                            WebClient client = new WebClient();
                            string filename = System.IO.Path.Combine(InputMode.AppPath, DateTime.Now.Ticks.ToString() + ".zip");
                       
                            client.DownloadFile(du, filename);
                            Thread.Sleep(200);

                            if (File.Exists(filename))
                            {
                                //下载完成解压
                                updataok = ZipClass.UnZip(filename, System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath),"");
                                Thread.Sleep(200);
                                File.Delete(filename);//解压完成，删除下载的文件
                                updataok = true;
                                info.Close();

                                Program.MIme.InputIni();
                               
                            }
                        }
                        catch
                        {
                            info.Close();
                            updataok = false;
                        }
                        
                    }

                  
                    #endregion
                    return;
                }
                else if (flag)
                {
                    MessageBox.Show("没有检测到要更新的词库!");
                    return;
                }

            }
            catch (Exception ex) { string ss = ex.ToString(); }
        }
        //private static void GetMyInfo()
        //{
        //    while (running)
        //    {
        //        Thread.Sleep(40000);
        //        try
        //        {
        //            if (!Win.WinInput.Input.indexComplete)
        //            {
        //                Thread.Sleep(2000);
        //                continue;
        //            }
 
        //            qzxxiEntity.InfoEntity data = ApiClient.GetMyInfo(Win.WinInput.ApiUrl);
        //            if (!string.IsNullOrEmpty(data.InfoStr))
        //            {
        //                Program.InfoFrm.SendMsg(data);
        //            }
                   
        //        }
        //        catch { Thread.Sleep(1000 * 60 * 20); }

        //        Thread.Sleep(1000 * 60 * 2);
        //    }
        //}


        //private static void UpdateCloudDict()
        //{
        //    while (running)
        //    {
        //        Thread.Sleep(20000);
        //        try
        //        {
        //            if (!Win.WinInput.Input.indexComplete)
        //            {
        //                Thread.Sleep(2000);
        //                continue;
        //            }
        //            if (!InputMode.OpenCould)
        //            {
        //                Thread.Sleep(1000 * 60 * 20);
        //                continue;
        //            }
        //            qzxxiEntity.ColdDictEntity data = ApiClient.GetCloudDictUpdate(Win.WinInput.ApiUrl);
        //            if (data.Value.Count > 0)
        //            {
        //                Win.WinInput.Input.ClouddDit.AddRange(data.Value);
        //                Program.MIme.SaveCloudDict();
        //                Thread.Sleep(1000 * 60 * 20);
        //            }
        //            if (!data.NextHave) Thread.Sleep(1000 * 60 * 60 * 8);
        //        }
        //        catch { Thread.Sleep(1000 * 60 * 20); }
        //    }
        //}
    }
}

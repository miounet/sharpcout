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
 
            updateTh = new Thread(new ThreadStart(UpdateDict));
            updateTh.IsBackground = true;
            updateTh.Start();

            updateCloudDictTh = new Thread(new ThreadStart(UpdateCloudDict));
            updateCloudDictTh.IsBackground = true;
            updateCloudDictTh.Start();
#endif
            getMyInfo = new Thread(new ThreadStart(GetMyInfo));
            getMyInfo.IsBackground = true;
            getMyInfo.Start();
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

                if (!Win.WinInput.Input.indexComplete)
                    continue;
                if (!InputMode.AutoUpdate)
                {
                    Thread.Sleep(1000 * 60 * 30);
                    continue;
                }
                UpdataSoft();

             
                Thread.Sleep(1000 * 60 * 60*12);
            }
        }

        public static void UpdataSoft(bool flag=false)
        {
            try
            {
                
                qzxxiEntity.UpdateDictEnt data = ApiClient.GetDictUpdate(Win.WinInput.ApiUrl);
                if (data.UpdateFalg == 2)
                {
                    //现在就可升级词库
                    #region 实现升级
                    bool updataok = false;
                    foreach (var du in data.UpdateUrl)
                    {
                        ///下载新词库
                        if (MessageBox.Show("有新词库,是否下载!\r\n", "巧指速录-有新词库", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Win.Login info = new Win.Login(false, Core.Win.WinInput.Input, 0);
                            info.TopMost = true;
                            info.Show();
                            ApiClient.StartDictUpdate(Win.WinInput.ApiUrl);//开始download
                            try
                            {

                                WebClient client = new WebClient();
                                string filename = System.IO.Path.Combine(Win.WinInput.Input.AppPath, DateTime.Now.Ticks.ToString() + ".zip");
                                client.DownloadFile(du, filename);
                                Thread.Sleep(200);

                                if (File.Exists(filename))
                                {
                                    //下载完成解压
                                    updataok = ZipClass.UnZip(filename, System.IO.Path.Combine(Win.WinInput.Input.AppPath, "dict"), string.Empty);
                                    Thread.Sleep(500);
                                    File.Delete(filename);//解压完成，删除下载的文件
                                    updataok = true;
                                }
                            }
                            catch
                            {
                                updataok = false;
                            }
                            finally
                            {
                                ApiClient.EndDictUpdate(Win.WinInput.ApiUrl);//download完成
                            }
                        }
                    }
                    if (updataok)
                    {
                        Win.WinInput.DictVersion = data.DictVersion;
                        Program.MIme.SaveSetting();
                    }
                    #endregion
                    return;
                }
                else if (data.UpdateFalg == 3)
                {
                    //下载最新版本
                    #region 实现升级
                    bool updataok = false;
                    foreach (var du in data.UpdateUrl)
                    {
                        ///下载
                        if (MessageBox.Show("有新版本,是否现在升级!\r\n", "巧指速录-有新版本", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Win.Login info = new Win.Login(false, Core.Win.WinInput.Input, 0);
                            info.TopMost = true;
                            info.Show();
                            ApiClient.StartDictUpdate(Win.WinInput.ApiUrl);//开始download
                            try
                            {

                                WebClient client = new WebClient();
                                string filename = System.IO.Path.Combine(Win.WinInput.Input.AppPath, "updatesoft.zip");
                                client.DownloadFile(du, filename);
                                Thread.Sleep(200);
                                updataok = true;
                            }
                            catch
                            {
                                updataok = false;
                            }
                            finally
                            {
                                ApiClient.EndDictUpdate(Win.WinInput.ApiUrl);//download完成
                            }
                        }
                    }
                    if (updataok)
                    {
                        //关闭速录软件，打开升级解压程序

                        System.Diagnostics.Process.Start(System.IO.Path.Combine(Win.WinInput.Input.AppPath, "update.exe"));

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
        private static void GetMyInfo()
        {
            while (running)
            {
                Thread.Sleep(40000);
                try
                {
                    if (!Win.WinInput.Input.indexComplete)
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
 
                    qzxxiEntity.InfoEntity data = ApiClient.GetMyInfo(Win.WinInput.ApiUrl);
                    if (!string.IsNullOrEmpty(data.InfoStr))
                    {
                        Program.InfoFrm.SendMsg(data);
                    }
                   
                }
                catch { Thread.Sleep(1000 * 60 * 20); }

                Thread.Sleep(1000 * 60 * 2);
            }
        }


        private static void UpdateCloudDict()
        {
            while (running)
            {
                Thread.Sleep(20000);
                try
                {
                    if (!Win.WinInput.Input.indexComplete)
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                    if (!InputMode.OpenCould)
                    {
                        Thread.Sleep(1000 * 60 * 20);
                        continue;
                    }
                    qzxxiEntity.ColdDictEntity data = ApiClient.GetCloudDictUpdate(Win.WinInput.ApiUrl);
                    if (data.Value.Count > 0)
                    {
                        Win.WinInput.Input.ClouddDit.AddRange(data.Value);
                        Program.MIme.SaveCloudDict();
                        Thread.Sleep(1000 * 60 * 20);
                    }
                    if (!data.NextHave) Thread.Sleep(1000 * 60 * 60 * 8);
                }
                catch { Thread.Sleep(1000 * 60 * 20); }
            }
        }
    }
}

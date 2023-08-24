using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Base;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using Core.Comm;
using Core.Config;
using qzxxiEntity;
using System.Drawing.Drawing2D;

namespace Core.Win
{
    /// <summary>
    /// widnows下的实现方式
    /// </summary>
    public class WinInput :System.Windows.Forms.Form,IOut
    {
        /// <summary>
        /// 输入法栏
        /// </summary>
        public static InputMode Input = new InputMode();
       
        public static string ApiUrl = "https://gitee.com/miounet/sharpcout/blob/master/sv.txt";
        public static string ApiDictUrl = "https://gitee.com/miounet/sharpcout/blob/master/dv.txt";
        public static string DictVersion = "1.0.1";
        public static IntPtr ForegroundWindow = IntPtr.Zero;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private Label label4;

        /// <summary>
        /// 输入法汉字候选框
        /// </summary>
        public static InputStatusFrm InputStatus = new InputStatusFrm(Input);
        private ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
        private Timer timer1;
        public static ImageInput ImageInput = new ImageInput();

        public static YAMLHelp settingYaml = new YAMLHelp("");
        ////统计
        //int aa = 0;
        //int aA = 0;
        //int Aa = 0;
        //int AA = 0;

        public bool LoadMasterDict()
        {

 
            var mdict = Function.Decrypt(Input.MasterDitPath,false);
            bool orderby = true;
            if (File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "Setting.shp")))
            {
                var setting = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "Setting.shp"), Encoding.UTF8);//读配置

                orderby = string.IsNullOrEmpty(SetInfo.GetValue("orderby", setting)) ? true : bool.Parse(SetInfo.GetValue("orderby", setting));
                InputStatusFrm.three_no2 = string.IsNullOrEmpty(SetInfo.GetValue("three_no2", setting)) ? false : bool.Parse(SetInfo.GetValue("three_no2", setting));
            }
            // Input.MasterDit = mdict.Where(w => w.Length > 0).OrderBy(o => o.Substring(0, 1)).ThenBy(t=>t.Split(' ')[0].Length).ToArray();
            if (orderby)
                Input.MasterDit = mdict.Where(w => w.Length > 0).OrderBy(o => o.Substring(0, 1)).ToArray();
            else
                Input.MasterDit = mdict;

            //File.WriteAllLines(System.IO.Path.Combine(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")), "dict.txt"), Input.MasterDit, Encoding.UTF8);

            //初始化索引
            Input.CreateIndex(Input.MasterDit, ref Input.DictIndex.IndexList, 1, 0, Input.MasterDit.Length);

            
            //string incode = "qwertyuiopasdfghjkl;zxcvbnm,./QWERTYUIOPASDFGHJKL:ZXCVBNM<?!";
            //List<string> codell = new List<string>();
            //for(int i = 0; i < incode.Length; i++)
            //{
            //    for (int ii = 0; ii < incode.Length; ii++)
            //    {
            //        codell.Add(incode.Substring(i, 1) + incode.Substring(ii, 1));
            //    }
            //}
            //StringBuilder ssb = new StringBuilder();
            //foreach (var item in codell)
            //{
            //    string[] mdict11 = null;
            //    PosIndex poi = Input.DictIndex.GetPos(item, ref mdict11);
            //    if (poi == null)
            //    {
            //        ssb.Append(item+"\r\n");
            //    }

            //}
            //File.WriteAllText(System.IO.Path.Combine(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")), "empty.txt"), ssb.ToString(), Encoding.UTF8);

            LoadProDict();

            //装载拼音字库
            if (File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", "pinyi.shp")))
            {
                Input.PinYi = new Dictionary<string, string>();
                var sdic = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", "pinyi.shp")).Select(line => line.Split(' '));
                foreach (var item in sdic)
                {
                    if (string.IsNullOrEmpty(item[0]) || string.IsNullOrEmpty(item[1])) continue;
                    if (!Input.PinYi.ContainsKey(item[0]))
                    {
                        Input.PinYi.Add(item[0], item[1]);
                    }
                    else
                    {
                        Input.PinYi[item[0]] = Input.PinYi[item[0]] + " " + item[1];
                    }
                }
               
            }

            Input.CfDict = new Dictionary<string, string>();
            //装载拆分表数据
            if (File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "cf.txt")))
            {
                var sdic = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "cf.txt")).Select(line => line.Split('\t'));
                foreach (var item in sdic)
                {
                    if (string.IsNullOrEmpty(item[0]) || string.IsNullOrEmpty(item[1])) continue;
                    if (!Input.CfDict.ContainsKey(item[0]))
                    {
                        Input.CfDict.Add(item[0], item[1]);
                    }
                    else
                    {
                        Input.CfDict[item[0]] = item[1];
                    }
                }

            }

            //装载繁体数据
            if (File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict",  "s2t.txt")))
            {
                var sdic = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", "s2t.txt")).Select(line => line.Split('='));
                foreach (var item in sdic)
                {
                    if (string.IsNullOrEmpty(item[0]) || string.IsNullOrEmpty(item[1])) continue;
                    if (!Input.S2TDict.ContainsKey(item[0]))
                    {
                        Input.S2TDict.Add(item[0], item[1]);
                    }
                    else
                    {
                        Input.S2TDict[item[0]] =  item[1];
                    }
                }

            }


          
            return true;
        }


        public bool LoadProDict()
        {
            try
            {
                #region 加载专业词库
                string cof = File.ReadAllText(System.IO.Path.Combine(Input.ProDitPath, "load.config"), Encoding.UTF8);
                if (!string.IsNullOrEmpty(cof))
                {
                    try
                    {
                        List<UpdateDictEnt> pl = ApiClient.JsonToObj<List<UpdateDictEnt>>(cof);
                        List<string> alld = new List<string>();
                        var files = Directory.GetFiles(Input.ProDitPath);
                        bool haveload = false;
                        foreach (var f in files)
                        {
                            var aa = pl.Find(p => f.EndsWith(p.Dictid));
                            if (aa != null && aa.Select)
                            {
                                haveload = true;
                                alld.AddRange(Function.Decrypt(f).ToList());
                            }
                        }
                        if (haveload)
                        {
                            Input.ProDit = alld.Where(w => w.Length > 0).OrderBy(o => o.Substring(0, 1)).ToArray();
                            Input.CreateIndex(Input.ProDit, ref Input.DictIndex.ProList, 1, 0, Input.ProDit.Length);
                        }
                        else
                        {
                            Input.DictIndex.ProList = new List<PosIndex>();
                            Input.ProDit = null;
                        }
                    }
                    catch { }
                }
                #endregion
            }
            catch { }
            return true;
        }

        public static Image Bkimg = null;

        public bool LoadSkin()
        {
            //this.pictureBox1.Location = new System.Drawing.Point(2,2);
            this.pictureBox1.Size = new System.Drawing.Size(18, 19);
 
            if (string.IsNullOrEmpty(InputMode.PFPath))
            {
                Bkimg = Image.FromFile(System.IO.Path.Combine(InputMode.AppPath, "skin", Input.GetSkinBKImg()));
                pictureBox1.BackgroundImage = Image.FromFile(System.IO.Path.Combine(InputMode.AppPath, "log32.ico"));   
            }
            else
            {
                Bkimg = Image.FromFile(System.IO.Path.Combine(InputMode.AppPath, "skin", InputMode.PFPath, Input.GetSkinBKImg()));
                pictureBox1.BackgroundImage = Image.FromFile(System.IO.Path.Combine(InputMode.AppPath, "skin", InputMode.PFPath, "log32.ico"));
            }
            this.BackgroundImage = Bkimg;



            return true;
        }

        /// <summary>
        /// 装载用户词库
        /// </summary>
        /// <returns></returns>
        public bool LoadUserDict()
        {
            if (!File.Exists(Input.UserDitPath))
            {
                File.WriteAllLines(Input.UserDitPath, new string[] { "KomM 空明码" }, Encoding.UTF8);
            }
            Input.UserDit = File.ReadAllLines(Input.UserDitPath, Encoding.UTF8);//用户词库不用加解密


            if (File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", "pinyin.txt")))
                Input.ClouddDit = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", "pinyin.txt"), Encoding.UTF8).ToList<string>();//使用临时拼音词库
            else
                Input.ClouddDit = File.ReadAllLines(Input.CloundDitPath, Encoding.UTF8).ToList<string>();//cloud词库不用加解密

            return true;
        }

        public bool LoadEnDict()
        {
            Input.EnDit = Function.Decrypt(Input.EnDitPath);
            Input.CreateIndex(Input.EnDit, ref Input.DictIndex.EnList, 1, 0, Input.EnDit.Length);
            return true;
        }

        public bool LoadSettting()
        {
            var setting = File.ReadAllLines(Input.SettingPath, Encoding.UTF8);//读配置

            InputMode.OpenCould = string.IsNullOrEmpty(SetInfo.GetValue("OpenCould", setting)) ? false : bool.Parse(SetInfo.GetValue("OpenCould", setting));
            InputMode.AutoUpdate = string.IsNullOrEmpty(SetInfo.GetValue("AutoUpdate", setting)) ? false : bool.Parse(SetInfo.GetValue("AutoUpdate", setting));
            InputMode.AutoRun = string.IsNullOrEmpty(SetInfo.GetValue("AutoRun", setting)) ? false : bool.Parse(SetInfo.GetValue("AutoRun", setting));
            curTrac = string.IsNullOrEmpty(SetInfo.GetValue("CurTrac", setting)) ? false : bool.Parse(SetInfo.GetValue("CurTrac", setting));
            curMouseTrac = string.IsNullOrEmpty(SetInfo.GetValue("curMouseTrac", setting)) ? false : bool.Parse(SetInfo.GetValue("curMouseTrac", setting));
            InputMode.OpenLink = string.IsNullOrEmpty(SetInfo.GetValue("OpenLink", setting)) ? false : bool.Parse(SetInfo.GetValue("OpenLink", setting));
            InputMode.OpenAltSelect = string.IsNullOrEmpty(SetInfo.GetValue("OpenAltSelect", setting)) ? false : bool.Parse(SetInfo.GetValue("OpenAltSelect", setting));
            InputMode.SkinHeith = string.IsNullOrEmpty(SetInfo.GetValue("SkinHeith", setting)) ? 46 : int.Parse(SetInfo.GetValue("SkinHeith", setting));
            InputMode.PageSize = string.IsNullOrEmpty(SetInfo.GetValue("PageSize", setting)) ? 6 : int.Parse(SetInfo.GetValue("PageSize", setting));
            InputMode.SkinFontSize = string.IsNullOrEmpty(SetInfo.GetValue("SkinFontSize", setting)) ? 13 : int.Parse(SetInfo.GetValue("SkinFontSize", setting));
            InputMode.Skinbstring = Color.FromArgb(int.Parse(SetInfo.GetValue("Skinbstring", setting)));
            InputMode.Skinbcstring = Color.FromArgb(int.Parse(SetInfo.GetValue("Skinbcstring", setting)));
            InputMode.Skinfbcstring = Color.FromArgb(int.Parse(SetInfo.GetValue("Skinfbcstring", setting)));
            InputMode.SkinBack = string.IsNullOrEmpty(SetInfo.GetValue("SkinIndex", setting)) ? Color.FromArgb(22, 79, 141) : Color.FromArgb(int.Parse(SetInfo.GetValue("SkinBack", setting)));
            InputMode.SkinIndex = string.IsNullOrEmpty(SetInfo.GetValue("SkinIndex", setting)) ? 0 : int.Parse(SetInfo.GetValue("SkinIndex", setting));
            InputMode.SkinFontName = string.IsNullOrEmpty(SetInfo.GetValue("SkinFontName", setting)) ? "宋体" : SetInfo.GetValue("SkinFontName", setting);
            InputMode.CDPath = SetInfo.GetValue("CDPath", setting);
            InputMode.ZFPath = string.IsNullOrEmpty(SetInfo.GetValue("ZFPath", setting)) ? "" : SetInfo.GetValue("ZFPath", setting);
            InputMode.PFPath = string.IsNullOrEmpty(SetInfo.GetValue("PFPath", setting)) ? "" : SetInfo.GetValue("PFPath", setting);
            InputMode.SingleInput = string.IsNullOrEmpty(SetInfo.GetValue("SingleInput", setting)) ? false : bool.Parse(SetInfo.GetValue("SingleInput", setting));
            InputMode.txtla = string.IsNullOrEmpty(SetInfo.GetValue("txtla", setting)) ? "2" : SetInfo.GetValue("txtla", setting);
            InputMode.txtlas = string.IsNullOrEmpty(SetInfo.GetValue("txtlas", setting)) ? "3" : SetInfo.GetValue("txtlas", setting);
            InputMode.txtra = string.IsNullOrEmpty(SetInfo.GetValue("txtra", setting)) ? "4" : SetInfo.GetValue("txtra", setting);
            InputMode.txtras = string.IsNullOrEmpty(SetInfo.GetValue("txtras", setting)) ? "5" : SetInfo.GetValue("txtras", setting);
            InputMode.txtlra = string.IsNullOrEmpty(SetInfo.GetValue("txtlra", setting)) ? "_" : SetInfo.GetValue("txtlra", setting);
            InputMode.txtlras = string.IsNullOrEmpty(SetInfo.GetValue("txtlras", setting)) ? "@" : SetInfo.GetValue("txtlras", setting);
            InputMode.right3_out = string.IsNullOrEmpty(SetInfo.GetValue("right3_out", setting)) ? true : bool.Parse(SetInfo.GetValue("right3_out", setting));
            InputMode.pinyin = string.IsNullOrEmpty(SetInfo.GetValue("pinyin", setting)) ? false : bool.Parse(SetInfo.GetValue("pinyin", setting));
            InputMode.closebj = string.IsNullOrEmpty(SetInfo.GetValue("closebj", setting)) ? false : bool.Parse(SetInfo.GetValue("closebj", setting));
            InputMode.autopos = string.IsNullOrEmpty(SetInfo.GetValue("autopos", setting)) ? false : bool.Parse(SetInfo.GetValue("autopos", setting));
            InputMode.bjzckgsp = string.IsNullOrEmpty(SetInfo.GetValue("bjzckgsp", setting)) ? false : bool.Parse(SetInfo.GetValue("bjzckgsp", setting));
            InputMode.omeno = string.IsNullOrEmpty(SetInfo.GetValue("omeno", setting)) ? false : bool.Parse(SetInfo.GetValue("omeno", setting));
            InputMode.zsallmap = string.IsNullOrEmpty(SetInfo.GetValue("zsallmap", setting)) ? false : bool.Parse(SetInfo.GetValue("zsallmap", setting));
            InputMode.zsmode1 = string.IsNullOrEmpty(SetInfo.GetValue("zsmode1", setting)) ? 0 : int.Parse(SetInfo.GetValue("zsmode1", setting));
            Input.IsChinese = string.IsNullOrEmpty(SetInfo.GetValue("IsChinese", setting)) ? (ushort)1 : ushort.Parse(SetInfo.GetValue("IsChinese", setting));
            InputMode.datacf = string.IsNullOrEmpty(SetInfo.GetValue("datacf", setting)) ? false : bool.Parse(SetInfo.GetValue("datacf", setting));
            InputMode.imghh = string.IsNullOrEmpty(SetInfo.GetValue("imghh", setting)) ? true : bool.Parse(SetInfo.GetValue("imghh", setting));
            InputMode.oneoutbj = string.IsNullOrEmpty(SetInfo.GetValue("oneoutbj", setting)) ? true : bool.Parse(SetInfo.GetValue("oneoutbj", setting));
            InputMode.ftfzxs = string.IsNullOrEmpty(SetInfo.GetValue("ftfzxs", setting)) ? false : bool.Parse(SetInfo.GetValue("ftfzxs", setting));
            InputMode.dcxz = string.IsNullOrEmpty(SetInfo.GetValue("dcxz", setting)) ? true : bool.Parse(SetInfo.GetValue("dcxz", setting));
            InputMode.iselect = string.IsNullOrEmpty(SetInfo.GetValue("iselect", setting)) ? true : bool.Parse(SetInfo.GetValue("iselect", setting));
            InputMode.onesp = string.IsNullOrEmpty(SetInfo.GetValue("onesp", setting)) ? true : bool.Parse(SetInfo.GetValue("onesp", setting));
            InputMode.select3 = string.IsNullOrEmpty(SetInfo.GetValue("select3", setting)) ? false : bool.Parse(SetInfo.GetValue("select3", setting));
            InputMode.spaceaout = string.IsNullOrEmpty(SetInfo.GetValue("spaceaout", setting)) ? 0 : int.Parse(SetInfo.GetValue("spaceaout", setting));
            InputMode.autodata = string.IsNullOrEmpty(SetInfo.GetValue("autodata", setting)) ? true : bool.Parse(SetInfo.GetValue("autodata", setting));
            InputMode.useregular = string.IsNullOrEmpty(SetInfo.GetValue("useregular", setting)) ? false : bool.Parse(SetInfo.GetValue("useregular", setting));
            Input.mapstr1 = string.IsNullOrEmpty(SetInfo.GetValue("leftkeys", setting))
                       ? "~1qaz2wsx3edc4rfv5tgb6"
                       : SetInfo.GetValue("leftkeys", setting);
            Input.mapstr2 = string.IsNullOrEmpty(SetInfo.GetValue("rightkeys", setting))
                       ? "yhn7ujm8ik,9ol.0p;/-['=]"
                       : SetInfo.GetValue("rightkeys", setting);
            try
            {
                InputMode.outtype = string.IsNullOrEmpty(SetInfo.GetValue("outtype", setting)) ? 0 : int.Parse(SetInfo.GetValue("outtype", setting));
            }
            catch
            {
                InputMode.outtype = 0;
            }
            if (Input.IsChinese != 1)
            {
                Input.IsCnBd = false;
                Input.IsQJ = false;
            }
          
            if (string.IsNullOrEmpty(InputMode.CDPath)) InputMode.CDPath = "空明码";

            try
            {
                setting = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "ditver.shp"), Encoding.UTF8);//读配置
                Win.WinInput.DictVersion = setting.Length > 0 ? setting[0] : "";
                setting = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "dicturl.shp"), Encoding.UTF8);//读配置
                Win.WinInput.ApiDictUrl = setting.Length > 0 ? setting[0] : "";
                if (File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "Setting.shp")))
                {
                    setting = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "Setting.shp"), Encoding.UTF8);//读配置

                    InputMode.autoup = string.IsNullOrEmpty(SetInfo.GetValue("autoup", setting)) ? (short)3 : short.Parse(SetInfo.GetValue("autoup", setting));
                    InputMode.stopup = string.IsNullOrEmpty(SetInfo.GetValue("stopup", setting)) ? "" : SetInfo.GetValue("stopup", setting);
                    InputMode.cffontname = string.IsNullOrEmpty(SetInfo.GetValue("cffontname", setting)) ? "" : SetInfo.GetValue("cffontname", setting);
                    Input.mdcode = string.IsNullOrEmpty(SetInfo.GetValue("mdcode", setting))
                        ? "qwertyuiopasdfghjkl;'zxcvbnm,./QWERTYUIOPASDFGHJKL:ZXCVBNM<>?!`~，。、；@*_#$%^&0123456789"
                        : SetInfo.GetValue("mdcode", setting);
                    InputMode.outdatetime = string.IsNullOrEmpty(SetInfo.GetValue("outdatetime", setting)) ? true : bool.Parse(SetInfo.GetValue("outdatetime", setting));
                }
                else
                {
                    InputMode.autoup = 3;
                    InputMode.stopup = "";
                    InputMode.cffontname = "";
                }
                if (!File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "setting.yaml")))
                {
                    InputMode.useregular = false;
                }
                else
                {
                    if (InputMode.useregular)
                        settingYaml = new YAMLHelp(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "setting.yaml"));
                }

            }
            catch { }
           
            return true;
        }

        public bool SaveSkin()
        {
            return false;

        }

        public bool SaveMasterDict()
        {
           
            return true;

        }

        public bool SaveUserDict()
        {
            try
            {
                File.WriteAllLines(Input.UserDitPath, Input.UserDit, Encoding.UTF8);
                
              
            }
            catch { MessageBox.Show("保存用户词库失败!"); }
            return true;
        }
        public bool SaveCloudDict()
        {
            try
            {
                File.WriteAllLines(Input.CloundDitPath, Input.ClouddDit.ToArray(), Encoding.UTF8);
            }
            catch {  }
            return true;
        }
        public bool SaveEnDict()
        {
            return false;
        }

        public bool SaveSetting()
        {
            List<string> set = new List<string>();
            set.Add("OpenCould=" + InputMode.OpenCould.ToString());
            set.Add("AutoUpdate=" + InputMode.AutoUpdate.ToString());
            set.Add("AutoRun=" + InputMode.AutoRun.ToString());
            set.Add("CurTrac=" + curTrac.ToString());
            set.Add("curMouseTrac=" + curMouseTrac.ToString());
            set.Add("OpenLink=" + InputMode.OpenLink.ToString());
            set.Add("OpenAltSelect=" + InputMode.OpenAltSelect.ToString());
            set.Add("SkinHeith=" + InputMode.SkinHeith.ToString());
            set.Add("PageSize=" + InputMode.PageSize.ToString());
            set.Add("SkinBack=" + InputMode.SkinBack.ToArgb().ToString());
            set.Add("SkinIndex=" + InputMode.SkinIndex.ToString());
            set.Add("Skinbstring=" + InputMode.Skinbstring.ToArgb().ToString());
            set.Add("Skinbcstring=" + InputMode.Skinbcstring.ToArgb().ToString());
            set.Add("Skinfbcstring=" + InputMode.Skinfbcstring.ToArgb().ToString());
            set.Add("SkinFontName=" + InputMode.SkinFontName);
            set.Add("SkinFontSize=" + InputMode.SkinFontSize.ToString());
            set.Add("CDPath=" + InputMode.CDPath);
            set.Add("ZFPath=" + InputMode.ZFPath);
            set.Add("PFPath=" + InputMode.PFPath);
            set.Add("SingleInput=" + InputMode.SingleInput.ToString());
            set.Add("txtla=" + InputMode.txtla.ToString());
            set.Add("txtlas=" + InputMode.txtlas.ToString());
            set.Add("txtlra=" + InputMode.txtlra.ToString());
            set.Add("txtlras=" + InputMode.txtlras.ToString());
            set.Add("txtra=" + InputMode.txtra.ToString());
            set.Add("txtras=" + InputMode.txtras.ToString());
            set.Add("right3_out=" + InputMode.right3_out.ToString());
            set.Add("pinyin=" + InputMode.pinyin.ToString());
            set.Add("closebj=" + InputMode.closebj.ToString());
            set.Add("autopos=" + InputMode.autopos.ToString());
            set.Add("bjzckgsp=" + InputMode.bjzckgsp.ToString());
            set.Add("omeno=" + InputMode.omeno.ToString());
            set.Add("zsallmap=" + InputMode.zsallmap.ToString());
            set.Add("zsmode1=" + InputMode.zsmode1.ToString());
            set.Add("outtype=" + InputMode.outtype.ToString());
            set.Add("IsChinese=" + Input.IsChinese);
            set.Add("datacf=" + InputMode.datacf);
            set.Add("imghh=" + InputMode.imghh);
            set.Add("oneoutbj=" + InputMode.oneoutbj);
            set.Add("ftfzxs=" + InputMode.ftfzxs);
            set.Add("dcxz=" + InputMode.dcxz);
            set.Add("iselect=" + InputMode.iselect);
            set.Add("onesp=" + InputMode.onesp);
            set.Add("select3=" + InputMode.select3);
            set.Add("spaceaout=" + InputMode.spaceaout);
            set.Add("autodata=" + InputMode.autodata);
            set.Add("useregular=" + InputMode.useregular);
            set.Add("leftkeys=" + Input.mapstr1);
            set.Add("rightkeys=" + Input.mapstr2);
            File.WriteAllLines(Input.SettingPath, set.ToArray(), Encoding.UTF8);//保存配置
            return true;

        }
        public bool SaveDictSetting()
        {
            List<string> set = new List<string>();
            set.Add(Win.WinInput.DictVersion);
            File.WriteAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "ditver.shp"), set.ToArray(), Encoding.UTF8);//保存配置
            return true;

        }
        #region ui win api
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_HIDE = 0;  
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hWnd, short cmdShow);
        private const int HWND_TOPMOST = -1;
        private const int SWP_NoActiveWINDOW = 0x10;
        [DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        private void ShowWindow()
        {
           
            ShowWindow(this.Handle, SW_SHOWNOACTIVATE);
            this.Width = 110;
            this.Height = 22;
   
            SetWindowPos(this.Handle, HWND_TOPMOST, this.Left, this.Top, this.Width, this.Height, SWP_NoActiveWINDOW);
        }
        private void HideWindow()
        {
            this.Hide();
            ShowWindow(this.Handle, SW_HIDE);
        }
        #endregion
        public bool CreateUI()
        {
            this.Text = string.Empty;
            this.TopLevel = true;
            this.TopMost = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ControlBox = true;
            
            CheckForIllegalCrossThreadCalls = false;
           
            this.ShowWindow();
            
            return false;
        }
        public void LoadLink()
        {
            Input.linkdictp = new Dictionary<string, List<string>>();
            var templist = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "dict", "LinkDit.shp")).Where(w => !string.IsNullOrEmpty(w)).GroupBy(g => g.Substring(0, 3)).Select(
               group =>
               {
                   Input.linkdictp.Add(group.Key.ToString(), group.ToList());
                   return "";
               }
              ).ToArray()[0];
        }
        public bool InputIni()
        {
            Input.indexComplete = false;//初始化未完成的状态

            #region 读取路径
            InputMode.AppPath = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
            Input.SettingPath = System.IO.Path.Combine(InputMode.AppPath, "Setting.shp");
            LoadSettting();


            Input.MasterDitPath = System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "MasterDit.shp");//兼容路径拼接方法就是这样实现
            Input.onedict = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "one.shp"));

            Input.UserDitPath = System.IO.Path.Combine(InputMode.AppPath, "dict", "UserDit.shp");
            Input.ProDitPath = System.IO.Path.Combine(InputMode.AppPath, "prodict");
            Input.EnDitPath = System.IO.Path.Combine(InputMode.AppPath, "dict", "EnDit.shp");
            Input.CloundDitPath = System.IO.Path.Combine(InputMode.AppPath, "dict", "CloundDitPath.shp");
            #endregion

            #region

            var qjzwdict = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "dict", "qjcn.shp")).ToList();
            Input.qjzwdict = qjzwdict.FindAll(f => f.IndexOf('=') > 0).ToArray();
            qjzwdict = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "dict", "bjcn.shp")).ToList();
            Input.bjzwdict = qjzwdict.FindAll(f => f.IndexOf('=') > 0).ToArray();
            qjzwdict = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "dict", "qjen.shp")).ToList();
            Input.qjywdict = qjzwdict.FindAll(f => f.IndexOf('=') > 0).ToArray();
            qjzwdict = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "dict", "bjen.shp")).ToList();
            Input.bjywdict = qjzwdict.FindAll(f => f.IndexOf('=') > 0).ToArray();

            LoadLink();
            if (Input.IsChinese != 2)
            {
                LoadMasterDict();
                LoadEnDict();
            }
            else
            {
                Task loadts = new Task(() =>
                  {
                      System.Threading.Thread.Sleep(1500);
                      LoadMasterDict();
                      LoadEnDict();
                  });
                loadts.Start();
            }
            LoadUserDict();

            #endregion

            #region 速录映射
            Input.mapsortkeys = new List<MapintKey>();
            string mapstr = "~1qaz2wsx3edc4rfv5tgb6yhn7ujm8ik,9ol.0p;/-['=]<?!:`@*_";

            for (short i = 0; i < mapstr.Length; i++)
            {
                MapintKey map = new MapintKey();
                map.ZM = mapstr.Substring(i, 1);
                map.Pos = i;
                Input.mapsortkeys.Add(map);
            }

            string[] srmap = null;
            if (File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "map.shp")))
                srmap = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "map.shp"));
            else
            {
                if (string.IsNullOrEmpty(InputMode.ZFPath))
                    srmap = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "dict", "map.shp"));
                else
                {
                    srmap = Function.Decrypt(System.IO.Path.Combine(InputMode.AppPath, "zf", InputMode.ZFPath));
                }
            }
            string[] mapdatacounts = new string[0];
            if (File.Exists(System.IO.Path.Combine(Base.InputMode.AppPath, "dict", Base.InputMode.CDPath, "mapdatacount.txt")))
            {
                mapdatacounts = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "mapdatacount.txt"), Encoding.UTF8);//读配置
            }
            Input.mapkeys = new List<MapintKey>();
            foreach (string m in srmap)
            {
                if (!string.IsNullOrEmpty(m))
                {
                    MapintKey map = new MapintKey();
                    string zm = m.Split('=')[0];
                
                    Input.mapsortkeys.FindAll(ma => zm.IndexOf(ma.ZM) >= 0).OrderBy(o => o.Pos).ToList().ForEach(f => map.ZM += f.ZM);
                    map.Pos = (short)m.Split('=')[0].Length;
                    map.Map = m.Split('=')[1];
                    map.keydown = 0;
                    if (zm.IndexOf("{") >= 0 && zm.IndexOf("}") > 0)
                    {
                        map.zfzh = true;
                        Keys key;
                        System.Enum.TryParse(zm.Replace("{", "").Replace("}", ""), out key);
                        map.ZMK = key;

                        Keys key1;
                        System.Enum.TryParse(map.Map.Replace("{", "").Replace("}", ""), out key1);
                        map.MapK = key1;
                    }
                    map.right = Win.WinInput.Input.SRRight(map.ZM);
                    map.left = Win.WinInput.Input.SRLeft(map.ZM);
                    foreach (string cm in mapdatacounts)
                    {
                        if(cm.StartsWith(map.ZM+"="))
                        {
                            map.keydown = long.Parse(cm.Split('=')[2]);
                            break;
                        }
                    }
                    Input.mapkeys.Add(map);
                }
            }
            if (Input.mapkeys.Find(m => m.Map == "~") == null)
            {
                MapintKey map = new MapintKey();
                map.ZM = "~";
                map.Pos = 1;
                map.Map = "~";
                map.right = Win.WinInput.Input.SRRight(map.ZM);
                map.left = Win.WinInput.Input.SRLeft(map.ZM);
                map.keydown = 0;
                Input.mapkeys.Add(map);
            }
          
            var objen = Input.mapkeys.Find(f => f.ZM == "aseclp");
            if (objen == null)
            {
                objen = new MapintKey();
                objen.ZM = "aseclp";
                objen.Pos = 6;
                objen.Map = "1";
                objen.right = false;
                objen.left = true;
                objen.keydown = 0;
                Input.mapkeys.Add(objen);
            }
            var objen1 = Input.mapkeys.Find(f => f.ZM == "asecrp");
            if (objen1 == null)
            {
                objen1 = new MapintKey();
                objen1.ZM = "asecrp";
                objen1.Pos = 6;
                objen1.Map = "1";
                objen1.right = true;
                objen1.left = false;
                objen1.keydown = 0;
                Input.mapkeys.Add(objen1);
            }

            Input.mapkeys = Input.mapkeys.OrderByDescending(m => m.Pos).ToList();

            #endregion

            Input.indexComplete = true;//初始化完成的状态

            System.GC.Collect();

            return true;
        }

        public WinInput()
        {
            this.InputIni();
       
 
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            this.CreateUI();

            this.LoadSkin();

            this.toolTip1.SetToolTip(this.pictureBox1, InputMode.CDPath);
            this.toolTip1.SetToolTip(this, InputMode.CDPath);
        }

        [DllImport("user32 ")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        public static HookProc KeyboardHookProcedure;   //声明键盘钩子事件类型.
        public static int hKeyboardHook = 0;   //键盘钩子句柄 
        private string ModuleName = "";
        //装置钩子的函数 
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //卸下钩子的函数 
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        //下一个钩挂的函数 
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32 ")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
        public const int WH_KEYBOARD_LL = 13;   //keyboard   hook   constant   
        private const int WM_MOUSEACTIVATE = 0x21;
        private const int MA_NOACTIVATE = 3;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;

        //[DllImport("oleacc.dll")]
        //internal static extern int AccessibleObjectFromWindow(
        //IntPtr hwnd,
        //uint id,
        //ref Guid iid,
        //[In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object ppvObject);

     
        //internal enum OBJID : uint
        //{
        //    WINDOW = 0x00000000,
        //    SYSMENU = 0xFFFFFFFF,
        //    TITLEBAR = 0xFFFFFFFE,
        //    MENU = 0xFFFFFFFD,
        //    CLIENT = 0xFFFFFFFC,
        //    VSCROLL = 0xFFFFFFFB,
        //    HSCROLL = 0xFFFFFFFA,
        //    SIZEGRIP = 0xFFFFFFF9,
        //    CARET = 0xFFFFFFF8,
        //    CURSOR = 0xFFFFFFF7,
        //    ALERT = 0xFFFFFFF6,
        //    SOUND = 0xFFFFFFF5,
        //}

        /// <summary>
        /// 安装键盘hook
        /// </summary>
        public void InstallKeyHook()
        {
 
            Cache.VDown.scanCode = 46;
            Cache.VDown.vkCode = 174;
            Cache.VUp.scanCode = 0;
            Cache.VUp.vkCode = 175;
            //安装键盘钩子   
            if (hKeyboardHook == 0)
            {
                ModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(ModuleName), 0);
                if (hKeyboardHook == 0)
                {
                    UInstallKeyHook();//安装不成功就卸装hook
                    throw new Exception("SetWindowsHookEx   ist   failed. ");
                }

            }
        }

        static int SendKeyToNex = 0;
        /// <summary>
        /// 卸装键盘hook
        /// </summary>
        public void UInstallKeyHook()
        {

            bool retKeyboard = true;

            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }
            //如果卸下钩子失败 
            if (!(retKeyboard)) throw new Exception("UnhookWindowsHookEx   failed. ");


        }

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode < 0 || !Win.WinInput.Input.indexComplete)
                return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
            if (Input.SelfOut)
                return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
            try
            {
                Core.Comm.KeyboardHookStruct MyKeyboardHookStruct = (Core.Comm.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(Core.Comm.KeyboardHookStruct));
                if (MyKeyboardHookStruct.vkCode == 231)
                {
                    return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
                }

                Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;

                if (keyData == Keys.Apps)
                {
                    //屏敝menu键，改键
                    Cache.VDown.dwExtraInfo = MyKeyboardHookStruct.dwExtraInfo;
                    Cache.VDown.flags = MyKeyboardHookStruct.flags;
                    Cache.VDown.time = MyKeyboardHookStruct.time;
                    keyData = (Keys)Cache.VDown.vkCode;
                }

                string keystring = string.Empty;

                #region 处理键盘事件
                bool openTSKey = false;
                #region 键盘转换
                var mkey = Input.mapkeys.Find(f => f.zfzh = true && f.ZMK == keyData);
                if (mkey != null) keyData = mkey.MapK;
                #endregion

                #region 键盘按下事件
                if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {

                    #region 按下分页键-+
                    if (InputMode.useregular)
                    {
                        if (InputStatus.inputstr.Length > 0 && (keyData == Keys.PageDown || keyData == Keys.PageUp))
                        {
                            InputStatus.pinyipos = 0;

                            if (keyData == Keys.PageDown)
                                InputStatus.NextPage();
                            else
                                InputStatus.PrePage();
                            keybjnum++;
                            return 1;
                        }
                    }
                    else
                    {
                        if (InputStatus.inputstr.Length > 0 && (keyData == Keys.OemMinus || keyData == Keys.Oemplus))
                        {
                            InputStatus.pinyipos = 0;

                            if (keyData == Keys.Oemplus)
                                InputStatus.NextPage();
                            else
                                InputStatus.PrePage();
                            keybjnum++;
                            return 1;
                        }
                    }
                    #endregion
                    if (keyData == Keys.Right)
                    {
                        InputStatus.pinyipos++;
                    }
                    #region 按下特殊键
                    if (keyData == Keys.RShiftKey || keyData == Keys.LShiftKey)
                    {
                        keybjnum++;
                        Input.IsPressShift = true;
                    }
                    if (keyData == Keys.RControlKey || keyData == Keys.LControlKey)
                    {
                        keybjnum++;
                        Input.IsPressCtrl = true;
                    }
                    if (keyData == Keys.Back)
                    {
                        keybjnum++;
                    }
                    if (keyData == Keys.RWin || keyData == Keys.LWin)
                    {
                        if (!InputMode.OpenAltSelect && keyData != Keys.RWin)
                            Input.IsPressWin = true;
                    }
                    if (keyData == Keys.LMenu || keyData == Keys.RMenu)
                    {
                        if (!InputMode.OpenAltSelect && keyData != Keys.RMenu)
                            Input.IsPressAlt = true;
                    }
                    #endregion

                    if (Input.IsPressCtrl && keyData == Keys.Delete)
                    {
                        Input.IsPressAlt = false;
                        Input.IsPressWin = false;
                        Input.IsPressCtrl = false;
                        Input.IsPressShift = false;
                        Input.isActiveInput = false;
                        Input.IsPressLAlt = false;
                        Input.IsPressRAlt = false;
                        Input.IsPresAltPos = 0;
                        InputStatus.Clear();

                        //inputFrm.EnterDown(false);
                        //TrayIcon.Icon = new Icon(Application.StartupPath + @"\ico\logh32.ico");
                    }
                    if (Input.IsPressCtrl && keyData == Keys.Space)
                    {

                        //激活、关闭输入法
                        TrayIcon_MouseDoubleClick(null, null);
                    }
                    if (!Input.isActiveInput)
                    {
                        SendKeyToNex = 0;
                        Input.IsPressAlt = false;
                        Input.IsPressWin = false;
                        Input.IsPressShift = false;
                        Input.IsPressLAlt = false;
                        Input.IsPressRAlt = false;
                        Input.IsPresAltPos = 0;
                        goto lastGO;
                    }
                    keystring = Input.CheckKeysString(keyData);
                    if (Input.IsPressShift)
                    {

                        if (InputStatus.inputstr.Length > 0 && Input.CheckCode(keystring))
                        {
                            InputStatus.ShangPing(1);

                        }
                        goto lastGO;
                    }
                    else if (Input.IsPressCtrl || Input.IsPressAlt || Input.IsPressWin)
                    {

                        if (InputStatus.inputstr.Length > 0 && Input.CheckCode(keystring))
                        {

                            InputStatus.Clear();
                        }
                        goto lastGO;
                    }



                    #region 有效按键时处理
                    //输入入队
                    Core.Comm.KeysQueue keyques = new Core.Comm.KeysQueue();
                    keyques.KeyData = keyData;
                    keyques.MyKeyboardHookStruct = MyKeyboardHookStruct;

                    SendKeyToNex = UserOnKeyDown(keyData);
                    if ((keyData == Keys.LMenu || keyData == Keys.RMenu
                        || keyData == Keys.LWin || keyData == Keys.RWin
                        || keyData == Keys.VolumeDown || keyData == Keys.VolumeUp
                        ) && InputMode.OpenAltSelect)
                        SendKeyToNex = 1;
                    if (SendKeyToNex == 1)
                    {
                        //lock (Comm.Cache.KeyQueue)
                        Comm.Cache.KeyQueue.Enqueue(keyques);

                        return 1;
                    }
                    if (SendKeyToNex == -1)
                    {
                        return 1;
                    }
                    #endregion
                }
                #endregion

                #region 键盘放开事件
                if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {

                    if (!Input.isActiveInput)
                    {

                        SendKeyToNex = 0;
                        Input.IsPressAlt = false;
                        Input.IsPressWin = false;
                        Input.IsPressCtrl = false;
                        Input.IsPressShift = false;
                        Input.IsPressLAlt = false;
                        Input.IsPressRAlt = false;
                        Input.IsPresAltPos = 0;
                        goto lastGO;
                    }

                    UserOnKeyUp();

                    #region 按下特殊键
                    if (keyData == Keys.RShiftKey || keyData == Keys.LShiftKey
                        || keyData == Keys.LControlKey || keyData == Keys.RControlKey
                        )
                    {
                        openTSKey = true;
                        Input.IsPressShift = false;

                        if (InputStatus.inputstr.Length > 0 || InputStatusFrm.Dream)
                        {
                            if (keyData == Keys.LShiftKey)
                            {
                                //左shift ,选2上屏
                                InputStatus.ShangPing(2, InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);

                            }
                            else if (keyData == Keys.RShiftKey)
                            {
                                //左shift ,选3上屏
                                InputStatus.ShangPing(3, InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);

                            }
                            else if (keyData == Keys.LControlKey)
                            {
                                //左control ,选6上屏
                                InputStatus.ShangPing(6, InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);

                            }
                            else if (keyData == Keys.RControlKey)
                            {
                                //右左control ,选7上屏
                                InputStatus.ShangPing(7, InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);

                            }

                        }


                    }
                    if (keyData == Keys.RControlKey || keyData == Keys.LControlKey)
                    {
                        openTSKey = true;
                        Input.IsPressCtrl = false;
                    }
                    if ((keyData == Keys.RMenu || keyData == Keys.LMenu
                        || keyData == Keys.LWin || keyData == Keys.RWin
                        || keyData == Keys.VolumeDown || keyData == Keys.VolumeUp)
                        && InputMode.OpenAltSelect)
                    {
                        //openTSKey = true;
                        openTSKey = false;
                        Input.IsPressAlt = false;
                        Input.IsPressWin = false;

                    }
                    else if ((keyData == Keys.RMenu || keyData == Keys.LMenu
                        || keyData == Keys.LWin || keyData == Keys.RWin
                        || keyData == Keys.VolumeDown || keyData == Keys.VolumeUp)
                        && !InputMode.OpenAltSelect)
                    {
                        openTSKey = true;
                        Input.IsPressAlt = false;
                        Input.IsPressWin = false;
                    }

                    #endregion

                }
            #endregion

            #endregion

            lastGO:
                if (Input.IsPressCtrl && keyData == Keys.Space) return 1;
                else if ((Input.IsPressCtrl || Input.IsPressAlt || Input.IsPressWin || Input.IsPressShift))
                {

                    if (keystring.Length == 0 && keyData != Keys.LMenu)
                        return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
                    else if (Input.IsPressShift && !(Input.IsPressCtrl || Input.IsPressAlt || Input.IsPressWin))
                    {
                        if (InputStatus.inputstr.Length > 0)
                        {
                            InputStatus.ShangPing(1);
                        }
                        InputStatusFrm.SendText(keystring, "", Input.IsChinese == 2);
                        return 1;
                    }
                    else if ((keyData == Keys.LMenu || keyData == Keys.RMenu
                        || keyData == Keys.LWin || keyData == Keys.RWin
                        || keyData == Keys.VolumeDown || keyData == Keys.VolumeUp) && InputMode.OpenAltSelect && Input.isActiveInput) return 1;
                    else
                        return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);

                }
                else if (openTSKey)
                {
                    openTSKey = false;
                    return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
                }
                else if (SendKeyToNex == 0)
                {
                    if (keyData == Keys.Back) InputStatusFrm.zdzjstr = string.Empty;
                    if ((keyData == Keys.LMenu || keyData == Keys.RMenu
                        || keyData == Keys.LWin || keyData == Keys.RWin
                        || keyData == Keys.VolumeDown || keyData == Keys.VolumeUp) && InputMode.OpenAltSelect
                        && Input.isActiveInput) return 1;
                    else
                        return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
                }
                else
                    return 1;
            }
            catch
            {
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(ModuleName), 0);
                return 1;
            }
        }

        /// <summary>
        /// 按下，返回是否需要本系统处理
        /// true本系统处理,false直接发到下一个应用
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int UserOnKeyDown(Keys key)
        {
            bool sp = false;
          
            if (InputStatus.inputstr.Length == 0 && key == Keys.Enter)
            {
                if (InputStatusFrm.Dream) { InputStatusFrm.Dream = false; InputStatusFrm.LastLinkString = string.Empty; InputStatus.Clear(); }

                return 0;
            }
            else if ((InputStatus.inputstr.Length > 0 || InputStatusFrm.Dream) && (key == Keys.Enter || key == Keys.Escape))
            {
                InputStatusFrm.Dream = false;
                InputStatusFrm.LastLinkString = string.Empty;
                InputStatus.Clear();
                return -1;
            }
            else if (InputStatus.inputstr.Length == 0 && key == Keys.Back)
            {
              if (InputStatusFrm.LastLinkString.Length > 1)
                    InputStatusFrm.LastLinkString = InputStatusFrm.LastLinkString.Substring(0, InputStatusFrm.LastLinkString.Length - 1);
                if (ImageInput.imgstr.Length - 1 < 0) ImageInput.imgstr = String.Empty;
                else
                {
                    ImageInput.imgstr = ImageInput.imgstr.Substring(0, ImageInput.imgstr.Length - 1);
                    return -1;
                }
                return 0;
            }
            else if (InputStatus.inputstr.Length > 0 && key == Keys.Back)
            {
                if (InputStatusFrm.LSView)
                {
                    InputStatus.input = string.Empty;
                    InputStatus.inputstr = string.Empty;
                    InputStatus.Clear();
                    return 0;
                }
                else if (InputMode.useregular)
                {
                    InputStatus.input = string.Empty;
                    InputStatus.inputstr = string.Empty;
                    InputStatus.Clear();
      
                }
                else if (InputStatus.inputstr.Length % 2 == 0 && InputStatus.inputstr.Length > 1)
                {
                    InputStatus.inputstr = InputStatus.inputstr.Substring(0, InputStatus.inputstr.Length - 2);
                    InputStatus.input = string.Empty;

                    InputStatus.ShowInput(sp);
                }
                else if (InputStatus.inputstr.Length > 0)
                {
                    InputStatus.inputstr = InputStatus.inputstr.Substring(0, InputStatus.inputstr.Length - 1);
                    InputStatus.input = string.Empty;
                    InputStatus.ShowInput(sp);
                }
                else
                {
                    InputStatus.input = string.Empty;
                    InputStatus.inputstr = string.Empty;
                    InputStatus.Clear();
                }
                return -1;
            }
            string keychar = Input.CheckKeysString(key);
            if (!Input.CheckCode(keychar) && key != Keys.Space)
            {
                if (InputStatus.inputstr.Length > 0)
                {

                    if (keychar.Length > 0 && "1234567890".IndexOf(keychar) >= 0)
                    {
                        //数字选重
                        InputStatus.ShangPing(int.Parse(keychar));
                        return -1;
                    }
                    else if (key == Keys.LShiftKey)
                    {
                        //左shift ,选2上屏
                        //当按下啥都不干，松开的时候选重
                        return 1;
                    }
                    else if (key == Keys.RShiftKey)
                    {
                        //左shift ,选3上屏
                        //当按下啥都不干，松开的时候选重
                        return 1;
                    }
                    else if (Comm.Function.FunionKey(key))
                    {

                        return 0;
                    }
                    else
                    {
                        //顶字上屏
                        InputStatus.ShangPing(1);
                        return 0;
                    }
                    //return -1;
                }
                if (InputStatusFrm.Dream)
                {
                    if (keychar.Length > 0 && "1234567890".IndexOf(keychar) >= 0)
                    {
                        //数字选重
                        InputStatus.ShangPing(int.Parse(keychar), InputStatusFrm.LastLinkString.Length);
                        InputStatus.Clear();
                        return -1;
                    }
                    else if (key == Keys.LShiftKey)
                    {
                        //左shift ,选2上屏
                        //当按下啥都不干，松开的时候选重
                        return 1;
                    }
                    else if (key == Keys.RShiftKey)
                    {
                        //左shift ,选3上屏
                        //当按下啥都不干，松开的时候选重
                        return 1;
                    }
                    InputStatus.Clear();
                }
                return 0;

            }
            return 1;
        }

        public static long keybjnum = 0;//并击次数
        public static long inputdznum = 0;//单字次数
        public static long inputcznum = 0;//词组次数
        public static long inputczsnum = 0;//词组字数
        /// <summary>
        /// 用户释放任意键发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserOnKeyUp()
        {
            string srinput = string.Empty;
            string psrinput = string.Empty;

            KeysQueue _lkey = null;
            bool srspace = false;
            int queuecount = 0;

            //lock (Comm.Cache.KeyQueue)
            //{
            queuecount = Comm.Cache.KeyQueue.Count;
            if (queuecount == 0)
            {
                return;
            }
            bool havejj = false;
            for (int i = 0; i < queuecount; i++) //出队
            {
                havejj = true;
                _lkey = Comm.Cache.KeyQueue.Dequeue();
                if (_lkey.KeyData == Keys.Space && !InputMode.useregular)
                {
                    if (!srspace && queuecount > 1)
                        srinput += "~";
                    srspace = true;

                }
                else if (_lkey.KeyData == Keys.LMenu || _lkey.KeyData == Keys.LWin || _lkey.KeyData == Keys.RMenu || _lkey.KeyData == Keys.RWin
                    || _lkey.KeyData == Keys.VolumeDown || _lkey.KeyData == Keys.VolumeUp)
                {
                    if (!Input.IsPressLAlt)
                        Input.IsPressLAlt = (_lkey.KeyData == Keys.LMenu || _lkey.KeyData == Keys.LWin || _lkey.KeyData == Keys.VolumeDown);
                    else if (_lkey.KeyData == Keys.VolumeDown)
                    {
                        Input.IsPressRAlt = true;
                        srspace = true;
                    }
                    if (!Input.IsPressRAlt)
                        Input.IsPressRAlt = (_lkey.KeyData == Keys.RMenu || _lkey.KeyData == Keys.RWin || _lkey.KeyData == Keys.VolumeUp);
                    else if (_lkey.KeyData == Keys.VolumeUp)
                    {
                        Input.IsPressLAlt = true;
                        srspace = true;
                    }
                }

                else
                    srinput += Input.CheckKeysString(_lkey.KeyData);

            }
            if (havejj && Comm.Cache.KeyQueue.Count == 0)
            {
                keybjnum++;
            }
            //}


            if (Input.IsPressLAlt && Input.IsPressRAlt && srspace)
            {
                short.TryParse(InputMode.txtlras, out Input.IsPresAltPos);
                if (Input.IsPresAltPos == 0 && !string.IsNullOrEmpty(InputMode.txtlras))
                    srinput += InputMode.txtlras.Trim();
            }
            else if (Input.IsPressLAlt && Input.IsPressRAlt)
            {
                short.TryParse(InputMode.txtlra, out Input.IsPresAltPos);
                if (Input.IsPresAltPos == 0 && !string.IsNullOrEmpty(InputMode.txtlra))
                    srinput += InputMode.txtlra.Trim();
            }
            else if (Input.IsPressLAlt && srspace == false)
            {
                short.TryParse(InputMode.txtla, out Input.IsPresAltPos);
                if (Input.IsPresAltPos == 0 && !string.IsNullOrEmpty(InputMode.txtla))
                    srinput += InputMode.txtla.Trim();
            }
            else if (Input.IsPressLAlt && srspace == true)
            {
                short.TryParse(InputMode.txtlas, out Input.IsPresAltPos);
                if (Input.IsPresAltPos == 0 && !string.IsNullOrEmpty(InputMode.txtlas))
                    srinput += InputMode.txtlas.Trim();
            }
            else if (Input.IsPressRAlt && srspace == false)
            {
                short.TryParse(InputMode.txtra, out Input.IsPresAltPos);
                if (Input.IsPresAltPos == 0 && !string.IsNullOrEmpty(InputMode.txtra))
                    srinput += InputMode.txtra.Trim();
            }
            else if (Input.IsPressRAlt && srspace == true)
            {
                short.TryParse(InputMode.txtras, out Input.IsPresAltPos);
                if (Input.IsPresAltPos == 0 && !string.IsNullOrEmpty(InputMode.txtras))
                    srinput += InputMode.txtras.Trim();
            }



            if (InputStatusFrm.LSView)
            {
                if (Input.IsPresAltPos > 1 && (srinput.Length == 0 || srinput == "~"))
                {
                    InputStatus.ShangPing(Input.IsPresAltPos, InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);
                    Input.IsPresAltPos = 0;
                }
                InputStatus.Clear();
            }
            psrinput = srinput;
            if (Input.IsChinese == 2 && (psrinput.Replace("~", "").Length == 1 || psrinput.Length == 0 || (Input.IsPresAltPos > 0 && psrinput == "~")))
            {
                Input.isActiveInput = false;

                try
                {


                    if (Input.IsPresAltPos > 0)
                    {
                        //完成字母上屏后选2位置上屏
                        InputStatusFrm.SendText(psrinput.Replace("~", "") + Input.IsPresAltPos, psrinput, true);
                        //InputStatusFrm.SendText("2", true);
                    }
                    else
                    {

                        if ("qwertyuiopasdfghjklzxcvbnm".IndexOf(psrinput.ToLower()) >= 0 && !InputMode.bjzckgsp)
                        {

                            if (psrinput.Replace("~", "").Length == 1)
                            {
                                if (InputMode.zsallmap) psrinput = Input.CovertStr(srinput.Replace("~", ""));
                                InputStatusFrm.SendText(psrinput.Replace("~", "") + " ", "", true);
                            }
                            if (srspace)
                                InputStatusFrm.SendText(" ", "", true);

                        }
                        else
                        {

                            if (psrinput.Replace("~", "").Length == 1)
                            {
                                if (InputMode.zsallmap) psrinput = Input.CovertStr(srinput.Replace("~", ""));
                                InputStatusFrm.SendText(psrinput.Replace("~", ""), "", true);
                            }
                            if (srspace)
                                InputStatusFrm.SendText(" ", "", true);

                        }
                    }
                    InputStatus.ClearOnly();

                }
                catch { }

                Input.isActiveInput = true;

                this.ShowWindow();
                //this.Show();
                return;
            }
            bool hleft = false;
            bool hright = false;
            bool allNum = false;
            int trynum = 0;
            allNum = int.TryParse(srinput, out trynum);

            Input.CheckLetRight(srinput, out hleft, out hright);//获取左右按键
            if (trynum > 9)
            {
                allNum = false;
                hright = false;
            }

            string nostr = string.Empty;
            if (InputStatus.inputstr.Length == Input.MaxLen && psrinput.Length > 0)
                InputStatus.ShangPing(1);
            //if (",./;，。、；，．／；＇‘’　".IndexOf(srinput) >= 0 && InputStatus.inputstr.Length < 4) hright = true;
            if (InputMode.oneoutbj &&
                Input.IsChinese == 1 && psrinput.Length == 1 && srinput.Length == 1
                && (InputStatus.inputstr.Length == 0
                || ((InputStatus.inputstr.Length == 2 || InputStatus.inputstr.Length == 3) && hright))
                        && ",./;，。、；，．／；＇‘’　".IndexOf(psrinput) >= 0)
            {
                //标点一键输出
                if (InputStatus.inputstr.Length == 0)
                    InputStatusFrm.SendText(psrinput.Replace(",", "，").Replace(".", "。").Replace("/", "、").Replace(";", "；"), "");
                else
                {
                    InputStatus.ShangPing(1);
                    InputStatusFrm.SendText(psrinput, "");
                }

                return;
            }
            else if (srinput.Length > 0)
            {
                #region 二合一版
                if (!InputStatus.inputstr.StartsWith("'") || srinput.Length > 2 || srinput == "yu" || srinput == "uy" || srinput == "rt" || srinput == "tr")
                {
                    if (InputMode.useregular)
                    {

                        if (srinput == " " && InputStatus.inputstr.Length == 0)
                        {
                            srinput = "";
                            InputStatusFrm.SendText(" ", "", true);
                            return;
                        }
                        else
                        {
                            srinput = Input.CovertStrByReg(srinput.Replace("~", ""));

                            if (InputStatus.inputstr.Length > 0 && srinput.Length == 1 && "1234567890".IndexOf(srinput) >= 0)
                            {
                                InputStatus.ShangPing(int.Parse(srinput), InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);

                                return;
                            }
                            else if (InputStatus.inputstr.Length == 0 && srinput.Length == 1 && "1234567890".IndexOf(srinput) >= 0)
                            {
                                InputStatusFrm.SendText(srinput, "");
                                return;
                            }
                        }
                    }
                    else
                    {
                        srinput = Input.CovertStr(srinput.Replace("~", ""));
                        if (srspace)
                        {
                            var objen = Input.mapkeys.Find(f => f.ZM == "aseclp");
                            if (objen != null) objen.keydown++;
                        }
                        if (Input.IsPresAltPos > 1)
                        {
                            var objen = Input.mapkeys.Find(f => f.ZM == "asecrp");
                            if (objen != null) objen.keydown++;
                        }
                    }
                    if (srinput.IndexOf("}") > 0)
                    {
                        //功能快捷键
                        InputStatusFrm.SendText(srinput, "", true);
                        InputStatus.Clear();
                        return;
                    }
                    if (srinput.Length == 1 && !InputMode.onesp && ",./;:!?<".IndexOf(srinput) >= 0)
                    {
                        InputStatusFrm.SendText(srinput.Replace(",", "，").Replace(".", "。").Replace("/", "、").Replace(";", "；").Replace("!", "！")
                            .Replace(":", "：").Replace("?", "？"), "");
                        return;
                    }
                }
                #endregion

                if (Input.IsPresAltPos > 0 && Input.IsChinese == 1)
                {
                    //优先码表找对应编码字词
                    var odic = Input.GetInputValue(InputStatus.inputstr + srinput + Input.IsPresAltPos, false, 9);
                    if (odic != null && odic[0].Length > 0 && odic[0].Length > 0)
                    {
                        srinput += Input.IsPresAltPos;
                        Input.IsPresAltPos = 0;
                    }
                }

                if ((InputMode.ftfzxs || !Input.IsJT) &&
                    (srinput.IndexOf("1I") == 0 || srinput.IndexOf("2I") == 0 || srinput.IndexOf("3I") == 0 || srinput.IndexOf("4I") == 0 || srinput.IndexOf("5I") == 0))
                {
                    int sint = int.Parse(srinput.Substring(0, 1));
                    if (!string.IsNullOrEmpty(InputStatusFrm.cachearry[InputStatus.pinyipos]) && InputStatus.s2t.Length > 0)
                    {

                        string ssss = InputStatusFrm.cachearry[InputStatus.pinyipos].Split('|')[0]
                            + "|" + InputStatus.s2t.Split(' ')[sint - 1].Replace(".", "").Replace(sint.ToString(), "")
                            + "|" + InputStatusFrm.cachearry[InputStatus.pinyipos].Split('|')[2];
                        InputStatusFrm.cachearry[InputStatus.pinyipos] = ssss;
                        InputStatus.ShangPing(InputStatus.pinyipos + 1, InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);
                    }


                    return;
                }
                else if (InputMode.iselect &&
                    (srinput.IndexOf("1i") == 0 || srinput.IndexOf("2i") == 0 || srinput.IndexOf("3i") == 0
                    || srinput.IndexOf("4i") == 0 || srinput.IndexOf("5i") == 0 || srinput.IndexOf("6i") == 0 || srinput.IndexOf("7i") == 0
                   || srinput.IndexOf("8i") == 0 || srinput.IndexOf("9i") == 0 || srinput.IndexOf("0i") == 0))
                {

                    InputStatus.ShangPing(int.Parse(srinput.Substring(0, 1)), InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);
                    return;
                }
                else if (InputMode.iselect && srinput.IndexOf(">") >= 0)
                {
                    InputStatus.ShangPing(int.Parse(srinput.Substring(1, 1)), InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);

                    return;
                }
                if (allNum || (InputStatusFrm.LastLinkNum.Length > 0 && srinput == "."))
                    InputStatusFrm.LastLinkNum += srinput;


                if (allNum && InputStatus.inputstr.Length == 0)
                {
                    InputStatusFrm.SendText(srinput, "");

                    return;
                }
                else if (allNum && srinput.Length > 1)
                {
                    InputStatus.ShangPing(1);
                    InputStatus.Clear();
                    InputStatusFrm.SendText(srinput, "");

                    return;
                }
                else if (allNum && (srinput.Length == 1 || InputStatusFrm.Dream))
                {

                    if (InputMode.autopos && InputStatus.inputstr.Length >= 3 && (srinput != "1" || InputStatus.PageNum > 1))
                    {
                        if (Input.UpdatePos(InputStatus.inputstr, trynum, InputStatus.PageNum, InputStatus.PageSize))
                        {
                            Task us = new Task(() =>
                              {
                                  if (DictEdit.orderby && File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "Setting.shp")))
                                  {
                                      var setting = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "Setting.shp"), Encoding.UTF8);//读配置

                                      DictEdit.orderby = string.IsNullOrEmpty(SetInfo.GetValue("orderby", setting)) ? true : bool.Parse(SetInfo.GetValue("orderby", setting));

                                  }
                                  if (DictEdit.orderby)
                                  {
                                      //保存词库
                                      List<string> dlist = new List<string>();
                                      var iteml = Input.MasterDit.ToList();

                                      foreach (var ind in Input.DictIndex.IndexList)
                                      {
                                          var item = iteml.FindAll(f => f.Split(' ')[0] == ind.Letter);
                                          if (item != null) dlist.AddRange(item);
                                          if (ind.mdict != null && ind.mdict.Length > 0)
                                              dlist.AddRange(ind.mdict.ToList());
                                      }

                                      File.WriteAllLines(Input.MasterDitPath, dlist.ToArray(), Encoding.UTF8);
                                      DictEdit.orderby = false;
                                  }
                                  else
                                  {
                                      File.WriteAllLines(Input.MasterDitPath, Input.MasterDit, Encoding.UTF8);
                                  }
                              });
                            us.Start();
                        }
                    }
                    //数字选重
                    InputStatus.ShangPing(trynum, InputStatusFrm.Dream ? InputStatusFrm.LastLinkString.Length : 0);

                    InputStatus.Clear();

                    return;
                }
                else if (InputStatusFrm.LastLinkNum.Length > 0 || srinput.IndexOf("$") >= 0)
                {
                    bool delprestr = true;
                    if (srinput.Length >= 3)
                    {
                        delprestr = false;
                        InputStatusFrm.LastLinkNum += srinput;
                        if (srinput.IndexOf("$") > 0)
                            srinput = srinput.Substring(srinput.IndexOf("$") - 1, 2);
                        InputStatusFrm.LastLinkNum = InputStatusFrm.LastLinkNum.Replace(srinput, "");
                    }
                    if (srinput == "Y$" && InputMode.outdatetime)
                    {
                        string cv = Core.Comm.Function.SentConverToDate(InputStatusFrm.LastLinkNum);
                        if (delprestr)
                            InputStatus.DelLastInput(InputStatusFrm.LastLinkNum.Length);
                        if (!string.IsNullOrEmpty(cv))
                        {
                            InputStatusFrm.SendText(cv, "");
                        }
                        InputStatusFrm.LastLinkNum = string.Empty;

                        return;
                    }
                    else if (srinput == "M$" && InputMode.outdatetime)
                    {
                        string cv = Core.Comm.Function.SentConverToMonth(InputStatusFrm.LastLinkNum);
                        if (delprestr)
                            InputStatus.DelLastInput(InputStatusFrm.LastLinkNum.Length);
                        if (!string.IsNullOrEmpty(cv))
                        {
                            InputStatusFrm.SendText(cv, "");
                        }
                        InputStatusFrm.LastLinkNum = string.Empty;

                        return;
                    }
                    else if (srinput == "D$" && InputMode.outdatetime)
                    {
                        string cv = Core.Comm.Function.SentConverToDay(InputStatusFrm.LastLinkNum);
                        if (delprestr)
                            InputStatus.DelLastInput(InputStatusFrm.LastLinkNum.Length);
                        if (!string.IsNullOrEmpty(cv))
                        {
                            InputStatusFrm.SendText(cv, "");
                        }
                        InputStatusFrm.LastLinkNum = string.Empty;

                        return;
                    }
                    else if (srinput == "J$")
                    {
                        string cv = Core.Comm.Function.ConvertToChinese(InputStatusFrm.LastLinkNum);
                        if (delprestr)
                            InputStatus.DelLastInput(InputStatusFrm.LastLinkNum.Length);
                        if (!string.IsNullOrEmpty(cv))
                        {
                            InputStatusFrm.SendText(cv, "");
                        }
                        InputStatusFrm.LastLinkNum = string.Empty;

                        return;
                    }
                    else if (srinput == "W$" && InputMode.outdatetime)
                    {
                        string cv = Core.Comm.Function.SentConverToWeek(InputStatusFrm.LastLinkNum);
                        if (delprestr)
                            InputStatus.DelLastInput(InputStatusFrm.LastLinkNum.Length);
                        if (!string.IsNullOrEmpty(cv))
                        {
                            InputStatusFrm.SendText(cv, "");
                        }

                        InputStatusFrm.LastLinkNum = string.Empty;
                        return;
                    }
                    else if (srinput == "Z$" && hleft)
                    {
                        //造词

                        UserDict frm = new UserDict(Input, this, InputStatusFrm.LastLinkCodeString, InputStatusFrm.LastLinkString);
                        frm.TopMost = true;
                        frm.Show();

                        return;
                    }
                    else if (srinput == "F$" && hleft)
                    {
                        //查找
                        QueryFrm frm = new QueryFrm(InputStatusFrm.LastSPValue);
                        frm.TopMost = true;
                        frm.Show();

                        return;
                    }
                }


                if (!allNum && srinput != ".")
                    InputStatusFrm.LastLinkNum = string.Empty;
            }
            else if (InputStatus.inputstr.Length == 0 && _lkey.KeyData == Keys.Space)
            {

                InputStatusFrm.SendText(" ", "");
                return;
            }
            #region 内置快捷方式
            switch (srinput)
            {
                case "$1":
                    {
                        inputst();
                        return;
                    }
                case "$11":
                    {
                        inputzsst();
                        return;
                    }
                case "$111":
                    {
                        inputenst();
                        return;
                    }
                case "$1111":
                    {
                        hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(ModuleName), 0);
                        return;
                    }
                case "$11111":
                    {
                        InputMode.imgsend = !InputMode.imgsend;
                        InputMode.zjsend = false;
                        ImageInput.imgstr = String.Empty;
                        if (InputMode.imgsend)
                            ImageInput.ShowWindow();
                        else
                            ImageInput.Hide();
                        return;
                    }
                case "$111111":
                    {
                        InputMode.imgsend = !InputMode.imgsend;
                        InputMode.zjsend = true;
                        ImageInput.imgstr = String.Empty;
                        if (InputMode.imgsend)
                            ImageInput.ShowWindow();
                        else
                            ImageInput.Hide();
                        return;
                    }
                case "$2":
                    {
                        inputstye();
                        return;
                    }
                case "$22":
                    {
                        if (Win.WinInput.InputStatus.Visible)
                        {
                            //图片输出
                            Bitmap img = new Bitmap(Win.WinInput.InputStatus.Width, Win.WinInput.InputStatus.Height);
                            Graphics g = Graphics.FromImage(img);
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.CopyFromScreen(Win.WinInput.InputStatus.Left, Win.WinInput.InputStatus.Top, 0, 0, new Size(Win.WinInput.InputStatus.Width, Win.WinInput.InputStatus.Height));
                            Clipboard.SetImage(img);
                            SendKeys.Send("^v"); //发送ctrl+v 进行粘贴
                        }
                        return;
                    }
                case "$3":
                    {
                        inputjf();

                        return;
                    }
                case "$33":
                    {
                        if (!InputMode.AutoZJ)
                        {
                            LoadLink();
                        }
                        InputMode.AutoZJ = !InputMode.AutoZJ;
                        InputMode.OpenLink = InputMode.AutoZJ;
                        return;
                    }
                case "$4":
                    {
                        if (InputStatus.inputstr.Length == 0)
                        {
                            //删除一次上屏的字
                            InputStatus.DelLastInput();
                            InputStatusFrm.zdzjstr = string.Empty;
                        }
                        else
                        {

                            if (InputStatus.PageNum == 1)
                            {
                                InputStatus.Clear();
                                //InputStatus.DelLastInput(); //删除一次上屏的字
                            }
                            else
                                InputStatus.PrePage(); //上一页翻页

                        }

                        return;
                    }
                case "$5":
                    {
                        if (InputStatus.inputstr.Length == 0)
                        {
                            if (!InputMode.imgsend || ImageInput.imgstr.Length == 0)
                            {
                                //删除
                                InputStatusFrm.SendText("{BACKSPACE}", "", true);
                            }
                            else
                            {
                                if (ImageInput.imgstr.Length - 1 <= 0) ImageInput.imgstr = String.Empty;
                                else ImageInput.imgstr = ImageInput.imgstr.Substring(0, ImageInput.imgstr.Length - 1);
                            }
                            InputStatusFrm.zdzjstr = string.Empty;
                        }
                        else
                        {
                            //下一页翻页
                            InputStatus.NextPage();
                            if (InputStatus.PageNum == 1)
                                InputStatusFrm.SendText("{BACKSPACE}", "", true);
                        }
                        return;
                    }
                case "$6":
                    {
                        //激活隐藏输入法
                        Input.Metor = !Input.Metor;
                        return;
                    }
                case "$66":
                    {
                        //单字模式，正常模式切换
                        InputMode.SingleInput = !InputMode.SingleInput;
                        return;
                    }
                default:
                    {

                        if (InputMode.dcxz && Input.IsChinese == 1 && srinput.Length == 2 && "1D2D3D4D5D".IndexOf(srinput) >= 0)
                        {
                            //右消字
                            int delcount = int.Parse(srinput.Replace("D", ""));

                            if (InputStatusFrm.LastSPValue.Length >= delcount)
                            {
                                string lastv = InputStatusFrm.LastSPValue;
                                InputStatus.DelLastInput();
                                InputStatusFrm.SendText(lastv.Substring(0, lastv.Length - delcount), "");
                            }
                        }
                        else if (InputMode.dcxz && Input.IsChinese == 1 && srinput.Length == 2 && "1d2d3d4d5d".IndexOf(srinput) >= 0)
                        {
                            //左消字
                            int delcount = int.Parse(srinput.Replace("d", ""));

                            if (InputStatusFrm.LastSPValue.Length >= delcount)
                            {
                                string lastv = InputStatusFrm.LastSPValue;
                                InputStatus.DelLastInput();
                                InputStatusFrm.SendText(lastv.Substring(delcount), "");
                            }
                        }
                        break;

                    }
            }
            #endregion

            if (InputStatus.inputstr.Length > 0 && queuecount == 1 && _lkey.KeyData == Keys.Space)
            {
                //当是单独空格的时候
                InputStatus.ShangPing(1);

                var objen = Input.mapkeys.Find(f => f.ZM == "aseclp");
                if (objen != null) objen.keydown++;

                return;
            }  
        
       
            SetCurPos();

            if (srinput.Length > 0)
            {
                try
                {
                    #region 
                    string inputss = string.Empty;
                    for (int i = 0; i < srinput.Length; i++)
                    {
                        if (Input.CheckCode(srinput.Substring(i, 1)))
                        {
                            inputss += srinput.Substring(i, 1);
                        }
                        else
                            nostr += srinput.Substring(i, 1);
                    }
                    if (Input.IsChinese == 1 && InputMode.right3_out && InputStatus.inputstr.Length == 2
                        && inputss.Length == 1 && hright
                        && !InputStatus.inputstr.StartsWith("'")
                        && !InputMode.closebj)
                    {
                        if (!(InputMode.select3 && srspace))
                        {
                            //第3码是右手顶字上屏
                            InputStatus.ShangPing(1);
                        }
                    }

                    if (Input.IsChinese == 0 && inputss.Length == 0 && nostr == " ")
                    {
                        InputStatus.inputstr += nostr;
                        InputStatus.input = nostr;
                    }
                    else if (Input.IsChinese == 1 && InputStatus.inputstr.Length == 0
                        && (nostr.Length == 0 || nostr == "　")
                        && InputStatusFrm.StrLength(inputss) == 1 && inputss != "'"
                        && !InputMode.closebj && InputMode.onesp)
                    {
                        if (srspace) inputss += "~";
                        InputStatusFrm.SendText(Input.GetLROne(inputss, hleft, Input.IsPresAltPos), inputss);
                        if (InputStatusFrm.LastLinkString.Length > 2 || (Input.IsChinese == 0 && InputStatusFrm.LastLinkString.Length > 0))
                            InputStatus.GetDreamValue(InputStatusFrm.LastLinkString);
                        return;
                    }
                    else if (Input.IsChinese == 1 && InputStatus.inputstr.Length == 0 && nostr == "~" && inputss.Length == 1
                        && inputss != "'")
                    {
                        InputStatusFrm.SendText(Input.GetLROne(nostr + inputss, hleft, Input.IsPresAltPos), inputss);
                        if (InputStatusFrm.LastLinkString.Length > 2 || (Input.IsChinese == 0 && InputStatusFrm.LastLinkString.Length > 0))
                            InputStatus.GetDreamValue(InputStatusFrm.LastLinkString);
                        return;
                    }
                    else
                    {
                        InputStatus.inputstr += inputss;
                        InputStatus.input = inputss;
                    }

                    if (Input.IsChinese == 1
                        && (InputStatus.inputstr.Length >= InputMode.autoup && InputMode.onesp)
                        && !InputStatus.inputstr.StartsWith("'")
                        && InputMode.stopup.IndexOf(InputStatus.inputstr.Substring(0, 1)) < 0
                        && !InputMode.closebj)
                    {
                        if (!(InputMode.select3 && srspace) || InputStatus.inputstr.Length > 3)
                        {
                            if (!srspace) srspace = true;
                            else srspace = false;
                        }

                    }
                    if (Input.IsChinese == 2)
                    {
                        Input.isActiveInput = false;
                        try
                        {
                            if (inputss.Length > 0)
                            {
                                string[] tempdic = null;
                                if (inputss.Length > 1 && (srspace || Input.IsPresAltPos > 1))
                                {
                                    tempdic = Input.GetInputValue(inputss, false, 0);
                                }
                                if (tempdic != null)
                                {
                                    if (Input.IsPresAltPos > 0 && Input.IsPresAltPos <= tempdic.Length)
                                    {
                                        Input.IsPresAltPos = (short)(Input.IsPresAltPos - 1);
                                    }
                                    else if (Input.IsPresAltPos > 0)
                                    {
                                        Input.IsPresAltPos = (short)(tempdic.Length - 1);

                                    }

                                    if (tempdic[Input.IsPresAltPos].Length > 0)
                                        InputStatusFrm.SendText(tempdic[Input.IsPresAltPos].Split('|')[1], psrinput, true);
                                }
                                else
                                {
                                    if (Input.IsPresAltPos > 0)
                                    {
                                        //完成字母上屏后选2位置上屏
                                        InputStatusFrm.SendText(inputss.Replace("~", "") + Input.IsPresAltPos, psrinput, true);

                                        //InputStatusFrm.SendText("2", true);
                                    }
                                    else if (srspace)
                                    {
                                        //完成字母上屏后有空格按键则补空格
                                        InputStatusFrm.SendText(inputss.Replace("~", ""), psrinput, true);
                                        InputStatusFrm.SendText(" ", "", true);
                                    }
                                    else if (inputss.Length > 1)
                                    {
                                        InputStatusFrm.SendText(inputss.Replace("~", ""), "", true);
                                    }
                                    else if (InputMode.bjzckgsp)
                                    {
                                        InputStatusFrm.SendText(inputss.Replace("~", ""), "", true);
                                    }
                                    else
                                    {
                                        InputStatusFrm.SendText(inputss.Replace("~", "") + " ", "", true);

                                    }
                                }
                            }
                            else if (srspace)
                            {
                                InputStatusFrm.SendText(" ", "", true);

                            }
                            InputStatus.ClearOnly();
                        }
                        catch { }

                        Input.isActiveInput = true;
                        this.ShowWindow();
                        // this.Show();
                    }
                    else
                    {
                        if (Input.IsPressLAlt && InputStatus.inputstr.Length == 4 && !InputStatus.inputstr.StartsWith("'"))
                        {
                            short tpos = Input.IsPresAltPos;
                            InputStatus.ShowInput(false, false, 0, false, false, 1, false, Input.IsPresAltPos);

                            if (tpos > 1)
                            {
                                if (InputMode.spaceaout == 3 && tpos == 3 && InputStatus.inputstr.Length >2) tpos = 2;
                                else if (InputMode.spaceaout == 3 && tpos == 2 && InputStatus.inputstr.Length > 2 && InputStatusFrm.cachearry.Length>1) tpos = 1;
                                Input.IsPresAltPos = tpos;
                            }
                            if (!(InputMode.spaceaout == 3 && InputStatus.inputstr.Length == 4) && InputStatus.inputstr.Length>1)
                            {
                                InputStatus.ShangPing(Input.IsPresAltPos);

                                InputStatus.Left += InputStatusFrm.LastSPValue.Length * 16;
                            }
                            else if (InputMode.spaceaout == 3 && InputStatus.inputstr.Length == 4 && InputStatusFrm.cachearry.Length > 1)
                            {
                                InputStatus.ShangPing(Input.IsPresAltPos,0,false);

                                InputStatus.Left += InputStatusFrm.LastSPValue.Length * 16;
                            }

                        }
                        else if (InputStatus.inputstr.Length == 4 && psrinput.IndexOf("~") >= 0)
                            InputStatus.ShowInput(srspace, InputStatus.inputstr.Length > 3 ? false : true, 0, true);
                        else
                        {
                            bool smspace = psrinput.IndexOf("~") >= 0;
                            if (Input.IsPresAltPos > 0) { srspace = false; smspace = false; }
                            int spp = 1;
                            if (!InputMode.right3_out && InputStatus.inputstr.Length == 3 && inputss.Length == 1
                                && hright && !InputStatus.inputstr.StartsWith("'") && !InputMode.closebj)
                            {
                                if (!(InputMode.select3 && smspace))
                                {
                                    //第3码是右手2选上屏 左右右
                                    spp = Input.IsPresAltPos > 1 ? (Input.IsPresAltPos + 1) : 2;
                                    if (Input.IsPresAltPos < 2)
                                        Input.IsPresAltPos = 0;
                                    else
                                        srspace = true;
                                }
                                else
                                {
                                    spp = Input.IsPresAltPos > 1 ? (Input.IsPresAltPos + 1) : 3;
                                    Input.IsPresAltPos = 0;
                                    srspace = true;
                                }
                            }
                            else if (!InputMode.right3_out && InputStatus.inputstr.Length == 3 && InputMode.select3 && smspace && hright)
                            {
                                spp = Input.IsPresAltPos > 1 ? (Input.IsPresAltPos + 1) : 3;
                                Input.IsPresAltPos = 0;
                            }
                            else if (!InputMode.right3_out && InputStatus.inputstr.Length == 3 && InputMode.select3 && smspace && !hright)
                            {
                                spp = Input.IsPresAltPos > 1 ? (Input.IsPresAltPos + 1) : 4;
                                Input.IsPresAltPos = 0;
                            }
                            else if (InputMode.right3_out && InputStatus.inputstr.Length == 3 && InputMode.select3 && smspace && hright)
                            {
                                spp = Input.IsPresAltPos > 1 ? (Input.IsPresAltPos + 1) : 2;
                                Input.IsPresAltPos = 0;
                            }
                            else if (InputMode.right3_out && InputStatus.inputstr.Length == 3 && InputMode.select3 && smspace && !hright)
                            {
                                spp = Input.IsPresAltPos > 1 ? (Input.IsPresAltPos + 1) : 3;
                                Input.IsPresAltPos = 0;
                            }
                            InputStatus.ShowInput(srspace, InputStatus.inputstr.Length > 3 ? false : true, 0, smspace
                                , Input.IsPresAltPos > 0, spp, hright, Input.IsPresAltPos);

                            if (srspace)
                                InputStatus.Left += InputStatusFrm.LastSPValue.Length * 16;
                            else if (InputMode.spaceaout == 3 && InputStatus.inputstr.Length == 4 && InputStatusFrm.cachearry.Length > 1)
                            {
                                InputStatus.ShangPing(1, 0, false);

                                InputStatus.Left += InputStatusFrm.LastSPValue.Length * 16;
                            }

                        }
                        if (InputStatus.inputstr.Length > 0 && InputStatus.inputstr.Length < 2 && psrinput.IndexOf("~") >= 0
                             && Input.IsPresAltPos < 2)
                        {
                            InputStatus.ShangPing(1);

                        }
                        else if (InputStatus.inputstr.Length > 0 && Input.IsPresAltPos > 0)
                        {
                            Input.IsPressLAlt = false;
                            Input.IsPressRAlt = false;

                            InputStatus.ShangPing(Input.IsPresAltPos);

                            Input.IsPresAltPos = 0;

                            InputStatus.Left += InputStatusFrm.LastSPValue.Length * 16;
                        }

                        if (Input.IsChinese == 0 && inputss.Length > 0 && srspace)
                        {
                            //英文速录下起作用
                            //完成字母上屏后有空格按键则补空格
                            InputStatusFrm.SendText(" ", "");
                        }
                        else if (Input.IsChinese == 0 && InputStatusFrm.LastLinkString.Length > 2)
                            InputStatus.GetDreamValue(InputStatusFrm.LastLinkString);
                    }
                    #endregion
                }
                catch { }
                Input.IsPressLAlt = false;
                Input.IsPressRAlt = false;
                Input.IsPresAltPos = 0;
            }
            else
            {
                if (InputStatus.inputstr.Length > 0)
                {
                    if (Input.IsPresAltPos > 0)
                    {
                        InputStatus.ShangPing(Input.IsPresAltPos);
                        var objen = Input.mapkeys.Find(f => f.ZM == "asecrp");
                        if (objen != null) objen.keydown++;
                        if (srspace)
                        {
                            objen = Input.mapkeys.Find(f => f.ZM == "aseclp");
                            if (objen != null) objen.keydown++;
                        }
                    }
                   
                }
                Input.IsPressLAlt = false;
                Input.IsPressRAlt = false;
                Input.IsPresAltPos = 0;
            }

        }

        private void inputst()
        {
            InputStatus.Clear();
            InputStatusFrm.LastLinkString = string.Empty;
            InputStatusFrm.Dream = false;
            //中文英文转换
            if (Input.IsChinese == 1)
            {
                InputStatusFrm.zdzjstr = string.Empty;
                //切换至英文
                Input.IsChinese = 0;
                Input.IsCnBd = false;
                Input.IsQJ = false;
            }
            else if (Input.IsChinese == 0)
            {
                //切换至速路助手
                Input.IsChinese = 2;
                Input.IsCnBd = false;
                Input.IsQJ = false;
            }
            else
            {
                //切换至中文
                Input.IsChinese = 1;
                Input.IsCnBd = true;
            }
            SaveSetting();
            LoadSkin();
        }

        private void inputzsst()
        {
            InputStatus.Clear();
            InputStatusFrm.LastLinkString = string.Empty;
            InputStatusFrm.Dream = false;
            //中文英文转换
            if (Input.IsChinese == 1 || Input.IsChinese == 0)
            {
                //切换至速路助手
                Input.IsChinese = 2;
                Input.IsCnBd = false;
                Input.IsQJ = false;
            }
            else
            {
                //切换至中文
                Input.IsChinese = 1;
                Input.IsCnBd = true;
            }
            SaveSetting();
            LoadSkin();
        }

        private void inputenst()
        {
            InputStatus.Clear();
            InputStatusFrm.LastLinkString = string.Empty;
            InputStatusFrm.Dream = false;
            //中文英文转换
            if (Input.IsChinese == 1 || Input.IsChinese == 2)
            {
                InputStatusFrm.zdzjstr = string.Empty;
                //切换至英文
                Input.IsChinese = 0;
                Input.IsCnBd = false;
                Input.IsQJ = false;
            }
            else
            {
                //切换至中文
                Input.IsChinese = 1;
                Input.IsCnBd = true;
            }
            SaveSetting();
            LoadSkin();
        }
        private void inputstye()
        {
            InputStatus.Clear();
            if (Input.IsChinese == 1)
            {
                //全角半角转换
                if (Input.IsQJ)
                {
                    Input.IsQJ = false;
                    Input.IsCnBd = true;
                }
                else
                {
                    Input.IsQJ = true;
                    Input.IsCnBd = true;
                }
                LoadSkin();
            }
        }

        private void inputjf()
        {
            InputStatus.Clear();

            //简繁文转换
            if (Input.IsJT)
            {
                Input.IsJT = false;
            }
            else
            {
                Input.IsJT = true;
            }
            LoadSkin();
        }
        #region 输入法候选框位置定位
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        [DllImport("user32.dll")]
        private static extern IntPtr AttachThreadInput(IntPtr idAttach, IntPtr idAttachTo, int fAttach);
        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();
        [DllImport("user32.dll")]
        public static extern bool GetCaretPos(out Point lpPoint);

  
        [DllImport("user32.dll")]
        private static extern void ClientToScreen(IntPtr hWnd, ref Point p);

        //[DllImport("user32.dll")]
        //private static extern void GetWindowRect(IntPtr hWnd, ref RECT p);    
        /// <summary>
        /// 可获取窗口进程信息
        /// </summary>
        /// <param name="dwthreadid"></param>   
        /// <param name="lpguithreadinfo"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetGUIThreadInfo")]
        public static extern uint GetGUIThreadInfo(uint dwthreadid, ref GUITHREADINFO lpguithreadinfo);
 
    
        //Win32 Calls
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public uint Left;
            public uint Top;
            public uint Right;
            public uint Bottom;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct GUITHREADINFO
        {
            public uint cbSize;
            public uint flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public RECT rcCaret;
        };

     
        IntPtr targetThreadID = IntPtr.Zero;//活动窗口线程号
        IntPtr localThreadID = IntPtr.Zero;//输入法线程号
        /// <summary>
        /// 设置光标位置
        /// </summary>
        private void GetCurPos()
        {
            #region 光标所在位置
            IntPtr ForegroundWindow = GetForegroundWindow();
            InputMode.deskDC = InputMode.GetTopDc();
            WinInput.ForegroundWindow = ForegroundWindow;
            curPoint = new Point();

            //Guid guid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
            //object obj = null;
            //int xx = 0, yy = 0, xt = 0, yt = 0;



            //得到Caret在屏幕上的位置  
            if (ForegroundWindow.ToInt32() != 0)
            {

                targetThreadID = GetWindowThreadProcessId(ForegroundWindow, IntPtr.Zero);


                if (localThreadID != targetThreadID)
                {
                    AttachThreadInput(localThreadID, targetThreadID, 1);
                    ForegroundWindow = GetFocus();

                    if (ForegroundWindow.ToInt32() != 0)
                    {


                        if (GetCaretPos(out curPoint))
                        {
                            ClientToScreen(ForegroundWindow, ref curPoint);
                        }

                        GUITHREADINFO gInfo = new GUITHREADINFO();
                        gInfo.cbSize = (uint)Marshal.SizeOf(gInfo);
                        bool bl = System.Convert.ToBoolean(GetGUIThreadInfo((uint)targetThreadID, ref gInfo));

                        if (bl)
                        {
                            if (gInfo.hwndCaret != ForegroundWindow)
                            {
                         
                                if (InputStatus.Top != this.Top - 25)
                                {
                                    InputStatus.Top = this.Top - 25;
                                }
                                curPoint.Y = InputStatus.Top;
                                if (InputStatus.ViewType == 1)
                                {

                                    if (curPoint.Y > Screen.PrimaryScreen.WorkingArea.Height - InputStatus.Height - 10 - 25)
                                        curPoint.Y = Screen.PrimaryScreen.WorkingArea.Height - InputStatus.Height - 10 - 25;
                                }


                                if (curPoint.X < this.Left + this.Width + 10)
                                    curPoint.X = this.Left + this.Width + 10;

                            }

                        }

                    }
                    else
                    {

                        curPoint.Y = Screen.PrimaryScreen.WorkingArea.Height - InputStatus.Height - 10 - 25;

                        if (curPoint.X < this.Left + this.Width + 10)
                            curPoint.X = this.Left + this.Width + 10;
                    }

                }
            }


            #endregion
        }

        //public void MousGetCursorPos()
        //{
        //    GetCursorPos(out curPoint);
         
        //}
        private Point curPoint;//当前光标位置
        public bool curTrac = true;//光标跟随
        public bool curMouseTrac = false;//鼠标跟随
        /// <summary>
        /// 设置输入框光标的位置
        /// </summary>
        private void SetCurPos()
        {
            if (curMouseTrac || curTrac || InputMode.imgsend)
            {

                if (InputStatus.inputstr == "")
                {
                    if (curMouseTrac)
                    {

                        InputStatus.Top = this.Top + this.Height + 1;

                        InputStatus.Left = this.Left;
                    }
                    else
                    {
                        GetCurPos();

                        if (curPoint.Y > 0)
                        {
                            if (curPoint.Y + 25 != InputStatus.Top)
                                InputStatus.Top = curPoint.Y + 25 + 5;

                            InputStatus.Left = curPoint.X + 10;
                        }
                        else
                        {
                            InputStatus.Left = this.Left + this.Width + 30;
                            InputStatus.Top = this.Top + 5;
                        }
                    }
                }

            }
            else
            {
                //InputStatus.Left = Screen.PrimaryScreen.WorkingArea.Width / 2 - 170;
                //InputStatus.Top = this.Top;
                InputStatus.Left = this.Left + this.Width + 30;
                InputStatus.Top = this.Top + 5;
            }
        }
        #endregion
        /// <summary>
        /// 截取close消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = new IntPtr(MA_NOACTIVATE);
                return;
            }
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinInput));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(90, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 22);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IME_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.IME_MouseMove);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(20, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 22);
            this.label2.TabIndex = 2;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(40, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 22);
            this.label3.TabIndex = 3;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(60, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 22);
            this.label4.TabIndex = 4;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WinInput
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(110, 22);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WinInput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.WinInput_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IME_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.IME_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThreadId();
        private void WinInput_Load(object sender, EventArgs e)
        {
            InstallKeyHook();
            InputStatus.Show();
            InputStatus.Hide();
            localThreadID = GetCurrentThreadId();//获取当前线程号

            //状态栏位于屏幕左下角
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - 63;
            this.Left = 5;
           
            Initializenotifyicon();
 
            PlanThread.Start();
     
        }

        private Point offset;
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IME_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;

            Point cur = this.PointToScreen(e.Location);
            offset = new Point(cur.X - this.Left, cur.Y - this.Top);
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IME_MouseMove(object sender, MouseEventArgs e)
        {

            if (MouseButtons.Left != e.Button) return;

            Point cur = MousePosition;
            this.Location = new Point(cur.X - offset.X, cur.Y - offset.Y);
        }

        /// <summary>
        /// 获取键盘值
        /// </summary>
        /// <param name="MyKeyboardHookStruct"></param>
        /// <returns></returns>
        private KeyPressEventArgs GetKeyPressEventArgs(Core.Comm.KeyboardHookStruct MyKeyboardHookStruct)
        {

            byte[] keyState = new byte[256];
            GetKeyboardState(keyState);
            byte[] inBuffer = new byte[2];
            if (ToAscii(MyKeyboardHookStruct.vkCode,
              MyKeyboardHookStruct.scanCode,
              keyState,
              inBuffer,
              MyKeyboardHookStruct.flags) == 1)
            {
                return (new KeyPressEventArgs((char)inBuffer[0]));
            }

            return null;
        }

        /// <summary>
        /// 检查是否是功能键
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        private bool CheckFunKey(Keys _key)
        {
            switch (_key)
            {
                case Keys.End: return true;
                case Keys.Home: return true;
                case Keys.PageUp: return true;
                case Keys.PageDown: return true;
                case Keys.Delete: return true;
                case Keys.Enter: return true;
                case Keys.Up: return true;
                case Keys.Down: return true;
                case Keys.Left: return true;
                case Keys.Right: return true;
                case Keys.Escape: return true;
                case Keys.F1: return true;
                case Keys.F2: return true;
                case Keys.F3: return true;
                case Keys.F4: return true;
                case Keys.F5: return true;
                case Keys.F6: return true;
                case Keys.F7: return true;
                case Keys.F8: return true;
                case Keys.F9: return true;
                case Keys.F10: return true;
                case Keys.F11: return true;
                case Keys.F12: return true;
                case Keys.Tab: return true;
                case Keys.CapsLock: return true;
                default: return false;
            }
        }

        NotifyIcon TrayIcon;
        MenuItem[] mnuItms = null;
        public static ContextMenu notifyiconMnu;
        /// <summary>
        /// 定义托盘
        /// </summary>
        private void Initializenotifyicon()
        {
            //设定托盘程序的各个属性 
            TrayIcon = new NotifyIcon();

            try
            {

                TrayIcon.Icon = new Icon(System.IO.Path.Combine(InputMode.AppPath, "log32.ico"));
            }
            catch
            {
                MessageBox.Show("图标文件未找到!", "装载图标出错");
                return;
            }

            TrayIcon.Text = "速录宝3.1.2";//鼠标移至托盘的提示文本
            TrayIcon.Visible = true;

            //定义一个MenuItem数组，并把此数组同时赋值给ContextMenu对象 
            mnuItms = new MenuItem[14];
            mnuItms[mnuItms.Length - 14] = new MenuItem();
            mnuItms[mnuItms.Length - 14].Text = "关于速录宝3.1.2";
            mnuItms[mnuItms.Length - 14].Visible = true;
            mnuItms[mnuItms.Length - 14].Click += new System.EventHandler(this.AboutInfo);
            mnuItms[mnuItms.Length - 13] = new MenuItem();
            mnuItms[mnuItms.Length - 13].Text = "打开官网";
            mnuItms[mnuItms.Length - 13].Visible = true;
            mnuItms[mnuItms.Length - 13].Click += new System.EventHandler(this.OpenKmmUrl);
            mnuItms[mnuItms.Length - 12] = new MenuItem();
            mnuItms[mnuItms.Length - 12].Text = "更新最新版本";
            mnuItms[mnuItms.Length - 12].Visible = true;
            mnuItms[mnuItms.Length - 12].Click += new System.EventHandler(this.UpdataSoft);
  
            mnuItms[mnuItms.Length - 11] = new MenuItem();
            mnuItms[mnuItms.Length - 11].Text = "设置";
            mnuItms[mnuItms.Length - 11].Visible = true;
            mnuItms[mnuItms.Length - 11].Click += new System.EventHandler(this.label1_Click);

            mnuItms[mnuItms.Length - 10] = new MenuItem();
            mnuItms[mnuItms.Length - 10].Text = "空明码指法练习";
            mnuItms[mnuItms.Length - 10].Visible = true;
            mnuItms[mnuItms.Length - 10].Click += new System.EventHandler(this.OpenBJLearn);

            mnuItms[mnuItms.Length - 9] = new MenuItem();
            mnuItms[mnuItms.Length - 9].Text = "指法方案";
            mnuItms[mnuItms.Length - 9].Visible = true;

            List<string> fs = new List<string>(Directory.GetFiles(System.IO.Path.Combine(InputMode.AppPath, "zf")));
            foreach (var dir in fs)
            {
                MenuItem ttools = new MenuItem();
                string inputname = dir.Substring(dir.LastIndexOf("\\") + 1); ;
                if (InputMode.ZFPath == inputname)
                    ttools.Checked = true;
                ttools.Text = inputname;
                ttools.Click += new System.EventHandler(this.OpenzfInput);
                mnuItms[mnuItms.Length - 9].MenuItems.Add(ttools);
            }

            mnuItms[mnuItms.Length - 8] = new MenuItem();
            mnuItms[mnuItms.Length - 8].Text = "输入法方案";
            mnuItms[mnuItms.Length - 8].Visible = true;

            List<string> dirs = new List<string>(Directory.GetDirectories(System.IO.Path.Combine(InputMode.AppPath, "dict"), "*", System.IO.SearchOption.AllDirectories));
            foreach (var dir in dirs)
            {
                MenuItem ttools = new MenuItem();
                string inputname= dir.Substring(dir.LastIndexOf("\\") + 1); ;
                if (InputMode.CDPath == inputname)
                    ttools.Checked = true;
                ttools.Text = inputname;
                ttools.Click += new System.EventHandler(this.OpenInput);
                mnuItms[mnuItms.Length - 8].MenuItems.Add(ttools);
            }

            mnuItms[mnuItms.Length - 7] = new MenuItem();
            mnuItms[mnuItms.Length - 7].Text = "皮肤";
            mnuItms[mnuItms.Length - 7].Visible = true;

            List<string> fdirs = new List<string>(Directory.GetDirectories(System.IO.Path.Combine(InputMode.AppPath, "skin"), "*", System.IO.SearchOption.AllDirectories));
            foreach (var dir in fdirs)
            {
                MenuItem ttools = new MenuItem();
                string inputname = dir.Substring(dir.LastIndexOf("\\") + 1); ;
                if (InputMode.PFPath == inputname)
                    ttools.Checked = true;
                ttools.Text = inputname;
                ttools.Click += new System.EventHandler(this.PFInput);
                mnuItms[mnuItms.Length - 7].MenuItems.Add(ttools);
            }

            mnuItms[mnuItms.Length - 6] = new MenuItem();
            mnuItms[mnuItms.Length - 6].Text = "工具";

            MenuItem itmetools44 = new MenuItem();
            itmetools44.Text = "词库编辑";
            itmetools44.Click += new System.EventHandler(this.CreateDictEdit);
            mnuItms[mnuItms.Length - 6].MenuItems.Add(itmetools44);

            MenuItem itmetools = new MenuItem();
            itmetools.Text = "记事本";
            itmetools.Click += new System.EventHandler(this.OpenJSB);
            mnuItms[mnuItms.Length - 6].MenuItems.Add(itmetools);

            MenuItem itmetools1 = new MenuItem();
            itmetools1.Text = "计算器";
            itmetools1.Click += new System.EventHandler(this.OpenJSQ);
            mnuItms[mnuItms.Length - 6].MenuItems.Add(itmetools1);

            MenuItem itmetools2 = new MenuItem();
            itmetools2.Text = "修复";
            itmetools2.Click += new System.EventHandler(this.ReStart);
            mnuItms[mnuItms.Length - 6].MenuItems.Add(itmetools2);

            MenuItem itmetools3 = new MenuItem();
            itmetools3.Text = "词库信息";
            itmetools3.Click += new System.EventHandler(this.CurDictInfo);
            mnuItms[mnuItms.Length - 6].MenuItems.Add(itmetools3);

           

            MenuItem itmetools4 = new MenuItem();
            itmetools4.Text = "词库生成";
            itmetools4.Click += new System.EventHandler(this.CreateDictInfo);
            mnuItms[mnuItms.Length - 6].MenuItems.Add(itmetools4);

            MenuItem itmetools5 = new MenuItem();
            itmetools5.Text = "并击测速";
            itmetools5.Click += new System.EventHandler(this.ImageCom);
            mnuItms[mnuItms.Length - 6].MenuItems.Add(itmetools5);

            mnuItms[mnuItms.Length - 5] = new MenuItem();
            mnuItms[mnuItms.Length - 5].Text = "帮助说明";
            mnuItms[mnuItms.Length - 5].Visible = true;
            mnuItms[mnuItms.Length - 5].Click += new System.EventHandler(this.OpenHelp);

            mnuItms[mnuItms.Length - 4] = new MenuItem();
            mnuItms[mnuItms.Length - 4].Text = "用户词库";
            mnuItms[mnuItms.Length - 4].Visible = true;
            mnuItms[mnuItms.Length - 4].Click += new System.EventHandler(this.UserDictM);

            mnuItms[mnuItms.Length - 3] = new MenuItem();
            mnuItms[mnuItms.Length - 3].Text = "汉字编码查询";
            mnuItms[mnuItms.Length - 3].Visible = true;
            mnuItms[mnuItms.Length - 3].Click += new System.EventHandler(this.QueryCode);

            mnuItms[mnuItms.Length - 2] = new MenuItem("-");

 
            mnuItms[mnuItms.Length - 1] = new MenuItem();
            mnuItms[mnuItms.Length - 1].Text = "退出";
            mnuItms[mnuItms.Length - 1].Click += new System.EventHandler(this.ExitSelect);

            notifyiconMnu = new ContextMenu(mnuItms);
            TrayIcon.ContextMenu = notifyiconMnu;
            this.label1.ContextMenu = notifyiconMnu;
            TrayIcon.MouseDoubleClick += new MouseEventHandler(TrayIcon_MouseDoubleClick);
            //为托盘程序加入设定好的ContextMenu对象 
        }
        private void ReStart(object sender, System.EventArgs e)
        {
            hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(ModuleName), 0);
            LoadLink();
          
        }

        private void CurDictInfo(object sender, System.EventArgs e)
        {
            InfoFrm frm = new InfoFrm(File.ReadAllText(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "ditinfo.shp"), Encoding.UTF8));
            frm.Show();
        }
        private void ImageCom(object sender, System.EventArgs e)
        {
            //ImgCom frm = new ImgCom();
            LXFrm frm = new LXFrm();
            frm.Show();
        }
        private void CreateDictInfo(object sender, System.EventArgs e)
        {
            DictTool frm = new DictTool();
            frm.Show();
        }
        private void CreateDictEdit(object sender, System.EventArgs e)
        {
            DictEdit frm = new DictEdit();
            frm.Show();
        }
        private void OpenInput(object sender, System.EventArgs e)
        {
            MenuItem mt=(MenuItem)sender;
            if (mt.Checked) return;
            foreach(var me in  mt.Parent.MenuItems)
            {
                MenuItem st = (MenuItem)me;
                st.Checked = false;
            }
            mt.Checked = true;
            InputMode.CDPath = mt.Text;
            this.SaveSetting();
            this.InputIni();
        }
        private void PFInput(object sender, System.EventArgs e)
        {
            MenuItem mt = (MenuItem)sender;
            if (mt.Checked) return;
            foreach (var me in mt.Parent.MenuItems)
            {
                MenuItem st = (MenuItem)me;
                st.Checked = false;
            }
            mt.Checked = true;
            InputMode.PFPath = mt.Text;
            this.SaveSetting();
            this.LoadSkin();
        }
        private void OpenzfInput(object sender, System.EventArgs e)
        {
            MenuItem mt = (MenuItem)sender;
            if (mt.Checked) return;
            foreach (var me in mt.Parent.MenuItems)
            {
                MenuItem st = (MenuItem)me;
                st.Checked = false;
            }
            mt.Checked = true;
            InputMode.ZFPath = mt.Text;
            this.SaveSetting();
            this.InputIni();
        }

        /// <summary>
        /// 打开记事本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenJSB(object sender, System.EventArgs e)
        {
            //运行记事本  
            System.Diagnostics.Process.Start("notepad.exe");  
        }
        /// <summary>
        /// 打开计算器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenJSQ(object sender, System.EventArgs e)
        {
            //运行计算器   
            System.Diagnostics.Process.Start("calc.exe");  
        }
        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            #region 激活关闭输入法
            //激活、关闭输入法
          
            Input.isActiveInput = !Input.isActiveInput;
            if (Input.isActiveInput)
            {
                InputStatusFrm.Dream = false; 
                InputStatusFrm.LastLinkString = string.Empty;
                InputStatus.Clear();
                this.ShowWindow();
                if (InputMode.imgsend)
                    ImageInput.ShowWindow();
                TrayIcon.Icon = new Icon(System.IO.Path.Combine(InputMode.AppPath, "log32.ico"));
            }
            else
            {
                InputStatusFrm.Dream = false; InputStatusFrm.LastLinkString = string.Empty;
                InputStatus.Clear();
                InputStatus.Hide();
                ImageInput.Hide();
                this.HideWindow();
                TrayIcon.Icon = new Icon(System.IO.Path.Combine(InputMode.AppPath, "nlog32.ico"));
            }
            #endregion
        }
        private void UpdataSoft(object sender, System.EventArgs e)
        {
            PlanThread.UpdataSoft(true);
            System.Threading.Thread.Sleep(1000);
            PlanThread.UpdataDict(true);
        }
        private void OpenHelp(object sender, System.EventArgs e)
        {

            try
            {
                System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
                Info.WorkingDirectory = System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath);
                Info.FileName = "help.chm";
                if (File.Exists(System.IO.Path.Combine(Info.WorkingDirectory, "help.chm")))
                {
                    Info.FileName = "help.chm";
                }
                else if (File.Exists(System.IO.Path.Combine(Info.WorkingDirectory, "help.pdf")))
                {
                    Info.FileName = "help.pdf";
                }
                else if (File.Exists(System.IO.Path.Combine(Info.WorkingDirectory, "help.doc")))
                {
                    Info.FileName = "help.doc";
                }
                else if (File.Exists(System.IO.Path.Combine(Info.WorkingDirectory, "help.docx")))
                {
                    Info.FileName = "help.docx";
                }
                else if (File.Exists(System.IO.Path.Combine(Info.WorkingDirectory, "help.txt")))
                {
                    Info.FileName = "help.txt";
                }

                Proc = System.Diagnostics.Process.Start(Info);
            }
            catch
            {

                MessageBox.Show("未找到帮助文档(help.pdf/help.doc/help.chm)!", "速录宝");

            }
        }

        private void OpenBJLearn(object sender, System.EventArgs e)
        {

            try
            {
                System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
                Info.WorkingDirectory = System.IO.Path.Combine(InputMode.AppPath, "速录练习软件");
                Info.FileName = "并击练习.exe";
           
                Proc = System.Diagnostics.Process.Start(Info);
            }
            catch
            {

                MessageBox.Show("未找到程序!", "速录宝");

            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            
 
            bool ok = false;
            if (e == null) ok = true;
            else
            {
                try
                {
                    MouseEventArgs ee = (MouseEventArgs)e;
                    if (ee.Button == MouseButtons.Left)
                    {
                        ok = true;
                    }
                }
                catch { ok = true; }
            }
            if (ok)
            {
                //处理任务
                ConfigFrm conf = new ConfigFrm(this);
                conf.ShowDialog();
            }
        }

         
        private void AboutInfo(object sender, System.EventArgs e)
        {
            Win.Login info = new Login(false,Input);
            info.Show();
        }
        private void OpenKmmUrl(object sender, System.EventArgs e)
        {
            //调用系统默认的浏览器
            System.Diagnostics.Process.Start("iexplore.exe", "http://srkmm.ysepan.com/");
        }
        //通过托盘的'退出'按钮关闭程序
        public void ExitSelect(object sender, System.EventArgs e)
        {
            if (sender != null)
                PlanThread.Stop();//停止线程

            Application.ExitThread();

            Input.Break = true;
            UInstallKeyHook();//释放快捷键注册
            //隐藏托盘程序中的图标 
            TrayIcon.Visible = false;
            TrayIcon.Dispose();
                   
            
            //关闭系统 
            this.Dispose();
            this.Close();
            
            Application.Exit();
        }

        /// <summary>
        /// 用户词库管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserDictM(object sender, System.EventArgs e)
        {
 
            Win.UserDict frm = new UserDict(Input,this);
            frm.ShowDialog();
        }

        /// <summary>
        /// 用户词库管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryCode(object sender, System.EventArgs e)
        {
 
            Win.QueryFrm frm = new QueryFrm();
            frm.Show();
        }

        /// <summary>
        /// 专业词库管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProDictM(object sender, System.EventArgs e)
        {
 
            Win.ProDictFrm frm = new ProDictFrm(Input, this);
            frm.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            inputst();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            inputstye();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            inputjf();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (curMouseTrac)
            {
                try
                {
                    curPoint = System.Windows.Forms.Cursor.Position;
                    this.Top = curPoint.Y;
                    this.Left = curPoint.X + 17;
                }
                catch { }
            }
        }

        public static string savemapdata()
        {
            string v = "";
            foreach (var item in Input.mapkeys)
            {
                v += item.ZM + "=" + item.Map + "=" + item.keydown + "=l:" + item.left + "\r\n";
            }
            return v;
        }
    }


}

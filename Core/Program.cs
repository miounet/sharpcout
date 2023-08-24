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

            #region 根据词频调整
            //Dictionary<string, long> dictcp = new Dictionary<string, long>();
            //Dictionary<string, long> blcp = new Dictionary<string, long>();
            //var blc = File.ReadAllText(@"D:\work\速录宝2.0\dict\空明码\MasterDit.shp", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //foreach (var item in blc)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (item.IndexOf(' ') <= 0) continue;
            //    string code = item.Split(' ')[0];
            //    string txt = item.Split(' ')[1];
            //    if (code.Length>3 && txt.Length==2 && !blcp.ContainsKey(txt))
            //        blcp.Add(txt, 100000000);
            //}
            ////var bigc = File.ReadAllText(@"D:\soft\office\词库\[雾凇拼音][20230725][简体 全拼]\合并词库.yaml", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            ////
            ////foreach (var item in bigc)
            ////{
            ////    if (string.IsNullOrEmpty(item)) continue;
            ////    if (item.IndexOf('	') <= 0) continue;
            ////    string txt = item.Split('	')[0].Replace("#", "");
            ////    if (!dictcp.ContainsKey(txt))
            ////        dictcp.Add(txt, 10000);
            ////}
            //var bigc = File.ReadAllText(@"D:\work\速录宝2.0\dict\空明码宏版\MasterDit.shp", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //StringBuilder ssb = new StringBuilder();
            //foreach (var item in bigc)
            //{
            //    bool cl = true;
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (item.Split(' ').Length < 3) cl = false;

            //    string txt = item.Split(' ')[0];
            //    if (txt.Length < 4) cl = false;
            //    if (txt.Substring(0, 1).ToString() == "i" || txt.Substring(0, 1).ToString() == "v" || txt.Substring(0, 1).ToString() == "u"
            //        || txt.Substring(0, 1).ToString() == ";" || txt.Substring(0, 1).ToString() == "7" || txt.Substring(0, 1).ToString() == ":")
            //        cl = false;

            //    if (item.Split(' ')[1].Length == 1) cl = false;

            //    if (cl)
            //    {
            //        List<vvclase> vvlist = new List<vvclase>();

            //        for (int i = 1; i < item.Split(' ').Length; i++)
            //        {
            //            vvclase vv = new vvclase();
            //            vv.txt = item.Split(' ')[i];
            //            vv.num = 0;
            //            if (blcp.ContainsKey(vv.txt))
            //            {
            //                vv.num = blcp[vv.txt];
            //            }
            //            else if (dictcp.ContainsKey(vv.txt))
            //            {
            //                vv.num = dictcp[vv.txt];
            //            }

            //            vvlist.Add(vv);
            //        }
            //        if (vvlist.Count > 0)
            //        {
            //            vvlist = vvlist.OrderByDescending(o => o.num).ToList();
            //            ssb.Append(txt + " ");
            //            string cdtxt = "";
            //            int count = 0;
            //            foreach (var item1 in vvlist)
            //            {
            //                count++;
            //                cdtxt += item1.txt + " ";
            //                //if (count > 18) break;
            //            }

            //            ssb.Append(cdtxt.Trim() + "\n");
            //        }
            //    }
            //    else
            //    {
            //        ssb.Append(item + "\n");
            //    }
            //}
            //File.WriteAllText(@"D:\soft\office\词库\MasterDit_111.shp", ssb.ToString(), Encoding.UTF8);
            #endregion
            #region 多音字处理
            //var pyck = File.ReadAllText(@"D:\soft\office\词库\[雾凇拼音][20230725][简体 全拼]\合并词库.yaml", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //var dict = File.ReadAllText(@"D:\work\速录宝2.0\dict\空明码宏版\MasterDit___多多格式.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //StringBuilder ssb = new StringBuilder();
            //Dictionary<string, string> pykdict = new Dictionary<string, string>();

            //Dictionary<string, string> dictk = new Dictionary<string, string>();
            //foreach (var item in pyck)
            //{
            //    if (item.Length > 1)
            //    {
            //        string str = item.Split('	')[0];
            //        string code = item.Replace(str + "	", "").Split('	')[0];
            //        if (!pykdict.ContainsKey(str) && code.Length > 0 && code.Replace("	", "") != str)
            //        {
            //            pykdict.Add(str, code);
            //        }
            //    }
            //}
            //foreach (var item in dict)
            //{
            //    if (item.Length > 1)
            //    {
            //        string str = item.Split('	')[0];
            //        string code = item.Split('	')[1];
            //        if (str.Length == 3 && str.IndexOf("什么") >= 0 && code.Length > 3)
            //        {
            //            if (str.StartsWith("什么"))
            //            {
            //                code = code.Substring(0, 2) + "m" + code.Substring(3);
            //            }
            //            else if (str.EndsWith("什么"))
            //            {
            //                code = code.Substring(0, code.Length - 1) + "m";
            //            }
            //        }
            //        else if (str.Length == 4 && str.IndexOf("什么") >= 0 && code.Length > 3)
            //        {
            //            if (str.StartsWith("什么"))
            //            {
            //                code = code.Substring(0, 1) + "m" + code.Substring(2);
            //            }
            //            else if (str.EndsWith("什么"))
            //            {
            //                code = code.Substring(0, code.Length - 1) + "m";
            //            }
            //            else
            //            {
            //                string code1 = "";
            //                for (int i = 0; i < str.Length; i++)
            //                {
            //                    if (str.Substring(i, 1) == "么")
            //                    {
            //                        code1 += "m";
            //                    }
            //                    else
            //                        code1 += code.Substring(i, 1);
            //                }
            //                code = code1;
            //            }
            //        }
            //        else if (str.Length > 4 && str.IndexOf("什么") >= 0 && code.Length == str.Length)
            //        {
            //            if (str.StartsWith("什么"))
            //            {
            //                code = code.Substring(0, 1) + "m" + code.Substring(2);
            //            }
            //            else if (str.EndsWith("什么"))
            //            {
            //                code = code.Substring(0, code.Length - 1) + "m";
            //            }
            //            else if (str.Length > 4 && code.Length > 4)
            //            {
            //                string code1 = "";
            //                for (int i = 0; i < str.Length; i++)
            //                {
            //                    if (str.Substring(i, 1) == "么")
            //                    {
            //                        code1 += "m";
            //                    }
            //                    else
            //                        code1 += code.Substring(i, 1);
            //                }
            //                code = code1;
            //            }
            //        }
            //        else if (str.Length > 4 && str.IndexOf("什么") >= 0 && code.Length != str.Length)
            //        {
            //            if (str.StartsWith("什么"))
            //            {
            //                code = code.Substring(0, 1) + "m" + code.Substring(2);
            //            }
            //            else if (str.EndsWith("什么"))
            //            {
            //                code = code.Substring(0, code.Length - 1) + "m";
            //            }
            //        }
            //        if (str.Length > 1 && code.Length > 3)
            //        {
            //            if (pykdict.ContainsKey(str))
            //            {
            //                try
            //                {
            //                    纠正读音
            //                    var py = pykdict[str].Split(' ');
            //                    string code1 = "";

            //                    for (int i = 0; i < str.Length; i++)
            //                    {
            //                        string ym = py[i];
            //                        string flag = "";
            //                        if (ym.StartsWith("a") || ym.StartsWith("o"))
            //                        {
            //                            flag = "a";
            //                        }
            //                        else if (ym.StartsWith("e"))
            //                        {
            //                            flag = "e";
            //                        }
            //                        else
            //                        {

            //                            string sym = "";
            //                            if (ym.Length > 2 &&
            //                                (ym.Substring(0, 2) == "sh" || ym.Substring(0, 2) == "ch" || ym.Substring(0, 2) == "zh"))
            //                            {
            //                                sym = ym.Substring(2, 1);
            //                            }
            //                            else
            //                            {
            //                                sym = ym.Substring(1, 1);
            //                            }

            //                            if (sym.StartsWith("a") || sym.StartsWith("o"))
            //                            {
            //                                flag = "a";
            //                            }
            //                            else if (sym.StartsWith("e"))
            //                            {
            //                                flag = "e";
            //                            }
            //                            else if (sym.StartsWith("i"))
            //                            {
            //                                flag = "i";
            //                            }
            //                            else
            //                                flag = "u";
            //                        }

            //                        if (str.Length == 2)
            //                        {
            //                            if (i == 0)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper() + code.Substring(1, 1).ToLower();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper() + code.Substring(1, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower() + code.Substring(1, 1).ToUpper();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower() + code.Substring(1, 1).ToLower();
            //                            }
            //                            else
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper() + code.Substring(3, 1).ToLower();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper() + code.Substring(3, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower() + code.Substring(3, 1).ToUpper();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower() + code.Substring(3, 1).ToLower();
            //                            }

            //                        }
            //                        else if (str.Length == 3)
            //                        {
            //                            if (i == 0)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper() + code.Substring(1, 1).ToLower();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper() + code.Substring(1, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower() + code.Substring(1, 1).ToUpper();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower() + code.Substring(1, 1).ToLower();
            //                            }
            //                            else if (i == 1)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                            }
            //                            else
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                            }
            //                        }
            //                        else if (str.Length == 4)
            //                        {
            //                            if (flag == "a" || flag == "o")
            //                            {
            //                                code1 += ym.Substring(0, 1).ToUpper();
            //                            }
            //                            else if (flag == "u")
            //                                code1 += ym.Substring(0, 1).ToUpper();
            //                            else if (flag == "e")
            //                                code1 += ym.Substring(0, 1).ToLower();
            //                            else
            //                                code1 += ym.Substring(0, 1).ToLower();
            //                        }
            //                        else if (str.Length > 4 && str.Length == code.Length)
            //                        {
            //                            if (flag == "a" || flag == "o")
            //                            {
            //                                code1 += ym.Substring(0, 1).ToUpper();
            //                            }
            //                            else if (flag == "u")
            //                                code1 += ym.Substring(0, 1).ToUpper();
            //                            else if (flag == "e")
            //                                code1 += ym.Substring(0, 1).ToLower();
            //                            else
            //                                code1 += ym.Substring(0, 1).ToLower();
            //                        }
            //                        else if (str.Length > 4 && code.Length == 4)
            //                        {
            //                            if (i == 0)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                            }
            //                            else if (i == 1)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                            }
            //                            else if (i == 2)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                            }
            //                            else if (i == str.Length - 1)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                            }
            //                        }
            //                        else if (str.Length > 4 && code.Length > 6)
            //                        {
            //                            if (i < 6)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(i, 1).ToUpper();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(i, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(i, 1).ToLower();
            //                                else
            //                                    code1 += ym.Substring(i, 1).ToLower();
            //                            }
            //                            else if (i == str.Length - 1)
            //                            {
            //                                if (flag == "a" || flag == "o")
            //                                {
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                }
            //                                else if (flag == "u")
            //                                    code1 += ym.Substring(0, 1).ToUpper();
            //                                else if (flag == "e")
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                                else
            //                                    code1 += ym.Substring(0, 1).ToLower();
            //                            }
            //                        }
            //                    }
            //                    if (code1 != "")
            //                        code = code1;
            //                }
            //                catch { }
            //            }
            //        }
            //        string sv = str + "	" + code;
            //        if (!dictk.ContainsKey(sv))
            //        {
            //            dictk.Add(sv, "");
            //            ssb.Append(sv + "\n");
            //        }
            //    }

            //}
            //File.WriteAllText(@"D:\work\速录宝2.0\dict\空明码宏版\MasterDit_1.txt", ssb.ToString(), Encoding.UTF8);
            #endregion

            #region 缩减词库

            //var bigc = File.ReadAllText(@"D:\work\速录宝2.0\dict\空明码宏版\MasterDit.shp", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //StringBuilder ssb = new StringBuilder();
            //foreach (var item in bigc)
            //{
            //    bool cl = true;
            //    if (string.IsNullOrEmpty(item)) continue;
            //    string txt = item.Split(' ')[0];
            //    if (txt.Length < 4) cl = false;
            //    if (txt.Length > 4) continue;
            //    if (item.Split(' ')[1].Length == 1) cl = false;
            //    if (item.Split(' ').Length > 1 && item.Split(' ')[1].Length == 1) cl = false;
            //    if (txt.Substring(0, 1).ToString() == "i" || txt.Substring(0, 1).ToString() == "v" || txt.Substring(0, 1).ToString() == "u"
            //        || txt.Substring(0, 1).ToString() == ";" || txt.Substring(0, 1).ToString() == "7" || txt.Substring(0, 1).ToString() == ":")
            //        cl = false;


            //    if (cl)
            //    {
            //        string item1 = "";
            //        item1 = txt + "";
            //        for (int i = 1; i < item.Split(' ').Length && i < 2; i++)
            //        {
            //            item1 += " " + item.Split(' ')[i];
            //        }
            //        if (item1.Length > 0)
            //        {
            //            ssb.Append(item1 + "\n");
            //        }
            //    }
            //    else
            //    {
            //        ssb.Append(item + "\n");
            //    }
            //}
            //File.WriteAllText(@"D:\work\速录宝2.0\dict\空明码宏版\MasterDit_s1.shp", ssb.ToString(), Encoding.UTF8);
            #endregion

            #region 筛选好词

            //Dictionary<string, long> mdict = new Dictionary<string, long>();
            //Dictionary<string, long> pydict = new Dictionary<string, long>();
            //var bigc = File.ReadAllText(@"D:\work\速录宝2.0\dict\空明码宏版\MasterDit___多多格式.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');

            //foreach (var item in bigc)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (item.IndexOf('	') <= 0) continue;

            //    string txt = item.Split('	')[0];

            //    if (!mdict.ContainsKey(txt))
            //        mdict.Add(txt, 0);
            //}

            //List<string> newdict = new List<string>();
            //var pyk = File.ReadAllText(@"D:\soft\office\词库\[雾凇拼音][20230725][简体 全拼]\合并词库.yaml", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //StringBuilder ssb = new StringBuilder();
            //foreach (var item in pyk)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (item.IndexOf('	') <= 0) continue;

            //    string txt = item.Split('	')[0].Replace("#", "");

            //    if (!pydict.ContainsKey(txt))
            //    {
            //        pydict.Add(txt, 0);

            //        if (!mdict.ContainsKey(txt))
            //        {
            //            int ispy = 1;
            //            newdict.Add(txt + " " + ispy);
            //        }
            //    }
            //}

            //Dictionary<string, long> blcp = new Dictionary<string, long>();
            //var blc = File.ReadAllText(@"D:\soft\office\词库\矧码词.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');

            //foreach (var item in blc)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (item.IndexOf('	') <= 0) continue;

            //    string txt = item.Split('	')[0].Replace("？", "").Replace("。", "").Replace("！", "").Replace("；", "").Replace("，", "");
            //    if (txt.Length == 1) continue;
            //    if (!blcp.ContainsKey(txt))
            //    {
            //        blcp.Add(txt, 0);

            //        if (!mdict.ContainsKey(txt))
            //        {
            //            int ispy = pydict.ContainsKey(txt) ? 1 : 0;
            //            newdict.Add(txt + " " + ispy);
            //        }
            //    }
            //}
            //foreach (var item in newdict)
            //{
            //    ssb.Append(item + " " + (blcp.ContainsKey(item.Split(' ')[0]) ? 1 : 0) + "\n");
            //}
            //File.WriteAllText(@"D:\soft\office\词库\矧码词_未加入的词.txt", ssb.ToString(), Encoding.UTF8);
            #endregion

            #region

            //var bigc = File.ReadAllText(@"C:\Users\Administrator\Documents\空明虎\虎码编码拆分表 2023.07.01.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //var xyk = File.ReadAllText(@"C:\Users\Administrator\Desktop\srkmm\生成词库单字列表___多多格式.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //StringBuilder cflist = new StringBuilder();
            //StringBuilder sbmlist = new StringBuilder();
            //Dictionary<string, long> dict1 = new Dictionary<string, long>();
            //Dictionary<string, long> dict2 = new Dictionary<string, long>();
            //Dictionary<string, long> cfdict = new Dictionary<string, long>();
            //foreach (var item in xyk)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (!cfdict.ContainsKey(item.Split('	')[0]))
            //    {
            //        cfdict.Add(item.Split('	')[0], 0);
            //    }
            //}
            //foreach (var item in bigc)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    string txt = item.Replace("〔", "").Replace("〕", "").Replace("·", "");

            //    var cf = txt.Split(' ');

            //    string cfstr = cf[0];
            //    string sbm = cf[0].Split('	')[1];
            //    if (sbm.Length < 4)
            //        sbm = sbm.Substring(sbm.Length - 1) + "	" + cf[2].Substring(cf[2].Length - 1);
            //    else sbm = "";

            //    if (!dict1.ContainsKey(cfstr))
            //    {
            //        if (cfdict.ContainsKey(cf[0].Split('	')[0]))
            //        {
            //            dict1.Add(cfstr, 0);
            //            cflist.Append(cfstr + "\n");
            //        }
            //    }
            //    if (!dict2.ContainsKey(sbm))
            //    {
            //        dict2.Add(sbm, 0);
            //        sbmlist.Append(sbm + "\n");
            //    }
            //}

            //File.WriteAllText(@"C:\Users\Administrator\Documents\空明虎\拆分表.txt", cflist.ToString(), Encoding.UTF8);
            //File.WriteAllText(@"C:\Users\Administrator\Documents\空明虎\双编识别码.txt", sbmlist.ToString(), Encoding.UTF8);
            #endregion

            #region

            //var myb = File.ReadAllText(@"C:\Users\Administrator\Documents\空明虎\杏码生成器-52码元虎码\out_zigen.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //var xyk = File.ReadAllText(@"C:\Users\Administrator\Documents\空明虎\双编识别码.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //var cfb = File.ReadAllText(@"C:\Users\Administrator\Documents\空明虎\拆分表.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');
            //StringBuilder cflist = new StringBuilder();

            //Dictionary<string, string> mydict = new Dictionary<string, string>();
            //Dictionary<string, string> sbmdict = new Dictionary<string, string>();
            //Dictionary<string, string> cfbdict = new Dictionary<string, string>();
            //foreach (var item in myb)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (!mydict.ContainsKey(item.Split('	')[0]))
            //    {
            //        mydict.Add(item.Split('	')[0], item.Split('	')[1]);
            //    }
            //}
            //var hm = File.ReadAllText(@"C:\Users\Administrator\Documents\空明虎\杏码生成器-52码元虎码\out_dict.txt", Encoding.UTF8).Replace("\r\n", "\n").Split('\n');

            //foreach (var item in xyk)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (!sbmdict.ContainsKey(item.Split('	')[0]))
            //    {
            //        sbmdict.Add(item.Split('	')[0], item.Split('	')[1]);
            //    }
            //}
            //foreach (var item in cfb)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    if (!cfbdict.ContainsKey(item.Split('	')[0]))
            //    {
            //        if (item.Split('	')[0] == "尹")
            //        {
            //            string abc = "";
            //        }
            //        cfbdict.Add(item.Split('	')[0], item.Split('	')[1]);
            //    }
            //}
            //foreach (var item in hm)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    string dz = item.Split('	')[0];
            //    string cf = item.Split('	')[1];

            //    string code = "";
            //    if (cf.Length == 1)
            //    {
            //        cf = cfbdict[dz];
            //        if (System.Text.Encoding.Default.GetBytes(cf).Length == 2 || System.Text.Encoding.Default.GetBytes(cf).Length == 1)
            //        {
            //            if (mydict.ContainsKey(cf.Substring(0, 1)) && sbmdict.ContainsKey(cf.Substring(0, 1)))
            //            {
            //                code += mydict[cf.Substring(0, 1)];
            //                code += sbmdict[cf.Substring(0, 1)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 2)) && sbmdict.ContainsKey(cf.Substring(0, 2)))
            //            {
            //                code += mydict[cf.Substring(0, 2)];
            //                code += sbmdict[cf.Substring(0, 2)];
            //            }
            //            cflist.Append(dz + "	" + code + "\n");
            //        }
            //        else if (System.Text.Encoding.Default.GetBytes(cf).Length == 4 || System.Text.Encoding.Default.GetBytes(cf).Length == 3)
            //        {
            //            if (mydict.ContainsKey(cf.Substring(0, 1)) && sbmdict.ContainsKey(cf.Substring(1, 1)))
            //            {
            //                code += mydict[cf.Substring(0, 1)];
            //                code += mydict[cf.Substring(1, 1)];
            //                code += sbmdict[cf.Substring(1, 1)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 1)) && sbmdict.ContainsKey(cf.Substring(1, 2)))
            //            {
            //                code += mydict[cf.Substring(0, 1)];
            //                code += mydict[cf.Substring(1, 2)];
            //                code += sbmdict[cf.Substring(1, 2)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 2)) && sbmdict.ContainsKey(cf.Substring(2, 1)))
            //            {
            //                code += mydict[cf.Substring(0, 2)];
            //                code += mydict[cf.Substring(2, 1)];
            //                code += sbmdict[cf.Substring(2, 1)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 2)) && sbmdict.ContainsKey(cf.Substring(2, 2)))
            //            {
            //                code += mydict[cf.Substring(0, 2)];
            //                code += mydict[cf.Substring(2, 2)];
            //                code += sbmdict[cf.Substring(2, 2)];
            //            }
            //            cflist.Append(dz + "	" + code + "\n");
            //        }
            //        else
            //        {
            //            if (mydict.ContainsKey(cf.Substring(0, 1)) && mydict.ContainsKey(cf.Substring(1, 1)) && mydict.ContainsKey(cf.Substring(2, 1)))
            //            {
            //                code += mydict[cf.Substring(0, 1)];
            //                code += mydict[cf.Substring(1, 1)];
            //                code += mydict[cf.Substring(2, 1)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 1)) && mydict.ContainsKey(cf.Substring(1, 1)) && mydict.ContainsKey(cf.Substring(2, 2)))
            //            {
            //                code += mydict[cf.Substring(0, 1)];
            //                code += mydict[cf.Substring(1, 1)];
            //                code += mydict[cf.Substring(2, 2)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 1)) && mydict.ContainsKey(cf.Substring(1, 2)) && mydict.ContainsKey(cf.Substring(3, 1)))
            //            {
            //                code += mydict[cf.Substring(0, 1)];
            //                code += mydict[cf.Substring(1, 2)];
            //                code += mydict[cf.Substring(3, 1)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 2)) && mydict.ContainsKey(cf.Substring(2, 1)) && mydict.ContainsKey(cf.Substring(3, 1)))
            //            {
            //                code += mydict[cf.Substring(0, 2)];
            //                code += mydict[cf.Substring(2, 1)];
            //                code += mydict[cf.Substring(3, 1)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 2)) && mydict.ContainsKey(cf.Substring(2, 2)) && mydict.ContainsKey(cf.Substring(4, 1)))
            //            {
            //                code += mydict[cf.Substring(0, 2)];
            //                code += mydict[cf.Substring(2, 2)];
            //                code += mydict[cf.Substring(4, 1)];
            //            }
            //            else if (mydict.ContainsKey(cf.Substring(0, 2)) && mydict.ContainsKey(cf.Substring(2, 2)) && mydict.ContainsKey(cf.Substring(4, 2)))
            //            {
            //                code += mydict[cf.Substring(0, 2)];
            //                code += mydict[cf.Substring(2, 2)];
            //                code += mydict[cf.Substring(4, 2)];
            //            }

            //            if (code.Length > 0)
            //                cflist.Append(dz + "	" + code + "\n");
            //        }


            //    }


            //}

            //foreach (var item in cfb)
            //{
            //    if (string.IsNullOrEmpty(item)) continue;
            //    string dz = item.Split('	')[0];
            //    string cf = item.Split('	')[1];

            //    if (dz == "尹")
            //    {
            //        string abc = "";
            //    }

            //    string code = "";
            //    if (System.Text.Encoding.Default.GetBytes(cf).Length == 2 || System.Text.Encoding.Default.GetBytes(cf).Length == 1)
            //    {
            //        if (mydict.ContainsKey(cf.Substring(0, 1)) && sbmdict.ContainsKey(cf.Substring(0, 1)))
            //        {
            //            code += mydict[cf.Substring(0, 1)];
            //            code += sbmdict[cf.Substring(0, 1)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 2)) && sbmdict.ContainsKey(cf.Substring(0, 2)))
            //        {
            //            code += mydict[cf.Substring(0, 2)];
            //            code += sbmdict[cf.Substring(0, 2)];
            //        }
            //        cflist.Append(dz + "	" + code + "\n");
            //    }
            //    else if (System.Text.Encoding.Default.GetBytes(cf).Length == 4 || System.Text.Encoding.Default.GetBytes(cf).Length == 3)
            //    {
            //        if (mydict.ContainsKey(cf.Substring(0, 1)) && sbmdict.ContainsKey(cf.Substring(1, 1)))
            //        {
            //            code += mydict[cf.Substring(0, 1)];
            //            code += mydict[cf.Substring(1, 1)];
            //            code += sbmdict[cf.Substring(1, 1)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 1)) && sbmdict.ContainsKey(cf.Substring(1, 2)))
            //        {
            //            code += mydict[cf.Substring(0, 1)];
            //            code += mydict[cf.Substring(1, 2)];
            //            code += sbmdict[cf.Substring(1, 2)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 2)) && sbmdict.ContainsKey(cf.Substring(2, 1)))
            //        {
            //            code += mydict[cf.Substring(0, 2)];
            //            code += mydict[cf.Substring(2, 1)];
            //            code += sbmdict[cf.Substring(2, 1)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 2)) && sbmdict.ContainsKey(cf.Substring(2, 2)))
            //        {
            //            code += mydict[cf.Substring(0, 2)];
            //            code += mydict[cf.Substring(2, 2)];
            //            code += sbmdict[cf.Substring(2, 2)];
            //        }
            //        cflist.Append(dz + "	" + code + "\n");
            //    }
            //    else
            //    {
            //        if (mydict.ContainsKey(cf.Substring(0, 1)) && mydict.ContainsKey(cf.Substring(1, 1)) && mydict.ContainsKey(cf.Substring(2, 1)))
            //        {
            //            code += mydict[cf.Substring(0, 1)];
            //            code += mydict[cf.Substring(1, 1)];
            //            code += mydict[cf.Substring(2, 1)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 1)) && mydict.ContainsKey(cf.Substring(1, 1)) && mydict.ContainsKey(cf.Substring(2, 2)))
            //        {
            //            code += mydict[cf.Substring(0, 1)];
            //            code += mydict[cf.Substring(1, 1)];
            //            code += mydict[cf.Substring(2, 2)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 1)) && mydict.ContainsKey(cf.Substring(1, 2)) && mydict.ContainsKey(cf.Substring(3, 1)))
            //        {
            //            code += mydict[cf.Substring(0, 1)];
            //            code += mydict[cf.Substring(1, 2)];
            //            code += mydict[cf.Substring(3, 1)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 2)) && mydict.ContainsKey(cf.Substring(2, 1)) && mydict.ContainsKey(cf.Substring(3, 1)))
            //        {
            //            code += mydict[cf.Substring(0, 2)];
            //            code += mydict[cf.Substring(2, 1)];
            //            code += mydict[cf.Substring(3, 1)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 2)) && mydict.ContainsKey(cf.Substring(2, 2)) && mydict.ContainsKey(cf.Substring(4, 1)))
            //        {
            //            code += mydict[cf.Substring(0, 2)];
            //            code += mydict[cf.Substring(2, 2)];
            //            code += mydict[cf.Substring(4, 1)];
            //        }
            //        else if (mydict.ContainsKey(cf.Substring(0, 2)) && mydict.ContainsKey(cf.Substring(2, 2)) && mydict.ContainsKey(cf.Substring(4, 2)))
            //        {
            //            code += mydict[cf.Substring(0, 2)];
            //            code += mydict[cf.Substring(2, 2)];
            //            code += mydict[cf.Substring(4, 2)];
            //        }

            //        if (code.Length > 0)
            //            cflist.Append(dz + "	" + code + "\n");
            //    }

            //    //else if (cf.Length == 3)
            //    //{
            //    //    cflist.Append(dz + "	" + mydict[cf.Substring(0, 1)] + mydict[cf.Substring(1, 1)] + mydict[cf.Substring(2, 1)] + sbmdict[cf.Substring(2, 1)] + "\n");
            //    //}
            //    //else if (cf.Length == 4)
            //    //{
            //    //    cflist.Append(dz + "	" + mydict[cf.Substring(0, 1)] + mydict[cf.Substring(1, 1)] + mydict[cf.Substring(2, 1)] + mydict[cf.Substring(3, 1)] + "\n");
            //    //}
            //}

            ////File.WriteAllText(@"C:\Users\Administrator\Documents\空明虎\虎码.txt", cflist.ToString(), Encoding.UTF8);

            #endregion
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

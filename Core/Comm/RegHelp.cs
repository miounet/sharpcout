using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Comm
{
    /// <summary>
    /// 正则配匹替换
    /// </summary>
    public class RegHelp
    {
        static List<MapintKey> regsortkeys = null;
        public static string RegexReplace(string value,string regstr)
        {
            string x = value;
            if (regstr.TrimStart().StartsWith("alphabet:"))
            {
                //排序
                //# 按键读取顺序，按照1234509876列的顺序
                // alphabet: "qawsedrftg'-p;olikujyh ="

                if (regsortkeys == null)
                {
                    regsortkeys = new List<MapintKey>();
                    string mapstr = regstr.Substring(regstr.IndexOf("\"") + 1).TrimEnd('\"');

                    for (short i = 0; i < mapstr.Length; i++)
                    {
                        MapintKey map = new MapintKey();
                        map.ZM = mapstr.Substring(i, 1);
                        map.Pos = i;
                        regsortkeys.Add(map);
                    }
                }
                string s = string.Empty;
                regsortkeys.FindAll(f => x.IndexOf(f.ZM) >= 0).OrderBy(o => o.Pos).ToList().ForEach(f => s += f.ZM);
                x = s;
            }
            else if (regstr.TrimStart().StartsWith("xlit"))
            {
                //xlit/'- =;/ABCDE/
                regstr = regstr.Replace("xlit", "");
                string[] reglist = new string[0];
                if (regstr.LastIndexOf('/') > 0)
                {
                    reglist = regstr.Split('/');
                }
                else if (regstr.LastIndexOf(' ') > 0)
                {
                    reglist = regstr.Split(' ');
                }
                else if (regstr.LastIndexOf('=') > 0)
                {
                    reglist = regstr.Split('=');
                }
                if (reglist.Length > 2)
                {
                    string str1 = reglist[1];
                    string str2 = reglist[2];
                    string tx = "";
                    for(int i = 0; i < x.Length; i++)
                    {
                        bool thok = false;
                        for (int j = 0; j < str1.Length; j++)
                        {
                            if(x.Substring(i,1)== str1.Substring(j, 1))
                            {
                                tx += str2.Substring(j, 1);
                                thok = true;
                                break;
                            }
                        }
                        if (!thok)
                        {
                            tx += x.Substring(i, 1);
                        }
                    }
                    x = tx;
                }
            }
            else if (regstr.TrimStart().StartsWith("xform"))
            {
                regstr = regstr.Replace("xform", "");
                string[] reglist = new string[0];
                if (regstr.LastIndexOf('/') > 0)
                {
                    reglist = regstr.Split('/');
                }
                else if (regstr.LastIndexOf("  ") >= 0)
                {
                    reglist = regstr.Replace("  "," ").Split(' ');
                }
                else if (regstr.LastIndexOf(' ') > 0)
                {
                    reglist = regstr.Split(' ');
                }
                else if (regstr.LastIndexOf('=') > 0)
                {
                    reglist = regstr.Split('=');
                }
                if (reglist.Length > 2)
                {
                    RegexOptions ops = RegexOptions.Multiline;
                    Regex r = new Regex(reglist[1], ops);
                    if (r.IsMatch(x))
                    {
                        x = r.Replace(x, reglist[2]);
                    }
                }

            }
            return x;
        }
        
    }
}

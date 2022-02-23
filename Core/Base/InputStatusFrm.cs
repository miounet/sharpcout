using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.Base
{
    public class InputStatusFrm:System.Windows.Forms.Form
    {
        public string inputstr = string.Empty;//当前的
        public string input = string.Empty;//本次输入的码元
        public static string zdzjstr = string.Empty;//自动造句
        private static InputMode Input = null;
        /// <summary>
        /// 在联想状态
        /// true联想状态
        /// false录入状态
        /// </summary>
        public static bool Dream = false;
        public int ViewType = 0;
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize = 6;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageNum = 1;

        /// <summary>
        /// 0为初级模式
        /// 1为高级模式
        /// </summary>
        public int LevelTop = 1;
        /// <summary>
        /// 最近汉字数组
        /// </summary>
        public string[] valuearry;
        /// <summary>
        /// 本屏显示的汉字
        /// </summary>
        public static string[] cachearry;

        [DllImport("user32.dll")]
        private static extern UInt32 SendInput(UInt32 nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keysend"></param>
        public static void SendText(string message, bool keysend = false)
        {
 
            if (string.IsNullOrEmpty(message)) return;
            Input.IsPressLAlt = false;
            Input.IsPressRAlt = false;
            if (keysend || message.StartsWith("sendkey"))
            {
                SendKeys.Send(message.StartsWith("sendkey") ? message.Split(':')[1] : message);
                return;
            }
            if (Input.OutType == 0)
            {
                INPUT[] input_down = new INPUT[message.Length];
                INPUT[] input_up = new INPUT[message.Length];
                for (int i = 0; i < message.Length; i++)
                {
                    input_down[i].type = (int)InputType.INPUT_KEYBOARD;
                    input_down[i].ki.dwFlags = (int)KEYEVENTF.UNICODE;
                    input_down[i].ki.wScan = (ushort)message[i];
                    input_down[i].ki.wVk = 0;
                    input_up[i].type = input_down[i].type;
                    input_up[i].ki.wScan = input_down[i].ki.wScan;
                    input_up[i].ki.wVk = 0;
                    input_up[i].ki.dwFlags = (int)(KEYEVENTF.KEYUP | KEYEVENTF.UNICODE);
                }

                for (int i = 0; i < input_down.Length; i++)
                {
                    SendInput(1, ref input_down[i], Marshal.SizeOf(input_down[i]));//keydown 
                    SendInput(1, ref input_up[i], Marshal.SizeOf(input_up[i]));//keyup    
                }
            }
            else if (Input.OutType == 1)
            {
                try
                {
                    Clipboard.SetText(message);
                    Input.SelfOut = true;
                    //发送ctrl+v 进行粘贴
                    keybd_event((byte)Keys.ControlKey, 0, 0, 0);//按下
                    keybd_event((byte)Keys.V, 0, 0, 0);
                    keybd_event((byte)Keys.ControlKey, 0, 0x2, 0);//松开
                    keybd_event((byte)Keys.V, 0, 0x2, 0);
                    System.Threading.Thread.Sleep(30);

                }
                catch { }
                finally { Input.SelfOut = false; }
            }
            
            LastSPValue = message;
            LastLinkString += message;
            if (Input.IsChinese == 1 && !CheckChinese(message,true))
            {
                LastLinkString = string.Empty;
                if (Dream)
                {
                    Input.Show = false;
                    Dream = false;
                }
                AutoZJ();
            }
            else if (Input.IsChinese == 0 && !IsLowerLetter(message) && !IsUpperLetter(message))
            {
                LastLinkString = string.Empty;
                if (Dream)
                {
                    Dream = false;
                    Input.Show = false;
                }
            }
            if (Input.IsChinese == 1 && LastLinkString.Length > 4) LastLinkString = LastLinkString.Substring(LastLinkString.Length-4);

        }

        //自动造4字以上的句，词作为联想字库，输入重请后消失。
        public static void AutoZJ()
        {
            if (InputMode.OpenLink)
            {
                zdzjstr = zdzjstr.Trim();
                if (zdzjstr.Length > 3)
                {
                    if (!Input.linkdictp.ContainsKey(zdzjstr.Substring(0, 3)))
                    {
                        Input.linkdictp.Add(zdzjstr.Substring(0, 3), new List<string>() { zdzjstr });
                    }
                    else
                    {
                        var tl = Input.linkdictp[zdzjstr.Substring(0, 3)];
                        bool add = true;
                        foreach (var item in tl)
                        {
                            if (item == zdzjstr)
                            {
                                add = false;
                                break;
                            }
                        }
                        if (add) tl.Add(zdzjstr);
                    }
                }
            }
            zdzjstr = string.Empty;
        }
        /// <summary>
        /// 判定是否为汉字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckChinese(string str, bool sp = false)
        {
            bool vv = Regex.IsMatch(str, @"^[\u4e00-\u9fa5]+$");
            //if (sp) zdzjstr += str;
            return vv;
        }
        /// <summary>
        /// 判断输入的编码是否是26个小写字母
        /// </summary>
        /// <param name="codechar"></param>
        /// <returns></returns>
        public static bool IsLowerLetter(string codechar)
        {
           
            return System.Text.RegularExpressions.Regex.IsMatch(codechar, "[a-z]");
        }

        /// <summary>
        /// 判断输入的编码是否是26个大写字母
        /// </summary>
        /// <param name="codechar"></param>
        /// <returns></returns>
        public static bool IsUpperLetter(string codechar)
        {
          
            return System.Text.RegularExpressions.Regex.IsMatch(codechar, "[A-Z]");
        }
        /// <summary>
        /// 上屏
        /// </summary>
        /// <param name="pos"></param>
        public void ShangPing(int pos,int index=0,bool clear=true)
        {
            int tpos = pos != 0 ? pos - 1 : pos;
            if (InputStatusFrm.cachearry==null ||
                (InputStatusFrm.cachearry.Length > 0 && string.IsNullOrEmpty(InputStatusFrm.cachearry[0])))
            {
                Clear();
                return;
            }
            for (int i = 0; i < InputStatusFrm.cachearry.Length; i++)
            {
                if (pos == 0)
                {
                    if (string.IsNullOrEmpty(InputStatusFrm.cachearry[i]))
                    {
                        if (LSView)
                        {
                            for (int j = 0; j < InputStatusFrm.cachearry[0].Split('|')[1].Length; j++)
                                InputStatusFrm.SendText("{BACKSPACE}", true);
                        }
                        SendText(InputStatusFrm.cachearry[i - 1].Split('|')[1].Substring(index));
                        break;
                    }
                    else if (i == PageSize - 1)
                    {
                        if (LSView)
                        {
                            for (int j = 0; j < InputStatusFrm.cachearry[0].Split('|')[1].Length; j++)
                                InputStatusFrm.SendText("{BACKSPACE}", true);
                        }
                        SendText(InputStatusFrm.cachearry[i].Split('|')[1].Substring(index));
                        break;
                    }
                }
                else if (tpos >= PageSize)
                {
                    if (!string.IsNullOrEmpty(InputStatusFrm.cachearry[PageSize - 1 - i]))
                    {
                        if (LSView)
                        {
                            for (int j = 0; j < InputStatusFrm.cachearry[0].Split('|')[1].Length; j++)
                                InputStatusFrm.SendText("{BACKSPACE}", true);
                        }
                        SendText(InputStatusFrm.cachearry[PageSize - 1 - i].Split('|')[1].Substring(index));
                        break;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(InputStatusFrm.cachearry[tpos]))
                    {
                        if (LSView)
                        {
                            for (int j = 0; j < InputStatusFrm.cachearry[0].Split('|')[1].Length; j++)
                                InputStatusFrm.SendText("{BACKSPACE}", true);
                        }
                        SendText(InputStatusFrm.cachearry[tpos].Split('|')[1].Substring(index));
                        break;
                    }
                    else if (!string.IsNullOrEmpty(InputStatusFrm.cachearry[PageSize - 1 - i]))
                    {
                        if (LSView)
                        {
                            for (int j = 0; j < InputStatusFrm.cachearry[0].Split('|')[1].Length; j++)
                                InputStatusFrm.SendText("{BACKSPACE}", true);
                        }
                        SendText(InputStatusFrm.cachearry[PageSize - 1 - i].Split('|')[1].Substring(index));
                        break;
                    }
                }
                
            }
            if (clear)
            {
                LSView = false;
                inputstr = string.Empty;
                input = string.Empty;
                Clear();
            }
            else 
                LSView = true;

            if (index > 0)
            {
                Dream = false;
                LastLinkString = string.Empty;
            }
            else if ((LastLinkString.Length > 2 || (Input.IsChinese == 0 && LastLinkString.Length > 0)) && !LSView)
                GetDreamValue(LastLinkString);
        }

        public void Clear()
        {
            inputstr = string.Empty;
            input = string.Empty;
            HideWindow();
        }
        public void ClearOnly()
        {
            inputstr = string.Empty;
            input = string.Empty;
 
        }
        public InputStatusFrm(InputMode input)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.Text = string.Empty;
            this.TopLevel = true;
            this.TopMost = true;
            this.Width = 170;
            this.Height = 133;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ControlBox = true;
           
            Input = input;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            UpdateStyles();

            //this.Show();
        }
        private const int WM_MOUSEACTIVATE = 0x21;
        private const int MA_NOACTIVATE = 3;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = new IntPtr(MA_NOACTIVATE);
                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// 删除最后上屏的字词
        /// </summary>
        public void DelLastInput()
        {
            if (LastSPValue.Length > 0)
                for (int j = 0; j < LastSPValue.Length; j++)
                    SendText("{BACKSPACE}", true);
            else
                SendText("{BACKSPACE}", true);

            LastSPValue = string.Empty;
            LastLinkString = string.Empty;
        }
         /// <summary>
        /// 删除指定字数
        /// </summary>
        public void DelLastInput(int num)
        {
            for (int j = 0; j < num; j++)
                SendText("{BACKSPACE}", true);
        }
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hWnd, short cmdShow);
        [DllImport("user32.dll")]
        private static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        private const int SW_SHOWNOACTIVATE = 4;
        const int SW_HIDE = 0;  
        private const int SWP_NoActiveWINDOW = 0x10;
        private const int HWND_TOPMOST = -1;
        /// <summary>
        /// view input
        /// </summary>
        public void ShowWindow(bool f)
        {
            Win.WinInput.InputStatus.Left = this.Left;
            Win.WinInput.InputStatus.Top = this.Top;

            Input.ShowInput(f);
            if (!this.Visible && f)
            {
                ShowWindow(this.Handle, SW_SHOWNOACTIVATE);
                SetWindowPos(this.Handle, HWND_TOPMOST, this.Left, this.Top, this.Width, this.Height, SWP_NoActiveWINDOW);
            }
            else if (!f)
            {

                this.Hide();
                ShowWindow(this.Handle, SW_HIDE);

            }
        }

        public void HideByApi()
        {
            //this.Hide();
            //ShowWindow(this.Handle, SW_HIDE);
        }
        /// <summary>
        /// 隐藏窗体
        /// </summary>
        public void HideWindow()
        {

            Input.ClearShow();

            this.ShowWindow(false);
        }
        /// <summary>
        /// 上一次的字词
        /// </summary>
        public static string PreFirstValue = string.Empty;
        /// <summary>
        /// 最后一次上屏的字词
        /// </summary>
        public static string LastSPValue = string.Empty;
        /// <summary>
        /// 最后上屏的四个字
        /// </summary>
        public static string LastLinkString = string.Empty;

        public static string LastLinkNum = string.Empty;
        public static bool LSView = false;
        /// <summary>
        /// 显示候选框的汉字
        /// smspace  
        /// </summary>
        public void ShowInput(bool sp,bool clear=true,int ncount=0,bool smspace=false)//可上屏
        {
            if (LSView)
            {
                LSView = false;
            }
            this.HideByApi();
            Dream = false;
            PageNum = 1;
            valuearry = Input.GetInputValue(this.inputstr);
            int count = 0;
            PreFirstValue = string.Empty;
            if (valuearry != null)
            {
                cachearry = new string[PageSize];
                for (int i = 0; i < PageSize && i < valuearry.Length; i++)
                {
                    count++;
                    cachearry[i] = valuearry[i];
                }
                if (count == 1)
                {
                    //无重码直接上屏
                    ShangPing(1);
                }
                else if(smspace && valuearry.Length == 2)
                {
                    //3码时，带空格，只有1重码，输出第2位字词。
                    ShangPing(2,0,false);
                    //this.ShowWindow(true);
                }
                //else if (smspace && valuearry.Length > 2 
                //    && valuearry[0].Split('|')[1].Length== 1 && valuearry[1].Split('|')[1].Length == 1
                //    &&  valuearry[2].Split('|')[1].Length >1)
                //{
                //    //3码时，带空格，只有1重码，输出第2位字词。
                //    ShangPing(2, 0, false);
                //    //this.ShowWindow(true);
                //}
                //else if(this.inputstr.Length==3 && smspace)
                //{
                //    ShangPing(1, 0, false);
                //}
                else if (smspace && count == 2)
                {
                    ShangPing(2);
                }
                else
                {
                    if (sp)
                        ShangPing(1, 0, clear);
                    else
                        this.ShowWindow(true);
                }
            }
            else
            {
                if (cachearry!=null && cachearry.Length > 0)
                {
                    if (!string.IsNullOrEmpty(cachearry[0])) PreFirstValue = cachearry[0].Split('|')[1];
                }
                if (cachearry == null) cachearry = new string[PageSize];
                if (PreFirstValue.Length > 0 && this.inputstr != this.input &&  this.inputstr.Length>=4)
                {
                    //错码上屏
                    SendText(PreFirstValue);
                    this.inputstr = this.input;
                    if (this.inputstr.Length > 0)
                        ShowInput(false);
                }
                else
                {
                    this.Clear();
                }
            }
        }
        public  void GetDreamValue(string v)
        {
#if DEBUG
            return  ;
#endif

            if (!InputMode.OpenLink) return;

            int count = 0;
            string valuestr = string.Empty;
            string tinput = v.Length > 3 ? v.Substring(1) : v;
            if (Input.IsChinese == 1)
            {
                if (Input.linkdictp.ContainsKey(tinput))
                {
                    count++;
                    valuestr += count + "z|" + v + "|\n";
                    foreach (var item in Input.linkdictp[tinput])
                    {
                        if (count >= this.PageSize) break;
                        count++;
                        valuestr += count + "z|" + item + "|\n";
                    }
                }
                //for (int i = 0; i < Input.linkdict.Length; i++)
                //{
                //    if (count >= this.PageSize) break;

                //    if (Input.linkdict[i].StartsWith(v))
                //    {
                //        count++;
                //        valuestr += count + "z|" + Input.linkdict[i] + "|\n";
                //    }
                //}


                if (valuestr.Length > 0)
                {
                    valuestr = valuestr.Replace("^", "＾");
                    if (Input.IsChinese == 1 && !Input.IsJT)
                    {
                        //转繁体
                        try
                        {
                            valuestr = Microsoft.VisualBasic.Strings.StrConv(valuestr, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
                        }
                        catch { }
                    }
                    valuearry = valuestr.Split(new string[1] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < valuearry.Length; i++)
                        valuearry[i] = Comm.Function.ReplaceSystem(valuearry[i]);
                    int pagesi = PageSize;
                    cachearry = new string[pagesi];
                    int postcount = 0;
                    for (int i = 0; i < pagesi && i < valuearry.Length; i++)
                    {

                        cachearry[postcount++] = valuearry[i];

                    }
                    if (postcount == 0)
                    {
                        Dream = false;
                        LastLinkString = string.Empty;
                        this.ShowWindow(false);
                    }
                    else
                    {
                        Dream = true;
                        this.ShowWindow(true);
                    }
                }
                else
                {
                    Dream = false;
                    LastLinkString = string.Empty;
                    this.ShowWindow(false);
                }
            }
            else
            {
                valuearry = Input.GetEnInputValue(LastLinkString);
                if (valuearry != null)
                {
                    int pagesi = PageSize;
                    cachearry = new string[pagesi];
                    int postcount = 0;
                    for (int i = 0; i < pagesi && i < valuearry.Length; i++)
                    {
                        if (string.Compare(LastLinkString , valuearry[i].Split('|')[1],true)!=0)
                            cachearry[postcount++] = valuearry[i];
                        else
                            pagesi++;
                    }
                    if (postcount == 0)
                    {
                        Dream = false;
                        LastLinkString = string.Empty;
                        this.ShowWindow(false);
                    }
                    else
                    {
                        Dream = true;
                        this.ShowWindow(true);
                    }
                }
                else
                {
                    Dream = false;
                    LastLinkString = string.Empty;
                    this.ShowWindow(false);
                }
            }
        }
 
        public void NextPage()
        {
            PageNum++;
            if ((PageNum - 1) * PageSize >= valuearry.Length) { PageNum--; return; }
     
            valuearry = Input.GetInputValue(this.inputstr);
            if (valuearry != null)
            {
                int pos = 0;
                cachearry = new string[PageSize];
                
                for (int i = (PageNum - 1) * PageSize; i < valuearry.Length; i++)
                {
                    if (pos >= cachearry.Length) break;
                    cachearry[pos] = valuearry[i];
                    pos++;
                }
                HideByApi();
            }

        }
        public void PrePage()
        {
            PageNum--;
            if (PageNum == 0) { PageNum = 1; return; }
            valuearry = Input.GetInputValue(this.inputstr);
            if (valuearry != null)
            {
                int pos = 0;
                cachearry = new string[PageSize];
                for (int i = (PageNum - 1) * PageSize; i < valuearry.Length; i++)
                {
                    if (pos >= cachearry.Length) break;
                    cachearry[pos] = valuearry[i];
                    pos++;
                }
                HideByApi();
            }

        }



        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // InputStatusFrm
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(175, 133);
            this.Name = "InputStatusFrm";
            this.Load += new System.EventHandler(this.InputStatusFrm_Load);
            this.ResumeLayout(false);

        }

        private void InputStatusFrm_Load(object sender, EventArgs e)
        {
            this.Width = 175;
            this.Height = 133;
        }

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public static string GetCutStr(string v)
        {
            if (string.IsNullOrEmpty(v)) return v;

            if (Input.IsChinese == 1)
                if (v.Length > 8)
                {
                    string vv = v;
                    v = vv.Substring(0, 4) + ".." + vv.Substring(vv.Length - 4, 4);
                }
                else
                    if (v.Length > 14)
                    {
                        string vv = v;
                        v = vv.Substring(0, 9) + ".." + vv.Substring(vv.Length - 4, 4);
                    }

            return v;
        }

        /// <summary>
        /// 绘制候选框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Input.Metor) return;
           
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            BufferedGraphics grafx = context.Allocate(e.Graphics, e.ClipRectangle);
      
            try
            {
                if (this.inputstr.Length==0 && !Dream) return;
                if (ViewType == 0)
                    grafx.Graphics.DrawImage(Win.WinInput.HBackImg, new Rectangle(0, 0, Width, Height));
                else
                    grafx.Graphics.DrawImage(Win.WinInput.BackImg, new Rectangle(0, 0, Width, Height));
                Pen bordpen = new Pen(InputMode.Skinbordpen);
                SolidBrush bstring = new SolidBrush(InputMode.Skinbstring);
                SolidBrush bcstring = new SolidBrush(InputMode.Skinbcstring);
                SolidBrush fbcstring = new SolidBrush(InputMode.Skinfbcstring);
                Rectangle hzrec = new Rectangle(0, 0, Width - 1, Height - 1);
                grafx.Graphics.DrawRectangle(bordpen, hzrec);
                int inputy = InputMode.SkinFontJG;
                string ins = InputStatusFrm.Dream ? "智能联想" : this.inputstr;
                int fontsize = InputMode.SkinFontSize;
 
                grafx.Graphics.DrawString(ins, new Font(InputMode.SkinFontName, fontsize, FontStyle.Bold), bstring, new Point(0 + 3, 0 + 4));
                if (valuearry != null && valuearry.Length > 0 && !InputStatusFrm.Dream) //分页数显示
                    grafx.Graphics.DrawString(string.Format("{0}/{1}", PageNum, (valuearry.Length % PageSize == 0 ? valuearry.Length / PageSize : valuearry.Length / PageSize + 1)), new Font("", 10F), bstring, new Point(Width - 44, 0 + 4));
                if (ViewType == 0)
                {
                    //横排显示
                    if (cachearry == null) return;
                    int wx = 1;
                    for (int i = 0; i < cachearry.Length; i++)
                    {
                        if (InputMode.lbinputc[i] == null) break;
                        if (string.IsNullOrEmpty(cachearry[i])) break; ;
                        string v = GetCutStr(cachearry[i].Split('|')[1]);

                        string pos = i == 9 ? "0." : (i + 1).ToString() + ".";
                        

                        Font tfont = new Font(InputMode.SkinFontName, fontsize);
                        if (i == 0)
                            grafx.Graphics.DrawString(pos + v, tfont, fbcstring, new Point(wx, inputy));
                        else
                            grafx.Graphics.DrawString(pos + v, tfont, bstring, new Point(wx, inputy));
                        if (InputMode.lbinputv == null || InputMode.lbinputv[i] == null) return;
                        InputMode.lbinputv[i].Text = pos + v;
                        //if (InputMode.lbinputv[i].Text.Length > 3)
                        //    wx += InputMode.lbinputv[i].PreferredWidth - 10;
                        //else
                        //    wx += InputMode.lbinputv[i].PreferredWidth - 10;

                        wx += InputMode.lbinputv[i].PreferredWidth - 10;
                        grafx.Graphics.DrawString(cachearry[i].Split('|')[2], new Font("宋体", fontsize - 1), bcstring, new Point(wx, inputy));
                        if (string.IsNullOrEmpty(InputMode.lbinputc[i].Text))
                        {
                            wx += 4;
                        }
                        else
                        {
                            wx += InputMode.lbinputc[i].PreferredWidth;
                            if (InputMode.lbinputv[i].Text.Length > 3)
                                wx += -4;
                        }
                        
                    }
              
                }
                else
                {
                    //竖排显示
                    if (cachearry == null) return;
                    for (int i = 0; i < cachearry.Length; i++)
                    {
                        if (string.IsNullOrEmpty(cachearry[i])) break; ;
                        string v = GetCutStr(cachearry[i].Split('|')[1]);
                        string pos = i == 9 ? "0." : (i + 1).ToString() + ".";
                        Font tfont = new Font(InputMode.SkinFontName, InputMode.SkinFontSize);
                        int vw = InputMode.GetWidth(cachearry[i].Split('|')[1]);
                        if (i == 0)
                            grafx.Graphics.DrawString(pos + v, tfont, fbcstring, new Point(3, inputy + i * InputMode.SkinFontH));
                        else
                            grafx.Graphics.DrawString(pos + v, tfont, bstring, new Point(3, inputy + i * InputMode.SkinFontH));

                        grafx.Graphics.DrawString(cachearry[i].Split('|')[2], new Font("宋体", InputMode.SkinFontSize - 1), bcstring, new Point(3 + vw, (inputy + i * InputMode.SkinFontH) - 1));
                    }
                }

                grafx.Render(e.Graphics);
            }
            catch { }
            finally
            {
                grafx.Graphics.Dispose();
                grafx.Dispose();
            }
        }
    }

    public enum InputType
    {
        INPUT_MOUSE = 0,
        INPUT_KEYBOARD = 1,
        INPUT_HARDWARE = 2,
    }

    [Flags()]
    public enum KEYEVENTF
    {
        EXTENDEDKEY = 0x0001,
        KEYUP = 0x0002,
        UNICODE = 0x0004,
        SCANCODE = 0x0008,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public Int16 wVk;
        public ushort wScan;
        public Int32 dwFlags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public Int32 dx;
        public Int32 dy;
        public Int32 mouseData;
        public Int32 dwFlags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT
    {
        [FieldOffset(0)]
        public Int32 type;//0-MOUSEINPUT;1-KEYBDINPUT;2-HARDWAREINPUT   
        [FieldOffset(4)]
        public KEYBDINPUT ki;
        [FieldOffset(4)]
        public MOUSEINPUT mi;
        [FieldOffset(4)]
        public HARDWAREINPUT hi;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public Int32 uMsg;
        public Int16 wParamL;
        public Int16 wParamH;
    }
}

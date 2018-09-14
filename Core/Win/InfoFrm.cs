using qzxxiEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Core.Win
{
    public partial class InfoFrm : Form
    {
        public InfoFrm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void InfoFrm_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = this.BackColor;
            //this.BackgroundImage = Image.FromFile(System.IO.Path.Combine(Win.WinInput.Input.AppPath, "skin", "msgback.png"));
        }

        public int UpTop = 400;
 
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hWnd, short cmdShow);
          [DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        
        public void SendMsg(InfoEntity info)
        {
            this.webBrowser1.ScriptErrorsSuppressed = true;
            if (string.Equals(info.Method, Method.view.ToString(),StringComparison.CurrentCultureIgnoreCase))
            {
                this.webBrowser1.DocumentText = info.InfoStr;
            }
            else if (string.Equals(info.Method, Method.url.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                if (info.InfoStr.StartsWith("http"))
                {
                    this.webBrowser1.Url = new Uri(info.InfoStr);
                    this.webBrowser1.Refresh();
                }
            }
            
            
            if (!this.Visible)
            {
                UpTop = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 5;

                ShowWindow(this.Handle, 5);
                SetWindowPos(this.Handle
                    , -1, Screen.PrimaryScreen.WorkingArea.Width - this.Width - 20
                    , Screen.PrimaryScreen.WorkingArea.Height + 10
                    , this.Width, this.Height, 0x10);
 
                System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
                {
                    System.Threading.Thread.Sleep(100);
                    while (true)
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y - 20);//每0.1秒上升40象素
                        if (this.Top <= UpTop)
                            return;
                        System.Threading.Thread.Sleep(20);
                    }
                });
                task.Start();
            }
        }

 
    }

     public enum AnimateWindowFlags : uint //AnimateWindow的dwFlags参数定义
         {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }

}

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
    public class ImageInput : System.Windows.Forms.Form
    {
        public static string imgstr = string.Empty;//图片文字

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hWnd, short cmdShow);
        [DllImport("user32.dll")]
        private static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        private const int SW_SHOWNOACTIVATE = 4;
        const int SW_HIDE = 0;
        private const int SWP_NoActiveWINDOW = 0x10;
        private const int HWND_TOPMOST = -1;
        public void ShowWindow()
        {


            if (!this.Visible)
            {
                ShowWindow(this.Handle, SW_SHOWNOACTIVATE);
                SetWindowPos(this.Handle, HWND_TOPMOST, this.Left, this.Top, this.Width, this.Height, SWP_NoActiveWINDOW);
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            BufferedGraphics grafx = context.Allocate(e.Graphics, e.ClipRectangle);

            try
            {

                //grafx.Graphics.DrawImage(Win.WinInput.HBackImg, new Rectangle(0, 0, Width, Height));

                if (ImageInput.imgstr.Length == 0) { this.Width = 0; this.Height = 0; }
                else
                {
                    this.Width = MaxWidth();
                    this.Height = 50;
                    Pen bordpen = new Pen(InputMode.Skinbordpen);
                    SolidBrush bstring = new SolidBrush(InputMode.Skinbstring);
                    SolidBrush bcstring = new SolidBrush(InputMode.Skinbcstring);
                    SolidBrush fbcstring = new SolidBrush(InputMode.Skinfbcstring);
                    Rectangle hzrec = new Rectangle(0, 0, Width - 1, Height - 1);
                    grafx.Graphics.DrawRectangle(bordpen, hzrec);


                    grafx.Graphics.DrawString(ImageInput.imgstr,
                        new Font(InputMode.SkinFontName, 16, FontStyle.Bold), bstring, new Point(0 + 3, 0 + 4));


                    grafx.Render(e.Graphics);
                }
            }
            catch { }
            finally
            {
                grafx.Graphics.Dispose();
                grafx.Dispose();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ImageInput
            // 
            this.ClientSize = new System.Drawing.Size(175, 133);
            this.Name = "ImageInput";
            this.Load += new System.EventHandler(this.ImageInput_Load);
            this.ResumeLayout(false);

        }
        public ImageInput()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.Text = string.Empty;
            this.TopLevel = true;
            this.TopMost = true;
            this.Width = 170;
            this.Height = 50;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ControlBox = true;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            UpdateStyles();
        }
  
        private void ImageInput_Load(object sender, EventArgs e)
        {
            this.Width = 170;
            this.Height = 50;
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

        public static int MaxWidth()
        {
            int vw = 0;
            int rcount = 0;
            for (int i = 0; i < ImageInput.imgstr.Length; i++)
            {
                vw += 26;
            }
            if (vw < 240) vw = 240;
            else if (rcount > 6 && vw < 300) vw = 300;
            return vw;
        }
    }
}

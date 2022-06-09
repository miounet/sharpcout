using Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.Win
{
    public partial class Login : Form
    {
        bool autoclose = false;
        public bool Wait = false;
        public Login()
        {
            autoclose = true;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        InputMode Input = null;
        public Login(bool close, InputMode input)
        {
            this.Input = input;
            this.autoclose = close;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "login.png"));
        }

        public Login(bool close, InputMode input,int flag)
        {
            this.Input = input;
            this.autoclose = close;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "loginup.png"));
            this.Click -= new System.EventHandler(this.Login_Click);
        }
        private void Login_Load(object sender, EventArgs e)
        {
            this.lbID.Text = Program.ProductVer + " QQ群:374971723"; 
            if (autoclose)
            {
                timer1.Enabled = true;
            }
        
            
        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (!autoclose)
                this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //调用系统默认的浏览器
            System.Diagnostics.Process.Start("iexplore.exe", this.linkLabel1.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (autoclose)
            {
                while (Wait) { System.Threading.Thread.Sleep(10); }
                timer1.Enabled = false;
                this.Close();
            }
        }
    }
}

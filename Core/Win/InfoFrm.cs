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
        public InfoFrm(string info)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.textBox1.Text = info;
        }

    

 
    }

 

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Win
{
    public partial class QueryFrm : Form
    {
        public QueryFrm()
        {
            InitializeComponent();
        }
        public QueryFrm(string str)
        {
            InitializeComponent();
            if (str.Length > 0)
            {
                textBox2.Text = str;
                textBox2_TextChanged(null, null);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim().Length > 0)
            {
                string vstr = Core.Comm.Function.QueryCodeByValue(textBox2.Text.Trim());
                if (vstr.Length > 0)
                {
                    textBox1.Text = vstr;
                }
            }
        }
    }
}

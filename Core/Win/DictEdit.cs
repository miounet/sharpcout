using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Base;
using Core.Config;

namespace Core.Win
{
    public partial class DictEdit : Form
    {
        public  static bool orderby = true;
        public DictEdit()
        {

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.button4.Enabled = false;
        }

        private void bntfind_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim().Length == 0) return;
            this.textBox2.Text = "";
            int first = 0, last = Core.Win.WinInput.Input.MasterDit.Length - 1;

            string[] mdict = null;
            string inputstr = this.textBox1.Text.Trim();
            PosIndex poi = Core.Win.WinInput.Input.DictIndex.GetPos(inputstr, ref mdict, false);
            if (poi == null) return;
            first = poi.Start;
            last = poi.End;

            if (mdict == null) mdict = Core.Win.WinInput.Input.MasterDit;

            for (int i = first; i <= last; i++)
            {

                if (mdict[i].Split(' ')[0] == inputstr)
                {

                    string strarr = mdict[i];
                    string fcode = strarr.Split(' ')[0];
                    string fvalue = strarr.Substring(strarr.Split(' ')[0].Length).Trim();//获取汉字

                    this.textBox2.Text = fvalue.Trim().Replace(" ", "\r\n");
                    this.button4.Enabled = true;
                    this.button4.Tag = i;
                    break;
                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim().Length == 0) return;
            try
            {
                int index = (int)this.button4.Tag;
                Core.Win.WinInput.Input.MasterDit[index] = this.textBox1.Text.Trim() + " " + this.textBox2.Text.Trim().Replace("\r\n", " ").Replace("  ", " ").Trim();

                int first = 0, last = Core.Win.WinInput.Input.MasterDit.Length - 1;
                string[] mdict = null;
                string inputstr = this.textBox1.Text.Trim();
                PosIndex poi = Core.Win.WinInput.Input.DictIndex.GetPos(inputstr, ref mdict, true);
                if (poi == null) return;
                first = poi.Start;
                last = poi.End;
                if (mdict == null) return;

                for (int i = first; i <= last; i++)
                {

                    if (mdict[i].Split(' ')[0] == inputstr)
                    {
                        mdict[i] = Core.Win.WinInput.Input.MasterDit[index];


                        if (DictEdit.orderby && File.Exists(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "Setting.shp")))
                        {
                            var setting = File.ReadAllLines(System.IO.Path.Combine(InputMode.AppPath, "dict", InputMode.CDPath, "Setting.shp"), Encoding.UTF8);//读配置

                            DictEdit.orderby = string.IsNullOrEmpty(SetInfo.GetValue("orderby", setting)) ? true : bool.Parse(SetInfo.GetValue("orderby", setting));

                        }
                        if (DictEdit.orderby)
                        {
                            //保存词库
                            List<string> dlist = new List<string>();
                            var iteml = Core.Win.WinInput.Input.MasterDit.ToList();

                            foreach (var ind in Core.Win.WinInput.Input.DictIndex.IndexList)
                            {
                                var item = iteml.FindAll(f => f.Split(' ')[0] == ind.Letter);
                                if (item != null) dlist.AddRange(item);
                                if (ind.mdict != null && ind.mdict.Length > 0)
                                    dlist.AddRange(ind.mdict.ToList());
                            }

                            File.WriteAllLines(Core.Win.WinInput.Input.MasterDitPath, dlist.ToArray(), Encoding.UTF8);
                            DictEdit.orderby = false;
                        }
                        else
                        {
                            File.WriteAllLines(Core.Win.WinInput.Input.MasterDitPath, Core.Win.WinInput.Input.MasterDit, Encoding.UTF8);
                        }
                        MessageBox.Show("保存成功");
                        return;
                    }

                }
            }
            catch { MessageBox.Show("操作失败"); }
        }
    }
}

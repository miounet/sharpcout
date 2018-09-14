using Core.Base;
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
    public partial class UserDict : Form
    {
        InputMode input = null;
        WinInput winput = null;
        DataTable dt = new DataTable();
        public UserDict(InputMode ninput, WinInput nwinput)
        {
            this.input = ninput;
            this.winput = nwinput;
            InitializeComponent();
        }
       
        private void UserDict_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("词库");
            Query("", "");
        }

        private void Query(string c, string v)
        {
            int count = 0;
            dt.Clear();
            foreach (var s in input.UserDit)
            {
                bool view = false;
                if (c.Length > 0 && v.Length > 0)
                {
                    if (s.IndexOf(c) >= 0 && s.IndexOf(v) > 0)
                        view = true;
                }
                else if (c.Length > 0 && v.Length == 0)
                {
                    if (s.IndexOf(c) == 0)
                        view = true;
                }
                else if (c.Length == 0 && v.Length > 0)
                {
                    if (s.IndexOf(v) > 0)
                        view = true;
                }
                else if (c.Length == 0 && v.Length == 0)
                    view = true;
                if (view)
                {
                    if (count > 50) break;
                    var rw = dt.NewRow();
                    rw[0] = s;
                    dt.Rows.Add(rw);
                    count++;
                }
            }
            this.dataGridView1.DataSource = dt;
        }

        private void AddDict(string c, string v)
        {
            var tu = input.UserDit.ToList();
            if (tu.Find(f => f == c + " " + v) == null)
            {
                tu.Add(c + " " + v);
                input.UserDit = tu.ToArray();

                winput.SaveUserDict();
                btnQuery_Click(null, null);
            }
            else
                MessageBox.Show("已存在该词库!");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAppOK)
            {
                MessageBox.Show("加密狗不对,请插入正确的加密狗!", "巧指速录");
                return;
            }
            if (this.txtCode.Text.Trim().Length > 0 && this.txtValue.Text.Trim().Length > 0)
            {
                AddDict(this.txtCode.Text.Trim(), this.txtValue.Text.Trim());
            }
            else
                MessageBox.Show("编码或字词不能为空!");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query(this.txtCode.Text.Trim(), this.txtValue.Text.Trim());
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAppOK)
            {
                MessageBox.Show("加密狗不对,请插入正确的加密狗!", "巧指速录");
                return;
            }
            if (this.dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("未选中要删除的词条!");
            }
            else
            {
                var tu = input.UserDit.ToList();
                bool haveupdate = false;
                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    if (this.dataGridView1.SelectedRows[i].Cells[0].Value == null) continue;
                    string selects = this.dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                    if (!string.IsNullOrEmpty(selects))
                    {
                        //delete one
                         
                          if (tu.Find(f => f == selects) != null)
                          {
                              tu.Remove(selects);
                              haveupdate = true;
                          }
                    }
                }
                if (haveupdate)
                {
                    input.UserDit = tu.ToArray();
                    winput.SaveUserDict();
                }
                btnQuery_Click(null, null);
            }
        }
    }
}

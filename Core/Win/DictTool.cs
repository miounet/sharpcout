using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.Win
{
    public partial class DictTool : Form
    {
        public DictTool()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim().Length == 0) return;
            if (this.textBox3.Text.Trim().Length == 0) return;
            this.textBox2.Text = "";
            CheckForIllegalCrossThreadCalls = false;

            Task ts = new Task(() =>
              {
                  var dict = (string[])Core.Win.WinInput.Input.MasterDit.Clone();

                  var tdict = dict.Select(s => s.Split(' ')[0]).ToList().Where(w => w.Length > 3).ToList();
                  List<string> tdl = this.textBox1.Lines.ToList();
                  int tcount = this.textBox3.Lines.Length;
                  int ccount = 0;
                  foreach (var item in this.textBox3.Lines)
                  {
                      string item1 = item.Replace(" ", "").Trim();
                      if (item1.Length < 2)
                      {
                          ccount++; 
                          label4.Text = tcount + "/" + ccount;
                          continue;
                      }

                      if (item1.Length == 2)
                      {
                          try
                          {
                              var a = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(0, 1));
                              if (a.Count > 0)
                              {
                                  foreach (var aa in a)
                                  {
                                      var b = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(1, 1));
                                      foreach (var bb in b)
                                      {
                                          string bm = aa.Replace(" ", "").Substring(1, 2) + bb.Replace(" ", "").Substring(1, 2);
                                          this.textBox2.AppendText(bm + " " + item1.Trim() + " " + tdict.Count(c => c == bm) + "\n");
                                      }


                                  }
                              }
                          }
                          catch { }
                      }
                      else if (item1.Length == 3)
                      {
                          try
                          {
                              var a = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(0, 1));
                              if (a.Count > 0)
                              {
                                  foreach (var aa in a)
                                  {
                                      var b = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(1, 1));
                                      foreach (var bb in b)
                                      {
                                          var c = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(2, 1));
                                          foreach (var cc in c)
                                          {
                                              string bm = aa.Replace(" ", "").Substring(1, 2) + bb.Replace(" ", "").Substring(1, 1) + cc.Replace(" ", "").Substring(1, 1);
                                              this.textBox2.AppendText(bm + " " + item1.Trim() + " " + tdict.Count(o => o == bm) + "\n");
                                          }
                                      }


                                  }
                              }
                          }
                          catch { }
                      }
                      else if (item1.Length == 4)
                      {
                          try
                          {
                              var a = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(0, 1));
                              if (a.Count > 0)
                              {
                                  foreach (var aa in a)
                                  {
                                      var b = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(1, 1));
                                      foreach (var bb in b)
                                      {
                                          var c = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(2, 1));
                                          foreach (var cc in c)
                                          {
                                              var d = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(3, 1));
                                              foreach (var dd in d)
                                              {
                                                  string bm = aa.Replace(" ", "").Substring(1, 1) + bb.Replace(" ", "").Substring(1, 1) + cc.Replace(" ", "").Substring(1, 1) + dd.Replace(" ", "").Substring(1, 1);
                                                  this.textBox2.AppendText(bm + " " + item1.Trim() + " " + tdict.Count(o => o == bm) + "\n");
                                              }
                                          }
                                      }


                                  }
                              }
                          }
                          catch { }
                      }
                      else
                      {
                          try
                          {
                              var a = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(0, 1));
                              if (a.Count > 0)
                              {
                                  foreach (var aa in a)
                                  {
                                      var b = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(1, 1));
                                      foreach (var bb in b)
                                      {
                                          var c = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(2, 1));
                                          foreach (var cc in c)
                                          {
                                              var d = tdl.FindAll(f => f.Substring(0, 1) == item1.Substring(item1.Length - 1, 1));
                                              foreach (var dd in d)
                                              {
                                                  string bm = aa.Replace(" ", "").Substring(1, 1) + bb.Replace(" ", "").Substring(1, 1) + cc.Replace(" ", "").Substring(1, 1) + dd.Replace(" ", "").Substring(1, 1);
                                                  this.textBox2.AppendText(bm + " " + item1.Trim() + " " + tdict.Count(o => o == bm) + "\n");
                                              }
                                          }
                                      }


                                  }
                              }
                          }
                          catch { }
                      }

                      ccount++;

                      label4.Text = tcount + "/" + ccount;
                  }
              });

            ts.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim().Length == 0) return;
           
            CheckForIllegalCrossThreadCalls = false;
            string[] dict = (string[])this.textBox2.Lines.Clone();
           
            var dicta = Core.Win.WinInput.Input.MasterDit.ToList();
            this.textBox2.Text = "";
            int ccount = 0;
            int tcount = dict.Length;
            Task ts = new Task(() =>
            {
                foreach (var item in dict)
                {
                    try
                    {
                        string item1 = item.Split('\t')[1];
                        if (item1.Length < 2) continue;
                        if (dicta.Find(f => f.IndexOf(" " + item1) > 0) == null)
                        {
                            this.textBox2.AppendText(item + "\n");
                        }
                    }
                    catch { }
                    ccount++;
                    label4.Text = tcount + "/" + ccount;
                }
            });
            ts.Start();
        }
    }
}

using Core.Base;
using Core.Comm;
using qzxxiEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Core.Win
{
    public partial class ProDictFrm : Form
    {
        InputMode input = null;
        WinInput winput = null;

        public ProDictFrm(InputMode ninput, WinInput nwinput)
        {
            this.input = ninput;
            this.winput = nwinput;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public ProDictFrm()
        {
            InitializeComponent();
        }

        private void ProDictFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.chlist.Items.Clear();
                this.lisload.Items.Clear();
                string cof = File.ReadAllText(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, "load.config"), Encoding.UTF8);
                List<UpdateDictEnt> pl = new List<UpdateDictEnt>();
                if (!string.IsNullOrEmpty(cof))
                {
                    pl = ApiClient.JsonToObj<List<UpdateDictEnt>>(cof);
                }
                foreach (var dd in pl)
                {

                    if (!File.Exists(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, dd.Dictid)))
                    {
                        ListViewItem item = new ListViewItem();
                        item.Checked = false;
                        item.Text = dd.DictName + "_";
                        item.Tag = dd.Dictid;
                        this.lisload.Items.Add(item);
                    }
                    else
                    {
                        ListViewItem item = new ListViewItem();
                        item.Checked = dd.Select;
                        item.Text = dd.DictName;
                        item.Tag = dd.Dictid;
                        this.chlist.Items.Add(item);
                        if (dd.UpdateFalg == 1)
                        {
                            ListViewItem item1 = new ListViewItem();
                            item1.Checked = false;
                            item1.Text = dd.DictName;
                            item1.Tag = dd.Dictid;
                            this.lisload.Items.Add(item1);
                        }
                    }

                }


                if (sender != null)
                {
                    this.chlist.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.chlist_ItemChecked);
                    GetProDict();
                }
            }
            catch { }
        }

        private void GetProDict()
        {
            System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
            {
                
                try
                {
                    string cof = File.ReadAllText(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, "load.config"), Encoding.UTF8);
                    List<UpdateDictEnt> pl = new List<UpdateDictEnt>();
                    if (!string.IsNullOrEmpty(cof))
                    {
                        pl = ApiClient.JsonToObj<List<UpdateDictEnt>>(cof);
                    }
                    var data = ApiClient.GetProDict(Win.WinInput.ApiUrl, this.txtCode.Text);
                    if (data.DictList != null && data.DictList.Count > 0)
                    {
                        foreach (var dd in data.DictList)
                        {
                            if (pl.Find(p => p.Dictid == dd.Dictid) == null)
                            {
                                dd.UpdateFalg = 1;
                                pl.Add(dd);
                            }
                            else
                            {
                                pl.Find(p => p.Dictid == dd.Dictid).DictName = dd.DictName;
                                if (
                                    pl.Find(p => p.Dictid == dd.Dictid).DictVersion != dd.DictVersion
                                    || !File.Exists(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, dd.Dictid))
                                    )
                                    pl.Find(p => p.Dictid == dd.Dictid).UpdateFalg = 1;
                                //pl.Find(p => p.Dictid == dd.Dictid).DictVersion = dd.DictVersion;
                                pl.Find(p => p.Dictid == dd.Dictid).UpdateUrl = dd.UpdateUrl;
                            }
                        }
                    }

                    File.WriteAllText(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, "load.config"), ApiClient.ToJson(pl), Encoding.UTF8);
                    this.chlist.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.chlist_ItemChecked);
                    try
                    {
                        ProDictFrm_Load(null, null);
                    }
                    catch { }
                    this.chlist.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.chlist_ItemChecked);
                    return;
                }
                catch { }
                
            });
            
            task.Start();
        }

        private void chlist_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                if (e.Item.Text.EndsWith("_"))
                {
                    e.Item.Checked = false;
                    MessageBox.Show("词库未下载,请先下载!");
                }
            }
           
        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            if (!Program.CheckAppOK)
            {
                MessageBox.Show("加密狗不对,请插入正确的加密狗!", "巧指速录");
                return;
            }

            string cof = File.ReadAllText(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, "load.config"), Encoding.UTF8);
            List<UpdateDictEnt> pl = new List<UpdateDictEnt>();
            if (!string.IsNullOrEmpty(cof))
            {
                pl = ApiClient.JsonToObj<List<UpdateDictEnt>>(cof);
            }
            int scount = 0;
            for (int i = 0; i < this.chlist.Items.Count; i++)
            {
                if (this.chlist.Items[i].Checked) scount++;
            }
            if (scount > 6)
            {
                MessageBox.Show("同时选择的专业词库不能超过6个!");
                return;
            }

            for (int i = 0; i < this.chlist.Items.Count; i++)
            {
                var sp = pl.Find(p => p.Dictid == this.chlist.Items[i].Tag.ToString());
                if (sp != null)
                    sp.Select = this.chlist.Items[i].Checked;
            }
            File.WriteAllText(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, "load.config"), ApiClient.ToJson(pl), Encoding.UTF8);
            this.btnSearch.Enabled = false;
            this.bntSave.Enabled = false;
            input.indexComplete = false;//初始化完成的状态
            try
            {
                winput.LoadProDict();
            }
            catch { }
            input.indexComplete = true;//初始化完成的状态
            this.btnSearch.Enabled = true;
            this.bntSave.Enabled = true;
            MessageBox.Show("已加载专业词库!");
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetProDict();
        }

        private void bntdonwload_Click(object sender, EventArgs e)
        {
            string cof = File.ReadAllText(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, "load.config"), Encoding.UTF8);
            List<UpdateDictEnt> pl = new List<UpdateDictEnt>();
            if (!string.IsNullOrEmpty(cof))
            {
                pl = ApiClient.JsonToObj<List<UpdateDictEnt>>(cof);
            }
            List<UpdateDictEnt> dpl = new List<UpdateDictEnt>();
            for (int i = 0; i < this.lisload.Items.Count; i++)
            {
                if (this.lisload.Items[i].Checked)
                {
                    var down = pl.Find(f => f.Dictid == this.lisload.Items[i].Tag.ToString());
                    if (down != null) dpl.Add(down);
                }
            }

            if(dpl.Count==0)
            {
                MessageBox.Show("请选择要下载或更新的词库!");
                return;
            }
            if (dpl.Count >3 )
            {
                MessageBox.Show("同时下载的词库不能超过3个!");
                return;
            }
            System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
            {
                this.bntdonwload.Enabled = false;
                this.lbdinfo.Visible = true;
                try
                {
                    foreach (var d in dpl)
                    {
                        try
                        {
                            var data = ApiClient.GetProDict(Win.WinInput.ApiUrl, d.Dictid);//获取下载地址
                            if (data.DictList.Count == 0) continue;
                            WebClient client = new WebClient();
                            string filename = System.IO.Path.Combine(Win.WinInput.Input.AppPath, DateTime.Now.Ticks.ToString() + ".zip");
                            client.DownloadFile(data.DictList[0].UpdateUrl[0], filename);
                            Thread.Sleep(100);
                            if (File.Exists(filename))
                            {
                                //下载完成解压
                                ZipClass.UnZip(filename, System.IO.Path.Combine(Win.WinInput.Input.AppPath, "prodict"), string.Empty);
                                d.UpdateFalg = 0;
                                d.DictVersion = data.DictList[0].DictVersion;
                                Thread.Sleep(200);
                                File.Delete(filename);//解压完成，删除下载的文件
                            }
                        }
                        catch (Exception ex)
                        {
                            string ss = ex.Message;
                        }
                    }

                    File.WriteAllText(System.IO.Path.Combine(Win.WinInput.Input.ProDitPath, "load.config"), ApiClient.ToJson(pl), Encoding.UTF8);
                    ProDictFrm_Load(null, null);
                }
                catch { }
                this.lbdinfo.Visible = false;
                this.bntdonwload.Enabled = true;
                return;

            });
            task.Start();
        }
 
    }
}

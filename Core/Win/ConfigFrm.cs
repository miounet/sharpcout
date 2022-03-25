using Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.Win
{
    public partial class ConfigFrm : Form
    {
        Win.WinInput winput = null;
        public ConfigFrm(Win.WinInput input)
        {
            this.winput=input;
            InitializeComponent();
            //this.Icon = new Icon(System.IO.Path.Combine(Application.StartupPath, "log32.ico"));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            winput.LoadSettting();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InputMode.OpenCould = this.ckOpenCould.Checked;
            InputMode.AutoRun = this.chkAutoRun.Checked;
            InputMode.AutoUpdate = this.ckAutoUpdate.Checked;
            InputMode.OpenLink = this.ckLink.Checked;
            InputMode.OpenAltSelect = this.ckalt.Checked;
            InputMode.SingleInput = this.SingleInput.Checked;
            InputMode.right3_out = this.ckright3out.Checked;
            winput.curTrac = this.tracchk.Checked;
            InputMode.PageSize = (int)this.selectnum.Value;
            InputMode.txtla = this.txtla.Text.Trim();
            InputMode.txtra = this.txtra.Text.Trim();
            InputMode.txtlas = this.txtlas.Text.Trim();
            InputMode.txtras = this.txtras.Text.Trim();
            InputMode.txtlra = this.txtlra.Text.Trim();
            InputMode.txtlras = this.txtlras.Text.Trim();
            InputMode.pinyin = this.ckpinyin.Checked;
            winput.SaveSetting();
            Comm.Function.RunWhenStart(InputMode.AutoRun);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ConfigFrm_Load(object sender, EventArgs e)
        {
            this.ckOpenCould.Checked = InputMode.OpenCould;
            this.ckAutoUpdate.Checked = InputMode.AutoUpdate;
            this.chkAutoRun.Checked = InputMode.AutoRun;
            this.ckLink.Checked = InputMode.OpenLink;
            this.numSkinHeight.Value = InputMode.SkinHeith;
            this.selectnum.Value = InputMode.PageSize;
            this.btnSkinbstring.ForeColor = InputMode.Skinbstring;
            this.btnSkinbcstring.ForeColor = InputMode.Skinbcstring;
            this.btnSkinfbcstring.ForeColor = InputMode.Skinfbcstring;
            this.btnSkinFontName.Font = new System.Drawing.Font(InputMode.SkinFontName, InputMode.SkinFontSize);
            this.tracchk.Checked = winput.curTrac;
            this.ckalt.Checked = InputMode.OpenAltSelect;
            this.SingleInput.Checked = InputMode.SingleInput;
            this.txtla.Text = InputMode.txtla;
            this.txtra.Text = InputMode.txtra;
            this.txtlas.Text = InputMode.txtlas;
            this.txtras.Text = InputMode.txtras;
            this.txtlra.Text = InputMode.txtlra;
            this.txtlras.Text = InputMode.txtlras;
            this.ckright3out.Checked = InputMode.right3_out;
            this.ckpinyin.Checked = InputMode.pinyin;
        }
 
        private void numSkinHeight_ValueChanged(object sender, EventArgs e)
        {
            InputMode.SkinHeith = int.Parse(this.numSkinHeight.Value.ToString());
        }

        private void btnSkinbstring_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.btnSkinbstring.ForeColor;
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnSkinbstring.ForeColor = this.colorDialog1.Color;
                InputMode.Skinbstring = this.btnSkinbstring.ForeColor;
            }
        }

        private void btnSkinbcstring_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.btnSkinbcstring.ForeColor;
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnSkinbcstring.ForeColor = this.colorDialog1.Color;
                InputMode.Skinbcstring = this.btnSkinbcstring.ForeColor;
            }
        }

        private void btnSkinfbcstring_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.btnSkinfbcstring.ForeColor;
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnSkinfbcstring.ForeColor = this.colorDialog1.Color;
                InputMode.Skinfbcstring = this.btnSkinfbcstring.ForeColor;
            }
        }

        private void btnSkinFontName_Click(object sender, EventArgs e)
        {
            this.fontDialog1.Font = new Font(InputMode.SkinFontName
                , InputMode.SkinFontSize);
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnSkinFontName.Font = this.fontDialog1.Font;
                InputMode.SkinFontName = this.btnSkinFontName.Font.Name;
                InputMode.SkinFontSize = (int)this.btnSkinFontName.Font.Size;
            }
        }
    }
}

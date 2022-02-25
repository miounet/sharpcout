namespace Core.Win
{
    partial class ConfigFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigFrm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckalt = new System.Windows.Forms.CheckBox();
            this.tracchk = new System.Windows.Forms.CheckBox();
            this.chkAutoRun = new System.Windows.Forms.CheckBox();
            this.ckLink = new System.Windows.Forms.CheckBox();
            this.ckAutoUpdate = new System.Windows.Forms.CheckBox();
            this.ckOpenCould = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSkinFontName = new System.Windows.Forms.Button();
            this.btnSkinfbcstring = new System.Windows.Forms.Button();
            this.btnSkinbcstring = new System.Windows.Forms.Button();
            this.btnSkinbstring = new System.Windows.Forms.Button();
            this.numSkinHeight = new System.Windows.Forms.NumericUpDown();
            this.label54 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.selectnum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.SingleInput = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectnum)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SingleInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.selectnum);
            this.groupBox1.Controls.Add(this.ckalt);
            this.groupBox1.Controls.Add(this.tracchk);
            this.groupBox1.Controls.Add(this.chkAutoRun);
            this.groupBox1.Controls.Add(this.ckLink);
            this.groupBox1.Controls.Add(this.ckAutoUpdate);
            this.groupBox1.Controls.Add(this.ckOpenCould);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本设置";
            // 
            // ckalt
            // 
            this.ckalt.AutoSize = true;
            this.ckalt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckalt.Location = new System.Drawing.Point(24, 78);
            this.ckalt.Name = "ckalt";
            this.ckalt.Size = new System.Drawing.Size(391, 20);
            this.ckalt.TabIndex = 5;
            this.ckalt.Text = "win/alt选重,左win/alt选2重,左win/alt+空格选3重";
            this.ckalt.UseVisualStyleBackColor = true;
            // 
            // tracchk
            // 
            this.tracchk.AutoSize = true;
            this.tracchk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tracchk.Location = new System.Drawing.Point(149, 53);
            this.tracchk.Name = "tracchk";
            this.tracchk.Size = new System.Drawing.Size(87, 20);
            this.tracchk.TabIndex = 4;
            this.tracchk.Text = "光标跟随";
            this.tracchk.UseVisualStyleBackColor = true;
            // 
            // chkAutoRun
            // 
            this.chkAutoRun.AutoSize = true;
            this.chkAutoRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAutoRun.Location = new System.Drawing.Point(24, 53);
            this.chkAutoRun.Name = "chkAutoRun";
            this.chkAutoRun.Size = new System.Drawing.Size(119, 20);
            this.chkAutoRun.TabIndex = 3;
            this.chkAutoRun.Text = "开机自动运行";
            this.chkAutoRun.UseVisualStyleBackColor = true;
            // 
            // ckLink
            // 
            this.ckLink.AutoSize = true;
            this.ckLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckLink.Location = new System.Drawing.Point(151, 25);
            this.ckLink.Name = "ckLink";
            this.ckLink.Size = new System.Drawing.Size(87, 20);
            this.ckLink.TabIndex = 2;
            this.ckLink.Text = "智能联想";
            this.ckLink.UseVisualStyleBackColor = true;
            // 
            // ckAutoUpdate
            // 
            this.ckAutoUpdate.AutoSize = true;
            this.ckAutoUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckAutoUpdate.Location = new System.Drawing.Point(24, 25);
            this.ckAutoUpdate.Name = "ckAutoUpdate";
            this.ckAutoUpdate.Size = new System.Drawing.Size(87, 20);
            this.ckAutoUpdate.TabIndex = 1;
            this.ckAutoUpdate.Text = "自动升级";
            this.ckAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // ckOpenCould
            // 
            this.ckOpenCould.AutoSize = true;
            this.ckOpenCould.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckOpenCould.Location = new System.Drawing.Point(341, 53);
            this.ckOpenCould.Name = "ckOpenCould";
            this.ckOpenCould.Size = new System.Drawing.Size(103, 20);
            this.ckOpenCould.TabIndex = 0;
            this.ckOpenCould.Text = "开启云词库";
            this.ckOpenCould.UseVisualStyleBackColor = true;
            this.ckOpenCould.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Controls.Add(this.btnSkinFontName);
            this.groupBox2.Controls.Add(this.btnSkinfbcstring);
            this.groupBox2.Controls.Add(this.btnSkinbcstring);
            this.groupBox2.Controls.Add(this.btnSkinbstring);
            this.groupBox2.Controls.Add(this.numSkinHeight);
            this.groupBox2.Controls.Add(this.label54);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 86);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字体大小设置";
            // 
            // btnSkinFontName
            // 
            this.btnSkinFontName.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSkinFontName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkinFontName.Location = new System.Drawing.Point(14, 52);
            this.btnSkinFontName.Name = "btnSkinFontName";
            this.btnSkinFontName.Size = new System.Drawing.Size(114, 27);
            this.btnSkinFontName.TabIndex = 26;
            this.btnSkinFontName.Text = "字体及大小";
            this.btnSkinFontName.UseVisualStyleBackColor = false;
            this.btnSkinFontName.Click += new System.EventHandler(this.btnSkinFontName_Click);
            // 
            // btnSkinfbcstring
            // 
            this.btnSkinfbcstring.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSkinfbcstring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkinfbcstring.Location = new System.Drawing.Point(151, 51);
            this.btnSkinfbcstring.Name = "btnSkinfbcstring";
            this.btnSkinfbcstring.Size = new System.Drawing.Size(150, 27);
            this.btnSkinfbcstring.TabIndex = 25;
            this.btnSkinfbcstring.Text = "第一候选框字体颜色";
            this.btnSkinfbcstring.UseVisualStyleBackColor = false;
            this.btnSkinfbcstring.Click += new System.EventHandler(this.btnSkinfbcstring_Click);
            // 
            // btnSkinbcstring
            // 
            this.btnSkinbcstring.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSkinbcstring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkinbcstring.Location = new System.Drawing.Point(231, 20);
            this.btnSkinbcstring.Name = "btnSkinbcstring";
            this.btnSkinbcstring.Size = new System.Drawing.Size(70, 27);
            this.btnSkinbcstring.TabIndex = 24;
            this.btnSkinbcstring.Text = "补码颜色";
            this.btnSkinbcstring.UseVisualStyleBackColor = false;
            this.btnSkinbcstring.Click += new System.EventHandler(this.btnSkinbcstring_Click);
            // 
            // btnSkinbstring
            // 
            this.btnSkinbstring.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSkinbstring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkinbstring.Location = new System.Drawing.Point(151, 20);
            this.btnSkinbstring.Name = "btnSkinbstring";
            this.btnSkinbstring.Size = new System.Drawing.Size(70, 27);
            this.btnSkinbstring.TabIndex = 23;
            this.btnSkinbstring.Text = "字词颜色";
            this.btnSkinbstring.UseVisualStyleBackColor = false;
            this.btnSkinbstring.Click += new System.EventHandler(this.btnSkinbstring_Click);
            // 
            // numSkinHeight
            // 
            this.numSkinHeight.Location = new System.Drawing.Point(73, 24);
            this.numSkinHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numSkinHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSkinHeight.Name = "numSkinHeight";
            this.numSkinHeight.Size = new System.Drawing.Size(55, 26);
            this.numSkinHeight.TabIndex = 19;
            this.numSkinHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSkinHeight.ValueChanged += new System.EventHandler(this.numSkinHeight_ValueChanged);
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(11, 28);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(55, 16);
            this.label54.TabIndex = 20;
            this.label54.Text = "选框高";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(101, 218);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(200, 218);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 29);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // selectnum
            // 
            this.selectnum.Location = new System.Drawing.Point(341, 21);
            this.selectnum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.selectnum.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.selectnum.Name = "selectnum";
            this.selectnum.Size = new System.Drawing.Size(55, 26);
            this.selectnum.TabIndex = 20;
            this.selectnum.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "候选数：";
            // 
            // SingleInput
            // 
            this.SingleInput.AutoSize = true;
            this.SingleInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SingleInput.Location = new System.Drawing.Point(248, 53);
            this.SingleInput.Name = "SingleInput";
            this.SingleInput.Size = new System.Drawing.Size(87, 20);
            this.SingleInput.TabIndex = 22;
            this.SingleInput.Text = "单字模式";
            this.SingleInput.UseVisualStyleBackColor = true;
            // 
            // ConfigFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 251);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性设置";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ConfigFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectnum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox ckOpenCould;
        private System.Windows.Forms.CheckBox ckAutoUpdate;
        private System.Windows.Forms.CheckBox ckLink;
        private System.Windows.Forms.NumericUpDown numSkinHeight;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Button btnSkinFontName;
        private System.Windows.Forms.Button btnSkinfbcstring;
        private System.Windows.Forms.Button btnSkinbcstring;
        private System.Windows.Forms.Button btnSkinbstring;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.CheckBox chkAutoRun;
        private System.Windows.Forms.CheckBox tracchk;
        private System.Windows.Forms.CheckBox ckalt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown selectnum;
        private System.Windows.Forms.CheckBox SingleInput;
    }
}
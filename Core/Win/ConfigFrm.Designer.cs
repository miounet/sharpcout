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
            this.ckright3out = new System.Windows.Forms.CheckBox();
            this.SingleInput = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectnum = new System.Windows.Forms.NumericUpDown();
            this.tracchk = new System.Windows.Forms.CheckBox();
            this.chkAutoRun = new System.Windows.Forms.CheckBox();
            this.ckLink = new System.Windows.Forms.CheckBox();
            this.ckAutoUpdate = new System.Windows.Forms.CheckBox();
            this.ckOpenCould = new System.Windows.Forms.CheckBox();
            this.ckalt = new System.Windows.Forms.CheckBox();
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtla = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtra = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtras = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtlas = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtlras = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtlra = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectnum)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkinHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckright3out);
            this.groupBox1.Controls.Add(this.SingleInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.selectnum);
            this.groupBox1.Controls.Add(this.tracchk);
            this.groupBox1.Controls.Add(this.chkAutoRun);
            this.groupBox1.Controls.Add(this.ckLink);
            this.groupBox1.Controls.Add(this.ckAutoUpdate);
            this.groupBox1.Controls.Add(this.ckOpenCould);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(559, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本设置";
            // 
            // ckright3out
            // 
            this.ckright3out.AutoSize = true;
            this.ckright3out.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckright3out.Location = new System.Drawing.Point(8, 87);
            this.ckright3out.Margin = new System.Windows.Forms.Padding(4);
            this.ckright3out.Name = "ckright3out";
            this.ckright3out.Size = new System.Drawing.Size(159, 20);
            this.ckright3out.TabIndex = 23;
            this.ckright3out.Text = "右手第3码顶字上屏";
            this.ckright3out.UseVisualStyleBackColor = true;
            // 
            // SingleInput
            // 
            this.SingleInput.AutoSize = true;
            this.SingleInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SingleInput.Location = new System.Drawing.Point(238, 58);
            this.SingleInput.Margin = new System.Windows.Forms.Padding(4);
            this.SingleInput.Name = "SingleInput";
            this.SingleInput.Size = new System.Drawing.Size(87, 20);
            this.SingleInput.TabIndex = 22;
            this.SingleInput.Text = "单字模式";
            this.SingleInput.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "候选数：";
            // 
            // selectnum
            // 
            this.selectnum.Location = new System.Drawing.Point(345, 24);
            this.selectnum.Margin = new System.Windows.Forms.Padding(4);
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
            this.selectnum.Size = new System.Drawing.Size(54, 26);
            this.selectnum.TabIndex = 20;
            this.selectnum.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // tracchk
            // 
            this.tracchk.AutoSize = true;
            this.tracchk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tracchk.Location = new System.Drawing.Point(137, 58);
            this.tracchk.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkAutoRun.Location = new System.Drawing.Point(8, 58);
            this.chkAutoRun.Margin = new System.Windows.Forms.Padding(4);
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
            this.ckLink.Location = new System.Drawing.Point(139, 27);
            this.ckLink.Margin = new System.Windows.Forms.Padding(4);
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
            this.ckAutoUpdate.Location = new System.Drawing.Point(8, 27);
            this.ckAutoUpdate.Margin = new System.Windows.Forms.Padding(4);
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
            this.ckOpenCould.Location = new System.Drawing.Point(345, 58);
            this.ckOpenCould.Margin = new System.Windows.Forms.Padding(4);
            this.ckOpenCould.Name = "ckOpenCould";
            this.ckOpenCould.Size = new System.Drawing.Size(103, 20);
            this.ckOpenCould.TabIndex = 0;
            this.ckOpenCould.Text = "开启云词库";
            this.ckOpenCould.UseVisualStyleBackColor = true;
            this.ckOpenCould.Visible = false;
            // 
            // ckalt
            // 
            this.ckalt.AutoSize = true;
            this.ckalt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckalt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckalt.Location = new System.Drawing.Point(14, 230);
            this.ckalt.Margin = new System.Windows.Forms.Padding(4);
            this.ckalt.Name = "ckalt";
            this.ckalt.Size = new System.Drawing.Size(487, 20);
            this.ckalt.TabIndex = 5;
            this.ckalt.Text = "开启拇指并击 用空格左右两边键：win/alt/VolumeDown/VolumeUp";
            this.ckalt.UseVisualStyleBackColor = true;
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
            this.groupBox2.Location = new System.Drawing.Point(0, 121);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(559, 102);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字体大小设置";
            // 
            // btnSkinFontName
            // 
            this.btnSkinFontName.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSkinFontName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkinFontName.Location = new System.Drawing.Point(358, 23);
            this.btnSkinFontName.Margin = new System.Windows.Forms.Padding(4);
            this.btnSkinFontName.Name = "btnSkinFontName";
            this.btnSkinFontName.Size = new System.Drawing.Size(153, 68);
            this.btnSkinFontName.TabIndex = 26;
            this.btnSkinFontName.Text = "字体及大小";
            this.btnSkinFontName.UseVisualStyleBackColor = false;
            this.btnSkinFontName.Click += new System.EventHandler(this.btnSkinFontName_Click);
            // 
            // btnSkinfbcstring
            // 
            this.btnSkinfbcstring.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSkinfbcstring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkinfbcstring.Location = new System.Drawing.Point(176, 59);
            this.btnSkinfbcstring.Margin = new System.Windows.Forms.Padding(4);
            this.btnSkinfbcstring.Name = "btnSkinfbcstring";
            this.btnSkinfbcstring.Size = new System.Drawing.Size(175, 31);
            this.btnSkinfbcstring.TabIndex = 25;
            this.btnSkinfbcstring.Text = "第一候选框字体颜色";
            this.btnSkinfbcstring.UseVisualStyleBackColor = false;
            this.btnSkinfbcstring.Click += new System.EventHandler(this.btnSkinfbcstring_Click);
            // 
            // btnSkinbcstring
            // 
            this.btnSkinbcstring.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSkinbcstring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkinbcstring.Location = new System.Drawing.Point(270, 23);
            this.btnSkinbcstring.Margin = new System.Windows.Forms.Padding(4);
            this.btnSkinbcstring.Name = "btnSkinbcstring";
            this.btnSkinbcstring.Size = new System.Drawing.Size(82, 31);
            this.btnSkinbcstring.TabIndex = 24;
            this.btnSkinbcstring.Text = "补码颜色";
            this.btnSkinbcstring.UseVisualStyleBackColor = false;
            this.btnSkinbcstring.Click += new System.EventHandler(this.btnSkinbcstring_Click);
            // 
            // btnSkinbstring
            // 
            this.btnSkinbstring.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSkinbstring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkinbstring.Location = new System.Drawing.Point(176, 23);
            this.btnSkinbstring.Margin = new System.Windows.Forms.Padding(4);
            this.btnSkinbstring.Name = "btnSkinbstring";
            this.btnSkinbstring.Size = new System.Drawing.Size(82, 31);
            this.btnSkinbstring.TabIndex = 23;
            this.btnSkinbstring.Text = "字词颜色";
            this.btnSkinbstring.UseVisualStyleBackColor = false;
            this.btnSkinbstring.Click += new System.EventHandler(this.btnSkinbstring_Click);
            // 
            // numSkinHeight
            // 
            this.numSkinHeight.Location = new System.Drawing.Point(85, 46);
            this.numSkinHeight.Margin = new System.Windows.Forms.Padding(4);
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
            this.numSkinHeight.Size = new System.Drawing.Size(64, 26);
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
            this.label54.Location = new System.Drawing.Point(13, 50);
            this.label54.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(55, 16);
            this.label54.TabIndex = 20;
            this.label54.Text = "选框高";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(135, 406);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(251, 406);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 34);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(42, 267);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "左按输出：";
            // 
            // txtla
            // 
            this.txtla.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtla.Location = new System.Drawing.Point(131, 261);
            this.txtla.Margin = new System.Windows.Forms.Padding(4);
            this.txtla.MaxLength = 20;
            this.txtla.Name = "txtla";
            this.txtla.Size = new System.Drawing.Size(87, 26);
            this.txtla.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(92, 364);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(271, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = " 数字代表选重，如：第2位选重可填2\r\n 有冲突请改用VolumeDown/VolumeUp";
            // 
            // txtra
            // 
            this.txtra.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtra.Location = new System.Drawing.Point(131, 293);
            this.txtra.Margin = new System.Windows.Forms.Padding(4);
            this.txtra.MaxLength = 20;
            this.txtra.Name = "txtra";
            this.txtra.Size = new System.Drawing.Size(87, 26);
            this.txtra.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(42, 299);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "右按输出：";
            // 
            // txtras
            // 
            this.txtras.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtras.Location = new System.Drawing.Point(320, 292);
            this.txtras.Margin = new System.Windows.Forms.Padding(4);
            this.txtras.MaxLength = 20;
            this.txtras.Name = "txtras";
            this.txtras.Size = new System.Drawing.Size(87, 26);
            this.txtras.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(230, 298);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "+空格输出：";
            // 
            // txtlas
            // 
            this.txtlas.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtlas.Location = new System.Drawing.Point(320, 260);
            this.txtlas.Margin = new System.Windows.Forms.Padding(4);
            this.txtlas.MaxLength = 20;
            this.txtlas.Name = "txtlas";
            this.txtlas.Size = new System.Drawing.Size(87, 26);
            this.txtlas.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(230, 266);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "+空格输出：";
            // 
            // txtlras
            // 
            this.txtlras.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtlras.Location = new System.Drawing.Point(320, 326);
            this.txtlras.Margin = new System.Windows.Forms.Padding(4);
            this.txtlras.MaxLength = 20;
            this.txtlras.Name = "txtlras";
            this.txtlras.Size = new System.Drawing.Size(87, 26);
            this.txtlras.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(230, 332);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 14);
            this.label7.TabIndex = 17;
            this.label7.Text = "+空格输出：";
            // 
            // txtlra
            // 
            this.txtlra.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtlra.Location = new System.Drawing.Point(131, 327);
            this.txtlra.Margin = new System.Windows.Forms.Padding(4);
            this.txtlra.MaxLength = 20;
            this.txtlra.Name = "txtlra";
            this.txtlra.Size = new System.Drawing.Size(87, 26);
            this.txtlra.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(14, 333);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "左右同按输出：";
            // 
            // ConfigFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 452);
            this.Controls.Add(this.txtlras);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtlra);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtras);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtlas);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtla);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ckalt);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性设置";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ConfigFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectnum)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSkinHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtla;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtras;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtlas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtlras;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtlra;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckright3out;
    }
}
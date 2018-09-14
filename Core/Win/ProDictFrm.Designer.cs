namespace Core.Win
{
    partial class ProDictFrm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProDictFrm));
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chlist = new System.Windows.Forms.ListView();
            this.bntSave = new System.Windows.Forms.Button();
            this.lisload = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.bntdonwload = new System.Windows.Forms.Button();
            this.lbdinfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(220, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(59, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "搜 索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtCode
            // 
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.Location = new System.Drawing.Point(59, 7);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(153, 21);
            this.txtCode.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "关键字";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "可选词库：";
            // 
            // chlist
            // 
            this.chlist.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.chlist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chlist.CheckBoxes = true;
            this.chlist.Font = new System.Drawing.Font("宋体", 13F);
            this.chlist.FullRowSelect = true;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            this.chlist.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.chlist.Location = new System.Drawing.Point(8, 52);
            this.chlist.MultiSelect = false;
            this.chlist.Name = "chlist";
            this.chlist.Size = new System.Drawing.Size(385, 120);
            this.chlist.TabIndex = 10;
            this.chlist.UseCompatibleStateImageBehavior = false;
            this.chlist.View = System.Windows.Forms.View.List;
            // 
            // bntSave
            // 
            this.bntSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSave.Location = new System.Drawing.Point(313, 5);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(75, 23);
            this.bntSave.TabIndex = 11;
            this.bntSave.Text = "保存设置";
            this.bntSave.UseVisualStyleBackColor = true;
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // lisload
            // 
            this.lisload.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lisload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lisload.CheckBoxes = true;
            this.lisload.Font = new System.Drawing.Font("宋体", 13F);
            this.lisload.FullRowSelect = true;
            listViewItem2.Checked = true;
            listViewItem2.StateImageIndex = 1;
            this.lisload.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.lisload.Location = new System.Drawing.Point(8, 198);
            this.lisload.MultiSelect = false;
            this.lisload.Name = "lisload";
            this.lisload.Size = new System.Drawing.Size(304, 120);
            this.lisload.TabIndex = 13;
            this.lisload.UseCompatibleStateImageBehavior = false;
            this.lisload.View = System.Windows.Forms.View.List;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "可下载的词库：";
            // 
            // bntdonwload
            // 
            this.bntdonwload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntdonwload.Location = new System.Drawing.Point(318, 276);
            this.bntdonwload.Name = "bntdonwload";
            this.bntdonwload.Size = new System.Drawing.Size(70, 42);
            this.bntdonwload.TabIndex = 14;
            this.bntdonwload.Text = "下载选中词库";
            this.bntdonwload.UseVisualStyleBackColor = true;
            this.bntdonwload.Click += new System.EventHandler(this.bntdonwload_Click);
            // 
            // lbdinfo
            // 
            this.lbdinfo.AutoSize = true;
            this.lbdinfo.Location = new System.Drawing.Point(318, 220);
            this.lbdinfo.Name = "lbdinfo";
            this.lbdinfo.Size = new System.Drawing.Size(71, 12);
            this.lbdinfo.TabIndex = 15;
            this.lbdinfo.Text = "正在下载...";
            this.lbdinfo.Visible = false;
            // 
            // ProDictFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 324);
            this.Controls.Add(this.lbdinfo);
            this.Controls.Add(this.bntdonwload);
            this.Controls.Add(this.lisload);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.chlist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProDictFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "专业词库管理";
            this.Load += new System.EventHandler(this.ProDictFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView chlist;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.ListView lisload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bntdonwload;
        private System.Windows.Forms.Label lbdinfo;
    }
}
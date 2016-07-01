namespace YimoSignMain
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSign = new System.Windows.Forms.Button();
            this.lvMemberList = new System.Windows.Forms.ListView();
            this.clh1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clh2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clh3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clh4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clh5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clh6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkSellectAll = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(25, 12);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(75, 23);
            this.btnSign.TabIndex = 0;
            this.btnSign.Text = "一键签到";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // lvMemberList
            // 
            this.lvMemberList.CheckBoxes = true;
            this.lvMemberList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clh1,
            this.clh2,
            this.clh3,
            this.clh4,
            this.clh5,
            this.clh6});
            this.lvMemberList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMemberList.FullRowSelect = true;
            this.lvMemberList.Location = new System.Drawing.Point(0, 0);
            this.lvMemberList.Name = "lvMemberList";
            this.lvMemberList.Size = new System.Drawing.Size(934, 243);
            this.lvMemberList.TabIndex = 1;
            this.lvMemberList.UseCompatibleStateImageBehavior = false;
            this.lvMemberList.View = System.Windows.Forms.View.Details;
            // 
            // clh1
            // 
            this.clh1.Text = "";
            this.clh1.Width = 76;
            // 
            // clh2
            // 
            this.clh2.Text = "网站名称";
            this.clh2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clh2.Width = 179;
            // 
            // clh3
            // 
            this.clh3.Text = "消息";
            this.clh3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clh3.Width = 133;
            // 
            // clh4
            // 
            this.clh4.Text = "签到时间（毫秒）";
            this.clh4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clh4.Width = 113;
            // 
            // clh5
            // 
            this.clh5.Text = "签到状态";
            this.clh5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clh5.Width = 97;
            // 
            // clh6
            // 
            this.clh6.Text = "尝试次数";
            this.clh6.Width = 79;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSign);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 48);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(934, 349);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtLog);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 243);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(934, 106);
            this.panel4.TabIndex = 4;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(934, 106);
            this.txtLog.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lvMemberList);
            this.panel3.Controls.Add(this.chkSellectAll);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(934, 243);
            this.panel3.TabIndex = 3;
            // 
            // chkSellectAll
            // 
            this.chkSellectAll.AutoSize = true;
            this.chkSellectAll.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkSellectAll.Location = new System.Drawing.Point(12, 6);
            this.chkSellectAll.Name = "chkSellectAll";
            this.chkSellectAll.Size = new System.Drawing.Size(48, 16);
            this.chkSellectAll.TabIndex = 2;
            this.chkSellectAll.Text = "全选";
            this.chkSellectAll.UseVisualStyleBackColor = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 397);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMain";
            this.Text = "易墨一键签到程序";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.ListView lvMemberList;
        private System.Windows.Forms.ColumnHeader clh1;
        private System.Windows.Forms.ColumnHeader clh2;
        private System.Windows.Forms.ColumnHeader clh3;
        private System.Windows.Forms.ColumnHeader clh4;
        private System.Windows.Forms.ColumnHeader clh5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ColumnHeader clh6;
        private System.Windows.Forms.CheckBox chkSellectAll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtLog;
    }
}


namespace MediaShare.VideoScan
{
    partial class CoverManager
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtScanPath = new System.Windows.Forms.TextBox();
            this.rbScanAll = new System.Windows.Forms.RadioButton();
            this.rbScanPath = new System.Windows.Forms.RadioButton();
            this.rbCreateNoImg = new System.Windows.Forms.RadioButton();
            this.rbOverride = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtScanPath);
            this.groupBox1.Controls.Add(this.rbScanAll);
            this.groupBox1.Controls.Add(this.rbScanPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "扫描设置";
            // 
            // txtScanPath
            // 
            this.txtScanPath.Enabled = false;
            this.txtScanPath.Location = new System.Drawing.Point(6, 42);
            this.txtScanPath.Multiline = true;
            this.txtScanPath.Name = "txtScanPath";
            this.txtScanPath.Size = new System.Drawing.Size(702, 93);
            this.txtScanPath.TabIndex = 1;
            // 
            // rbScanAll
            // 
            this.rbScanAll.AutoSize = true;
            this.rbScanAll.Checked = true;
            this.rbScanAll.Location = new System.Drawing.Point(197, 20);
            this.rbScanAll.Name = "rbScanAll";
            this.rbScanAll.Size = new System.Drawing.Size(95, 16);
            this.rbScanAll.TabIndex = 1;
            this.rbScanAll.TabStop = true;
            this.rbScanAll.Text = "扫描所有资源";
            this.rbScanAll.UseVisualStyleBackColor = true;
            this.rbScanAll.CheckedChanged += new System.EventHandler(this.rbScan_CheckedChanged);
            // 
            // rbScanPath
            // 
            this.rbScanPath.AutoSize = true;
            this.rbScanPath.Location = new System.Drawing.Point(6, 20);
            this.rbScanPath.Name = "rbScanPath";
            this.rbScanPath.Size = new System.Drawing.Size(185, 16);
            this.rbScanPath.TabIndex = 1;
            this.rbScanPath.Text = "扫描指定路径(多目录以;分隔)";
            this.rbScanPath.UseVisualStyleBackColor = true;
            this.rbScanPath.CheckedChanged += new System.EventHandler(this.rbScan_CheckedChanged);
            // 
            // rbCreateNoImg
            // 
            this.rbCreateNoImg.AutoSize = true;
            this.rbCreateNoImg.Checked = true;
            this.rbCreateNoImg.Location = new System.Drawing.Point(4, 20);
            this.rbCreateNoImg.Name = "rbCreateNoImg";
            this.rbCreateNoImg.Size = new System.Drawing.Size(95, 16);
            this.rbCreateNoImg.TabIndex = 1;
            this.rbCreateNoImg.TabStop = true;
            this.rbCreateNoImg.Text = "只生成缺失的";
            this.rbCreateNoImg.UseVisualStyleBackColor = true;
            this.rbCreateNoImg.CheckedChanged += new System.EventHandler(this.rbCreate_CheckedChanged);
            // 
            // rbOverride
            // 
            this.rbOverride.AutoSize = true;
            this.rbOverride.Location = new System.Drawing.Point(107, 20);
            this.rbOverride.Name = "rbOverride";
            this.rbOverride.Size = new System.Drawing.Size(143, 16);
            this.rbOverride.TabIndex = 1;
            this.rbOverride.Text = "重新生成并覆盖原有的";
            this.rbOverride.UseVisualStyleBackColor = true;
            this.rbOverride.CheckedChanged += new System.EventHandler(this.rbCreate_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbCreateNoImg);
            this.groupBox2.Controls.Add(this.rbOverride);
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(714, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "生成设置";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(11, 217);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(715, 161);
            this.txtLog.TabIndex = 2;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(651, 384);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "执行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // CoverManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 422);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CoverManager";
            this.Text = "CoverManager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOverride;
        private System.Windows.Forms.RadioButton rbScanAll;
        private System.Windows.Forms.RadioButton rbCreateNoImg;
        private System.Windows.Forms.RadioButton rbScanPath;
        private System.Windows.Forms.TextBox txtScanPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnRun;

    }
}
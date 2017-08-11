namespace fehteam.tools
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.lbWikiPath = new System.Windows.Forms.Label();
            this.ckProxy = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "GetWikiData";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbWikiPath
            // 
            this.lbWikiPath.AutoSize = true;
            this.lbWikiPath.Location = new System.Drawing.Point(12, 43);
            this.lbWikiPath.Name = "lbWikiPath";
            this.lbWikiPath.Size = new System.Drawing.Size(70, 13);
            this.lbWikiPath.TabIndex = 1;
            this.lbWikiPath.Text = "[contentpath]";
            // 
            // ckProxy
            // 
            this.ckProxy.AutoSize = true;
            this.ckProxy.Checked = true;
            this.ckProxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckProxy.Location = new System.Drawing.Point(13, 12);
            this.ckProxy.Name = "ckProxy";
            this.ckProxy.Size = new System.Drawing.Size(74, 17);
            this.ckProxy.TabIndex = 2;
            this.ckProxy.Text = "使用代理";
            this.ckProxy.UseVisualStyleBackColor = true;
            this.ckProxy.CheckedChanged += new System.EventHandler(this.ckProxy_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Backup2Sql";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ckProxy);
            this.Controls.Add(this.lbWikiPath);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbWikiPath;
        private System.Windows.Forms.CheckBox ckProxy;
        private System.Windows.Forms.Button button2;
    }
}


namespace SmartGC.Ui
{
    partial class FormConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxServer = new System.Windows.Forms.TextBox();
            this.tbxToken = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbxLogenable = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxExpect100Continue = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxPagesize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "令牌";
            // 
            // tbxServer
            // 
            this.tbxServer.Location = new System.Drawing.Point(104, 55);
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.Size = new System.Drawing.Size(424, 21);
            this.tbxServer.TabIndex = 5;
            this.tbxServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxToken
            // 
            this.tbxToken.Location = new System.Drawing.Point(104, 88);
            this.tbxToken.Name = "tbxToken";
            this.tbxToken.PasswordChar = '#';
            this.tbxToken.Size = new System.Drawing.Size(424, 21);
            this.tbxToken.TabIndex = 6;
            this.tbxToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.Location = new System.Drawing.Point(453, 153);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbxLogenable
            // 
            this.cbxLogenable.AutoSize = true;
            this.cbxLogenable.Location = new System.Drawing.Point(78, 157);
            this.cbxLogenable.Name = "cbxLogenable";
            this.cbxLogenable.Size = new System.Drawing.Size(15, 14);
            this.cbxLogenable.TabIndex = 11;
            this.cbxLogenable.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "日志";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Expect100Continue ";
            // 
            // cbxExpect100Continue
            // 
            this.cbxExpect100Continue.AutoSize = true;
            this.cbxExpect100Continue.Location = new System.Drawing.Point(280, 157);
            this.cbxExpect100Continue.Name = "cbxExpect100Continue";
            this.cbxExpect100Continue.Size = new System.Drawing.Size(15, 14);
            this.cbxExpect100Continue.TabIndex = 13;
            this.cbxExpect100Continue.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "每页记录数";
            // 
            // tbxPagesize
            // 
            this.tbxPagesize.Location = new System.Drawing.Point(104, 121);
            this.tbxPagesize.Name = "tbxPagesize";
            this.tbxPagesize.Size = new System.Drawing.Size(424, 21);
            this.tbxPagesize.TabIndex = 16;
            this.tbxPagesize.Text = "20";
            this.tbxPagesize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(31, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "修改设置后请退出软件重新运行。";
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 192);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxPagesize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxExpect100Continue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxLogenable);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbxToken);
            this.Controls.Add(this.tbxServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " 参数设置";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxServer;
        private System.Windows.Forms.TextBox tbxToken;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox cbxLogenable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbxExpect100Continue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxPagesize;
        private System.Windows.Forms.Label label6;
    }
}
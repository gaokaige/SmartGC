namespace SmartGC.Ui
{
    partial class FormExchange
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExchage = new System.Windows.Forms.Button();
            this.tbxCardNo = new System.Windows.Forms.TextBox();
            this.tbxCommodity = new System.Windows.Forms.TextBox();
            this.tbxCount = new System.Windows.Forms.TextBox();
            this.tbxTotalScore = new System.Windows.Forms.TextBox();
            this.tbxScoreHave = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxScorePer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IC卡编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "兑换物品";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "兑换数量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "积分合计";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "现有积分";
            // 
            // btnExchage
            // 
            this.btnExchage.Location = new System.Drawing.Point(209, 273);
            this.btnExchage.Name = "btnExchage";
            this.btnExchage.Size = new System.Drawing.Size(75, 23);
            this.btnExchage.TabIndex = 5;
            this.btnExchage.Text = "兑换";
            this.btnExchage.UseVisualStyleBackColor = true;
            this.btnExchage.Click += new System.EventHandler(this.btnExchage_Click);
            // 
            // tbxCardNo
            // 
            this.tbxCardNo.Location = new System.Drawing.Point(85, 52);
            this.tbxCardNo.Name = "tbxCardNo";
            this.tbxCardNo.ReadOnly = true;
            this.tbxCardNo.Size = new System.Drawing.Size(199, 21);
            this.tbxCardNo.TabIndex = 6;
            this.tbxCardNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxCommodity
            // 
            this.tbxCommodity.Location = new System.Drawing.Point(85, 89);
            this.tbxCommodity.Name = "tbxCommodity";
            this.tbxCommodity.ReadOnly = true;
            this.tbxCommodity.Size = new System.Drawing.Size(199, 21);
            this.tbxCommodity.TabIndex = 7;
            this.tbxCommodity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxCount
            // 
            this.tbxCount.Location = new System.Drawing.Point(85, 126);
            this.tbxCount.MaxLength = 5;
            this.tbxCount.Name = "tbxCount";
            this.tbxCount.Size = new System.Drawing.Size(199, 21);
            this.tbxCount.TabIndex = 8;
            this.tbxCount.Text = "1";
            this.tbxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxCount.TextChanged += new System.EventHandler(this.tbxCount_TextChanged);
            // 
            // tbxTotalScore
            // 
            this.tbxTotalScore.Location = new System.Drawing.Point(85, 200);
            this.tbxTotalScore.Name = "tbxTotalScore";
            this.tbxTotalScore.ReadOnly = true;
            this.tbxTotalScore.Size = new System.Drawing.Size(199, 21);
            this.tbxTotalScore.TabIndex = 9;
            this.tbxTotalScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxScoreHave
            // 
            this.tbxScoreHave.Location = new System.Drawing.Point(85, 237);
            this.tbxScoreHave.Name = "tbxScoreHave";
            this.tbxScoreHave.ReadOnly = true;
            this.tbxScoreHave.Size = new System.Drawing.Size(199, 21);
            this.tbxScoreHave.TabIndex = 10;
            this.tbxScoreHave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(25, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "请将IC卡放入读卡区";
            // 
            // tbxScorePer
            // 
            this.tbxScorePer.Location = new System.Drawing.Point(85, 163);
            this.tbxScorePer.Name = "tbxScorePer";
            this.tbxScorePer.ReadOnly = true;
            this.tbxScorePer.Size = new System.Drawing.Size(199, 21);
            this.tbxScorePer.TabIndex = 13;
            this.tbxScorePer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "积分(个)";
            // 
            // FormExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 306);
            this.Controls.Add(this.tbxScorePer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxScoreHave);
            this.Controls.Add(this.tbxTotalScore);
            this.Controls.Add(this.tbxCount);
            this.Controls.Add(this.tbxCommodity);
            this.Controls.Add(this.tbxCardNo);
            this.Controls.Add(this.btnExchage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormExchange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "兑换";
            this.Load += new System.EventHandler(this.FormExchange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExchage;
        private System.Windows.Forms.TextBox tbxCardNo;
        private System.Windows.Forms.TextBox tbxCommodity;
        private System.Windows.Forms.TextBox tbxCount;
        private System.Windows.Forms.TextBox tbxTotalScore;
        private System.Windows.Forms.TextBox tbxScoreHave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxScorePer;
        private System.Windows.Forms.Label label7;
    }
}
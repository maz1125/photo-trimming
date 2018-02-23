namespace PhotoTrimminger
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.selectTargetFolderButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.trimmingBtn = new System.Windows.Forms.Button();
            this.consoleTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // selectTargetFolderButton
            // 
            this.selectTargetFolderButton.Location = new System.Drawing.Point(356, 33);
            this.selectTargetFolderButton.Name = "selectTargetFolderButton";
            this.selectTargetFolderButton.Size = new System.Drawing.Size(89, 25);
            this.selectTargetFolderButton.TabIndex = 0;
            this.selectTargetFolderButton.Text = "参照";
            this.selectTargetFolderButton.UseVisualStyleBackColor = true;
            this.selectTargetFolderButton.Click += new System.EventHandler(this.selectTargetFolderButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(328, 19);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "トリミング対象フォルダ";
            // 
            // trimmingBtn
            // 
            this.trimmingBtn.Location = new System.Drawing.Point(356, 74);
            this.trimmingBtn.Name = "trimmingBtn";
            this.trimmingBtn.Size = new System.Drawing.Size(89, 23);
            this.trimmingBtn.TabIndex = 3;
            this.trimmingBtn.Text = "実行";
            this.trimmingBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.trimmingBtn.UseVisualStyleBackColor = true;
            this.trimmingBtn.Click += new System.EventHandler(this.trimmingBtn_Click);
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.Location = new System.Drawing.Point(22, 110);
            this.consoleTextBox.Multiline = true;
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.Size = new System.Drawing.Size(423, 218);
            this.consoleTextBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 364);
            this.Controls.Add(this.consoleTextBox);
            this.Controls.Add(this.trimmingBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.selectTargetFolderButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectTargetFolderButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button trimmingBtn;
        private System.Windows.Forms.TextBox consoleTextBox;
    }
}


namespace School_Calendar_Converter
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.pdfFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pdfFileTextBox = new System.Windows.Forms.TextBox();
            this.pdfFileButton = new System.Windows.Forms.Button();
            this.pdfFileLabel = new System.Windows.Forms.Label();
            this.executeButton = new System.Windows.Forms.Button();
            this.pdfFileLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // pdfFileTextBox
            // 
            this.pdfFileTextBox.Location = new System.Drawing.Point(12, 24);
            this.pdfFileTextBox.Name = "pdfFileTextBox";
            this.pdfFileTextBox.ReadOnly = true;
            this.pdfFileTextBox.Size = new System.Drawing.Size(380, 19);
            this.pdfFileTextBox.TabIndex = 0;
            // 
            // pdfFileButton
            // 
            this.pdfFileButton.Location = new System.Drawing.Point(398, 22);
            this.pdfFileButton.Name = "pdfFileButton";
            this.pdfFileButton.Size = new System.Drawing.Size(45, 23);
            this.pdfFileButton.TabIndex = 1;
            this.pdfFileButton.Text = "...";
            this.pdfFileButton.UseVisualStyleBackColor = true;
            this.pdfFileButton.Click += new System.EventHandler(this.excelFileButton_Click);
            // 
            // pdfFileLabel
            // 
            this.pdfFileLabel.AutoSize = true;
            this.pdfFileLabel.Location = new System.Drawing.Point(12, 9);
            this.pdfFileLabel.Name = "pdfFileLabel";
            this.pdfFileLabel.Size = new System.Drawing.Size(140, 12);
            this.pdfFileLabel.TabIndex = 2;
            this.pdfFileLabel.Text = "読み込むPDFファイルの選択";
            // 
            // executeButton
            // 
            this.executeButton.Location = new System.Drawing.Point(368, 53);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(75, 23);
            this.executeButton.TabIndex = 5;
            this.executeButton.Text = "実行";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // pdfFileLinkLabel
            // 
            this.pdfFileLinkLabel.AutoSize = true;
            this.pdfFileLinkLabel.Location = new System.Drawing.Point(313, 9);
            this.pdfFileLinkLabel.Name = "pdfFileLinkLabel";
            this.pdfFileLinkLabel.Size = new System.Drawing.Size(130, 12);
            this.pdfFileLinkLabel.TabIndex = 6;
            this.pdfFileLinkLabel.TabStop = true;
            this.pdfFileLinkLabel.Text = "行事予定表のダウンロード";
            this.pdfFileLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.pdfFileLinkLabel_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 88);
            this.Controls.Add(this.pdfFileLinkLabel);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.pdfFileLabel);
            this.Controls.Add(this.pdfFileButton);
            this.Controls.Add(this.pdfFileTextBox);
            this.Name = "MainForm";
            this.Text = "鹿児島高専 行事予定表 変換ソフト";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog pdfFileDialog;
        private System.Windows.Forms.TextBox pdfFileTextBox;
        private System.Windows.Forms.Button pdfFileButton;
        private System.Windows.Forms.Label pdfFileLabel;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.LinkLabel pdfFileLinkLabel;
    }
}


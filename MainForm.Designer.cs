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
            this.excelFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.excelFileTextBox = new System.Windows.Forms.TextBox();
            this.excelFileButton = new System.Windows.Forms.Button();
            this.pdfFileLabel = new System.Windows.Forms.Label();
            this.executeButton = new System.Windows.Forms.Button();
            this.pdfFileLinkLabel = new System.Windows.Forms.LinkLabel();
            this.PDF2ExcelLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // excelFileTextBox
            // 
            this.excelFileTextBox.Location = new System.Drawing.Point(12, 24);
            this.excelFileTextBox.Name = "excelFileTextBox";
            this.excelFileTextBox.ReadOnly = true;
            this.excelFileTextBox.Size = new System.Drawing.Size(725, 19);
            this.excelFileTextBox.TabIndex = 0;
            // 
            // excelFileButton
            // 
            this.excelFileButton.Location = new System.Drawing.Point(743, 22);
            this.excelFileButton.Name = "excelFileButton";
            this.excelFileButton.Size = new System.Drawing.Size(45, 23);
            this.excelFileButton.TabIndex = 1;
            this.excelFileButton.Text = "...";
            this.excelFileButton.UseVisualStyleBackColor = true;
            this.excelFileButton.Click += new System.EventHandler(this.excelFileButton_Click);
            // 
            // pdfFileLabel
            // 
            this.pdfFileLabel.AutoSize = true;
            this.pdfFileLabel.Location = new System.Drawing.Point(12, 9);
            this.pdfFileLabel.Name = "pdfFileLabel";
            this.pdfFileLabel.Size = new System.Drawing.Size(146, 12);
            this.pdfFileLabel.TabIndex = 2;
            this.pdfFileLabel.Text = "読み込むExcelファイルの選択";
            // 
            // executeButton
            // 
            this.executeButton.Location = new System.Drawing.Point(713, 415);
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
            this.pdfFileLinkLabel.Location = new System.Drawing.Point(555, 9);
            this.pdfFileLinkLabel.Name = "pdfFileLinkLabel";
            this.pdfFileLinkLabel.Size = new System.Drawing.Size(130, 12);
            this.pdfFileLinkLabel.TabIndex = 6;
            this.pdfFileLinkLabel.TabStop = true;
            this.pdfFileLinkLabel.Text = "行事予定表のダウンロード";
            this.pdfFileLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.pdfFileLinkLabel_LinkClicked);
            // 
            // PDF2ExcelLinkLabel
            // 
            this.PDF2ExcelLinkLabel.AutoSize = true;
            this.PDF2ExcelLinkLabel.Location = new System.Drawing.Point(691, 9);
            this.PDF2ExcelLinkLabel.Name = "PDF2ExcelLinkLabel";
            this.PDF2ExcelLinkLabel.Size = new System.Drawing.Size(97, 12);
            this.PDF2ExcelLinkLabel.TabIndex = 7;
            this.PDF2ExcelLinkLabel.TabStop = true;
            this.PDF2ExcelLinkLabel.Text = "PDFをExcelに変換";
            this.PDF2ExcelLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PDF2ExcelLinkLabel_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PDF2ExcelLinkLabel);
            this.Controls.Add(this.pdfFileLinkLabel);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.pdfFileLabel);
            this.Controls.Add(this.excelFileButton);
            this.Controls.Add(this.excelFileTextBox);
            this.Name = "MainForm";
            this.Text = "鹿児島高専 行事予定表 変換ソフト";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog excelFileDialog;
        private System.Windows.Forms.TextBox excelFileTextBox;
        private System.Windows.Forms.Button excelFileButton;
        private System.Windows.Forms.Label pdfFileLabel;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.LinkLabel pdfFileLinkLabel;
        private System.Windows.Forms.LinkLabel PDF2ExcelLinkLabel;
    }
}


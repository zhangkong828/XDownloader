
namespace XDownloader.Windows
{
    partial class CreateWindow
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
            this.AddLinksTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DownloadDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.FolderBrowserButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // AddLinksTextBox
            // 
            this.AddLinksTextBox.Location = new System.Drawing.Point(22, 26);
            this.AddLinksTextBox.Multiline = true;
            this.AddLinksTextBox.Name = "AddLinksTextBox";
            this.AddLinksTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AddLinksTextBox.Size = new System.Drawing.Size(605, 132);
            this.AddLinksTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "下载到：";
            // 
            // DownloadDirectoryTextBox
            // 
            this.DownloadDirectoryTextBox.Location = new System.Drawing.Point(22, 205);
            this.DownloadDirectoryTextBox.Name = "DownloadDirectoryTextBox";
            this.DownloadDirectoryTextBox.Size = new System.Drawing.Size(533, 21);
            this.DownloadDirectoryTextBox.TabIndex = 2;
            // 
            // FolderBrowserButton
            // 
            this.FolderBrowserButton.Location = new System.Drawing.Point(561, 203);
            this.FolderBrowserButton.Name = "FolderBrowserButton";
            this.FolderBrowserButton.Size = new System.Drawing.Size(66, 23);
            this.FolderBrowserButton.TabIndex = 3;
            this.FolderBrowserButton.Text = "选择目录";
            this.FolderBrowserButton.UseVisualStyleBackColor = true;
            this.FolderBrowserButton.Click += new System.EventHandler(this.FolderBrowserButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(248, 251);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(101, 31);
            this.DownloadButton.TabIndex = 4;
            this.DownloadButton.Text = "下载";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // CreateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(653, 311);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.FolderBrowserButton);
            this.Controls.Add(this.DownloadDirectoryTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddLinksTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加链接";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AddLinksTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DownloadDirectoryTextBox;
        private System.Windows.Forms.Button FolderBrowserButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
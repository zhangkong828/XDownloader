using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XDownloader.Windows
{
    public partial class CreateWindow : Form
    {
        public CreateWindow()
        {
            InitializeComponent();

            SelectedPath = DownloadDirectoryTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        public string SelectedPath { get; set; }

        private void FolderBrowserButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择文件夹";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.SelectedPath = dialog.SelectedPath;
                DownloadDirectoryTextBox.Text = SelectedPath;
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(this.SelectedPath))
            {
                MessageBox.Show("下载目录不能为空");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}

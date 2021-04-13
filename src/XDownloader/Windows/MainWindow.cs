using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XDownloader.Infrastructure;
using XDownloader.Models;

namespace XDownloader.Windows
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            var plist = Process.GetProcessesByName("ffmpeg");
            foreach (var p in plist) p.Kill();
            plist = Process.GetProcessesByName("annie");
            foreach (var p in plist) p.Kill();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var createForm = new CreateWindow();
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                var downloadPath = createForm.SelectedPath;
                var links = createForm.Links;
                Download(links, downloadPath);
            }
        }

        private void Download(string url, string output)
        {
            var subItems = new string[] { url, "--", "--", "--", "--", "--", "--","准备中" };
            ListViewItem currentItem = new ListViewItem(subItems);
            ListView.Items.Add(currentItem);

            Task.Run(() =>
            {
                try
                {
                    var sb = new StringBuilder();
                    sb.Append($" -o {output}");
                    sb.Append($" {url}");

                    var arguments = sb.ToString();
                    var process = new ProcessHelper(AnnieHelper.ExePath, arguments, new Action<string>(log =>
                    {
                        Invoke(new Action(() =>
                        {
                            if (!string.IsNullOrWhiteSpace(log))
                            {
                                DownloadInfo downloadInfo = null;
                                try
                                {
                                    downloadInfo = AnnieHelper.ParseOutput(log);
                                }
                                catch (Exception ex)
                                {
                                    loggerTextBox.AppendText(ex.Message);
                                }

                                var title = currentItem.SubItems[0];
                                var site = currentItem.SubItems[1];
                                var type = currentItem.SubItems[2];
                                var size = currentItem.SubItems[3];
                                var progress = currentItem.SubItems[4];
                                var speed = currentItem.SubItems[5];
                                var time = currentItem.SubItems[6];
                                var status = currentItem.SubItems[7];

                                if (downloadInfo.IsMessage)
                                {
                                    loggerTextBox.AppendText(downloadInfo.Message);
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(downloadInfo.Title))
                                        title.Text = downloadInfo.Title;
                                    if (!string.IsNullOrWhiteSpace(downloadInfo.Site))
                                        site.Text = downloadInfo.Site;
                                    if (!string.IsNullOrWhiteSpace(downloadInfo.Type))
                                        type.Text = downloadInfo.Type;

                                    size.Text = downloadInfo.Size;
                                    progress.Text = downloadInfo.Progress;
                                    speed.Text = downloadInfo.Speed;
                                    time.Text = downloadInfo.Time;
                                    status.Text = "下载中";

                                    if (downloadInfo.Progress=="100.00%")
                                    {
                                        status.Text = "完成";
                                        speed.Text = "";
                                        time.Text = "";
                                    }
                                }
                            }
                        }));
                    }));
                    process.Start();

                }
                catch (Exception ex)
                {
                    loggerTextBox.AppendText(ex.Message);
                }
            });
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XDownloader.Infrastructure;

namespace XDownloader.Windows
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogOutput(string log)
        {
            Invoke(new Action(() =>
            {
                if (!string.IsNullOrWhiteSpace(log))
                    loggerTextBox.AppendText(log + Environment.NewLine);

            }));
        }


        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var createForm = new CreateWindow();
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                var downloadPath = createForm.SelectedPath;
                var links = createForm.Links;
                AnnieHelper.Download(links, downloadPath, LogOutput);
            }
        }



    }
}

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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var createForm = new CreateWindow();
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = createForm.SelectedPath;
            }
        }
    }
}

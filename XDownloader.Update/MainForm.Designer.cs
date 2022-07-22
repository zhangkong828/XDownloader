
namespace XDownloader.Update
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lab_close = new System.Windows.Forms.Label();
            this.lab_quit = new System.Windows.Forms.Label();
            this.panel_main = new System.Windows.Forms.Panel();
            this.pic_title = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pic_close = new System.Windows.Forms.PictureBox();
            this.lab_text = new System.Windows.Forms.Label();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_close)).BeginInit();
            this.SuspendLayout();
            // 
            // lab_close
            // 
            this.lab_close.AutoSize = true;
            this.lab_close.BackColor = System.Drawing.Color.Transparent;
            this.lab_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lab_close.ForeColor = System.Drawing.Color.Blue;
            this.lab_close.Location = new System.Drawing.Point(321, 78);
            this.lab_close.Name = "lab_close";
            this.lab_close.Size = new System.Drawing.Size(32, 17);
            this.lab_close.TabIndex = 10;
            this.lab_close.Text = "重启";
            this.lab_close.Click += new System.EventHandler(this.lab_close_Click);
            // 
            // lab_quit
            // 
            this.lab_quit.AutoSize = true;
            this.lab_quit.BackColor = System.Drawing.Color.Transparent;
            this.lab_quit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lab_quit.ForeColor = System.Drawing.Color.Blue;
            this.lab_quit.Location = new System.Drawing.Point(356, 78);
            this.lab_quit.Name = "lab_quit";
            this.lab_quit.Size = new System.Drawing.Size(32, 17);
            this.lab_quit.TabIndex = 12;
            this.lab_quit.Text = "退出";
            this.lab_quit.Click += new System.EventHandler(this.lab_quit_Click);
            // 
            // panel_main
            // 
            //this.panel_main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_main.BackgroundImage")));
            this.panel_main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_main.Controls.Add(this.pic_title);
            this.panel_main.Controls.Add(this.lab_quit);
            this.panel_main.Controls.Add(this.progressBar);
            this.panel_main.Controls.Add(this.lab_close);
            this.panel_main.Controls.Add(this.pic_close);
            this.panel_main.Controls.Add(this.lab_text);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(400, 105);
            this.panel_main.TabIndex = 19;
            this.panel_main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseDown);
            this.panel_main.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseMove);
            this.panel_main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseUp);
            // 
            // pic_title
            // 
            this.pic_title.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic_title.Location = new System.Drawing.Point(-1, -1);
            this.pic_title.Name = "pic_title";
            this.pic_title.Size = new System.Drawing.Size(16, 16);
            this.pic_title.TabIndex = 18;
            this.pic_title.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(25, 30);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(349, 16);
            this.progressBar.TabIndex = 1;
            // 
            // pic_close
            // 
            this.pic_close.Anchor = System.Windows.Forms.AnchorStyles.Top;
            //this.pic_close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_close.BackgroundImage")));
            this.pic_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pic_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_close.Location = new System.Drawing.Point(380, 0);
            this.pic_close.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.pic_close.Name = "pic_close";
            this.pic_close.Size = new System.Drawing.Size(16, 16);
            this.pic_close.TabIndex = 17;
            this.pic_close.TabStop = false;
            this.pic_close.Click += new System.EventHandler(this.pic_close_Click);
            // 
            // lab_text
            // 
            this.lab_text.AutoSize = true;
            this.lab_text.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lab_text.Location = new System.Drawing.Point(22, 59);
            this.lab_text.Name = "lab_text";
            this.lab_text.Size = new System.Drawing.Size(128, 17);
            this.lab_text.TabIndex = 8;
            this.lab_text.Text = "正在检查更新,请稍后...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 105);
            this.Controls.Add(this.panel_main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检查更新";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_close)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lab_close;
        private System.Windows.Forms.Label lab_quit;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.PictureBox pic_title;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pic_close;
        private System.Windows.Forms.Label lab_text;
    }
}
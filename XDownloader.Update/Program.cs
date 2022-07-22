using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XDownloader.Update
{
    static class Program
    {

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //同一时间只允许启动一个
            var runMutex = new Mutex(true, Application.ProductName, out bool result);
            if (result == false)
            {
               // MessageBox.Show("目前已有一个程序在运行，请勿重复运行程序");
                Environment.Exit(0);
            }


            //当前用户是管理员的时候，直接启动应用程序
            //如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                Start();
            }
            else
            {
                var startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Application.StartupPath;
                startInfo.FileName = Application.ExecutablePath;
                startInfo.Verb = "runas";

                var newThread = new Thread(() =>
                {
                    Thread.Sleep(500);
                    try
                    {
                        Process.Start(startInfo);
                    }
                    catch
                    {
                        //
                    }
                });
                newThread.IsBackground = false;
                newThread.Start();

                Application.Exit();
            }
        }

        static void Start()
        {
            int pId = Process.GetCurrentProcess().Id;
            string pName = Process.GetCurrentProcess().ProcessName;
            bool isRun = false;

            foreach (Process p in Process.GetProcessesByName(pName))
            {
                try
                {
                    //当前目录下只能启动一个(多个系统账户下也一样)
                    if (System.Reflection.Assembly.GetExecutingAssembly().Location.ToLower() == p.MainModule.FileName.ToLower())
                    {
                        if (pId != p.Id)
                        {
                            isRun = true;
                            string handle = File.ReadAllText(Application.StartupPath + @"\handle.ini"); //FileHelper.ReadKeyValue(Application.StartupPath + @"\" + Process.GetCurrentProcess().ProcessName + ".exe.config", "MainWindowHandle");
                            int h;
                            if (int.TryParse(handle, out h))
                            {
                                ShowWindow((IntPtr)h, 1);
                                SetWindowPos((IntPtr)h, -1, 0, 0, 0, 0, 1 | 2);
                            }
                            break;
                        }
                    }
                }
                catch
                {

                }

            }

            if (!isRun)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
                Application.Exit();
        }

    }
}

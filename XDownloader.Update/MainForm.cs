using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XDownloader.Update
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
        }

        static string updateUrl;
        static string updateDownUrl;

        static string updatePath = Application.StartupPath + @"\" + Process.GetCurrentProcess().ProcessName + ".exe";
        //static string updateConfigPath = Application.StartupPath + @"\" + Process.GetCurrentProcess().ProcessName + ".exe.config";

        static string updateConfigPath = Application.StartupPath + @"\handle.ini";

        static string servicePath = Application.StartupPath + @"\BiHu.BaoXian.Service\";
        static string serviceConfigPath = Application.StartupPath + @"\BiHu.BaoXian.Service\";
        static string appsettingsConfigPath = Application.StartupPath + @"\BiHu.BaoXian.Service\appsettings.config";
        static string artificialPath = string.Empty;
        //static string artificialPathVPN = string.Empty;
        //static string PathVPNName = string.Empty;
        static string artificialName = string.Empty;
        static string macSuffix = string.Empty;
        static string updateProcessName = string.Empty;

        static string frameDir = Application.StartupPath + @"\BiHu.BaoXian.Service";
        static string bodyDir = Application.StartupPath + @"\BiHu.BaoXian.Service\lib";

        static bool isSetShowHide = false;


        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                string title = string.Format("{0} {1}", this.Text, "");

                //检查更新
                Task.Run(() =>
                {
                    int updateTimes = 0;
                    while (true)
                    {
                        if (updateTimes <= 0)
                        {
                            lab_close.Enabled = false;

                            this.Text = title;
                            UpdateCheck();
                            updateTimes = 60 * new Random().Next(10, 20);

                            lab_close.Enabled = true;
                        }
                        lab_text.Text = string.Format("{0} -> {1}分{2}秒", title, updateTimes / 60, updateTimes % 60);
                        updateTimes--;
                        Thread.Sleep(1000);

                        //凌晨3点
                        if (DateTime.Now.Hour == 3 && DateTime.Now.Minute == 0)
                        {
                            //RestartComputer.JudgeRestart();

                            lab_close_Click(null, null);
                            Thread.Sleep(1000 * 60);
                        }
                        else
                        {
                            //检查被关闭后的重启60秒
                            //if (updateTimes != 0 && updateTimes % 60 == 0)
                            //    if (GetServiceProcess() == null)
                            //        Process.Start(servicePath);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                //Log.FatalFormat("MainForm_Load 发生异常:{0}", ex.Message);
            }
        }

        /// <summary>
        /// 关闭更改为隐藏窗口
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        /// <summary>
        /// 重启
        /// </summary>
        private void lab_close_Click(object sender, EventArgs e)
        {
            //StopService();

            //Thread.Sleep(1000);
            //StartServie(macSuffix, string.Format("已重新启动！\r\n欢迎使用{0}系统.", Process.GetCurrentProcess().ProcessName), 1);
        }

        /// <summary>
        /// 退出整个
        /// </summary>
        private void lab_quit_Click(object sender, EventArgs e)
        {
            //StopService();
            //Environment.Exit(0);
        }

        private void pic_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Point _mouseOff; //鼠标移动位置变量
        private bool _leftFlag; //标签是否为左键

        private void panel_main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                _leftFlag = true; //点击左键按下时标注为true
            }
        }

        private void panel_main_MouseMove(object sender, MouseEventArgs e)
        {
            if (_leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(_mouseOff.X, _mouseOff.Y); //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void panel_main_MouseUp(object sender, MouseEventArgs e)
        {
            if (_leftFlag)
            {
                _leftFlag = false; //释放鼠标后标注为false
            }
        }

        /// <summary>
        /// 设置进度条
        /// </summary>
        private void AddProgressBar(int value)
        {
            lock (progressBar)
            {
                if (progressBar.Value + value <= 100)
                    progressBar.Value += value;
                else
                    progressBar.Value = 100;
            }
        }

        /// <summary>
        /// 重置进度条
        /// </summary>
        private void ResetProgressBar()
        {
            lock (progressBar)
            {
                progressBar.Value = 0;
            }
        }

        /// <summary>
        /// 执行过程说明框
        /// </summary>
        private void Record(string text)
        {
            lab_text.Text = text;
            //Log.InfoFormat(text);
        }


        [DllImport("kernel32.dll")]
        static extern bool GenerateConsoleCtrlEvent(int dwCtrlEvent, int dwProcessGroupId);
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(IntPtr handlerRoutine, bool add);
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        /// <summary>
        /// 关闭服务,可能会多次调用
        /// </summary>
        private void StopService()
        {
            try
            {
                //先用命令关闭
                var pro = GetServiceProcess();
                if (pro != null)
                {
                    //AttachConsole(pro.Id); //附加到目标进程的console
                    //SetConsoleCtrlHandler(IntPtr.Zero, true); //设置自己的ctrl+c处理，防止自己被终止
                    //GenerateConsoleCtrlEvent(0, 0); //发送ctrl+c（注意：这是向所有共享该console的进程发送）
                    //FreeConsole(); //脱离目标console
                }

                //1秒后再进行强制关闭
                Thread.Sleep(1000);

                pro = GetServiceProcess();
                if (pro != null)
                    pro.Kill();

                //foreach (var p in Process.GetProcessesByName(artificialName))
                //{
                //    try
                //    {
                //        if (artificialPath.ToLower() == p.MainModule.FileName.ToLower())
                //            p.Kill();
                //    }
                //    catch (Exception e)
                //    {
                //        Log.ErrorFormat("message:{0}\r\nStackTrace{1}", e.Message, e.StackTrace);
                //        string covertLibPath = Application.StartupPath + @"\BiHu.BaoXian.Service\";
                //        //可能访问到64位进程
                //        Process.Start("KillPro.exe");
                //        Thread.Sleep(10000);
                //        break;
                //    }
                //}

            }
            catch (Exception ex)
            {
               // Log.FatalFormat("关闭时发生异常:{0}", ex.Message);
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        private void StartServie(string name, string text, int type)
        {
            try
            {
                Hide();

                if (GetServiceProcess() == null)
                {
                    //Process.Start(servicePath);
                    ShowMessage(name, text, type);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(name, "启动失败！\r\n请联系交互或运营人员.", 2);
                //Log.FatalFormat("启动时发生异常:{0}", ex.Message);
                //Log.FatalFormat(ex.StackTrace);
            }
        }

        /// <summary>
        /// 得到执行的服务程序
        /// </summary>
        private Process GetServiceProcess()
        {
            //foreach (var p in Process.GetProcessesByName(updateProcessName))
            //{
            //    try
            //    {
            //        if (servicePath.ToLower() == p.MainModule.FileName.ToLower())
            //            return p;
            //    }
            //    catch
            //    {
            //        //可能访问到64位进程
            //    }
            //}
            return null;
        }

        /// <summary>
        /// 显示提示消息
        /// </summary>
        private void ShowMessage(string name, string text, int type, int times = 9)
        {
            Hide();
            Thread newThread = new Thread(() =>
            {
                try
                {
                    Thread.Sleep(3000);
                    new ShowMessage(name, text, type, times).ShowDialog();
                }
                catch
                { }
            });
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.IsBackground = true; //随主线程一同退出
            newThread.Start();
        }

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        private void Hide()
        {
            //隐藏自己,同时将主窗口句柄记录到文件中
            var handle = Process.GetCurrentProcess().MainWindowHandle;
            if ((int)handle != 0)
            {
                Program.ShowWindow(handle, 0);
                if (!isSetShowHide)
                {
                    //FileHelper.SaveKeyValue(updateConfigPath, "MainWindowHandle", handle.ToString());
                    File.WriteAllText(updateConfigPath, handle.ToString());
                    isSetShowHide = true;
                }
            }
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        private void Show()
        {
            //string handle = FileHelper.ReadKeyValue(updateConfigPath, "MainWindowHandle");
            string handle = File.ReadAllText(updateConfigPath);// FileHelper.ReadKeyValue(updateConfigPath, "MainWindowHandle");
            int h;
            if (int.TryParse(handle, out h))
                Program.ShowWindow((IntPtr)h, 1);
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        private void UpdateCheck()
        {
            try
            {
                //得到本地的两个版本号
                //string frameVersion = FileHelper.ReadKeyValue(serviceConfigPath, "ClientVersion").Trim();
                //string bodyVersion = FileHelper.ReadKeyValue(appsettingsConfigPath, "ClientVersion").Trim();
                //string frameType = FileHelper.ReadKeyValue(serviceConfigPath, "VersionType").Trim();
                //string bodyType = FileHelper.ReadKeyValue(appsettingsConfigPath, "VersionType").Trim();


                bool isUpdateOk = true;
                string frameResponse = string.Empty;
                string bodyResponse = string.Empty;

                #region 得到服务器的两个版本信息
                //string serviceUpdateUrl = updateUrl + frameType;
                //string bodyUpdateUrl = updateUrl + bodyType;

                bool isOver = false;
                Task.Run(() =>
                {
                    while (!isOver)
                    {
                        AddProgressBar(1);
                        Thread.Sleep(1000);
                    }
                });

                Task[] tasks = new Task[2];
                tasks[0] = Task.Run(() =>
                {
                    try
                    {
                        //Record("正在检查 -> " + frameType);
                        //using (var httpClientHandler = new HttpClientHandler())
                        //{
                        //    httpClientHandler.Proxy = null;
                        //    httpClientHandler.UseProxy = false;
                        //    using (var client = new HttpClient(httpClientHandler))
                        //    {
                        //        client.Timeout = new TimeSpan(0, 0, 15);
                        //        frameResponse = client.GetStringAsync(serviceUpdateUrl).Result;
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        //Record(frameType + " -> " + ex.Message);
                        //Log.ErrorFormat("frameResponse 发生异常:{0},{1}", serviceUpdateUrl, ex.Message);
                    }
                });
                tasks[1] = Task.Run(() =>
                {
                    //try
                    //{
                    //    Record("正在检查 -> " + bodyType);
                    //    using (var client = new HttpClient())
                    //    {
                    //        client.Timeout = new TimeSpan(0, 0, 15);
                    //        bodyResponse = client.GetStringAsync(bodyUpdateUrl).Result;
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Record(bodyType + " -> " + ex.Message);
                    //    Log.ErrorFormat("bodyResponse 发生异常:{0},{1}", bodyUpdateUrl, ex.Message);
                    //}
                });
                Task.WaitAll(tasks);

                //如果exe程序丢失
                //if (!File.Exists(servicePath) && frameResponse != null && frameResponse.Contains(".exe"))
                //    frameVersion = "0";

                //if (!File.Exists(artificialPath) && bodyResponse != null && bodyResponse.Contains(".exe"))
                //    bodyVersion = "0";

                isOver = true;
                AddProgressBar(100);
                #endregion

                //是否能正常获取
                //if (!string.IsNullOrWhiteSpace(frameResponse) || !string.IsNullOrWhiteSpace(bodyResponse))
                //{
                //    var jsonFrame = JsonConvert.DeserializeObject<dynamic>(frameResponse);
                //    var jsonBody = JsonConvert.DeserializeObject<dynamic>(bodyResponse);

                //    #region 检查返回信息,检查是否有更新 (注：线上只要版本号不一致就更新，线下低于时才更新)
                //    if (jsonFrame != null && jsonFrame.Version != null)
                //    {
                //        if ((Program.IsOnLine && jsonFrame.Version.ToString().Trim() != frameVersion) || jsonFrame.Version.ToString().CompareTo(frameVersion) > 0)
                //        {
                //            if (jsonFrame.Urls != null && jsonFrame.Urls.ToString() != string.Empty && jsonFrame.Urls.ToString() != "[]")
                //            {
                //                ResetProgressBar();
                //                Record("有更新 -> " + frameType);

                //                isUpdateOk = DowLoadAndUpdate(1, frameType, jsonFrame.Version.ToString());
                //            }
                //            else
                //                isUpdateOk = false;
                //        }
                //    }
                //    else
                //        isUpdateOk = false;

                //    if (jsonBody != null && (frameType != "Service_Artificial_version" || jsonBody.Version != null))
                //    {
                //        if ((Program.IsOnLine && jsonBody.Version.ToString().Trim() != bodyVersion) || jsonBody.Version.ToString().CompareTo(bodyVersion) > 0)
                //        {
                //            if (jsonBody.Urls != null && jsonBody.Urls.ToString() != string.Empty && jsonBody.Urls.ToString() != "[]")
                //            {
                //                ResetProgressBar();
                //                Record("有更新 -> " + bodyType);

                //                isUpdateOk = DowLoadAndUpdate(2, bodyType, jsonBody.Version.ToString());
                //            }
                //            else
                //                isUpdateOk = false;
                //        }
                //    }
                //    else
                //        isUpdateOk = false;
                //    #endregion
                //}

                if (!isUpdateOk)
                {
                    StartServie(macSuffix, "已启动！但检查更新失败.\r\n建议联系交互或运营人员询问原因.", 3);
                }
                else
                    StartServie(macSuffix, string.Format("已启动！检查更新成功.\r\n欢迎使用{0}系统.", Process.GetCurrentProcess().ProcessName), 1);
            }
            catch (Exception ex)
            {
                StartServie(macSuffix, "已启动！但检查更新异常.\r\n请联系技术人员.", 2);
                //Log.FatalFormat("检查更新发生异常:{0}", ex.Message);
            }
            finally
            {
                ResetProgressBar();
            }
        }
    }
}

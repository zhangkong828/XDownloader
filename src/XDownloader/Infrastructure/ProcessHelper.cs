using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDownloader.Infrastructure
{
    public class ProcessHelper
    {
        private Process dlProcess;

        private void PrepareDlProcess()
        {
            dlProcess = new Process();
            dlProcess.StartInfo.FileName = "";
            dlProcess.StartInfo.CreateNoWindow = true;
            dlProcess.StartInfo.UseShellExecute = false;
            dlProcess.StartInfo.RedirectStandardError = true;
            dlProcess.StartInfo.RedirectStandardOutput = true;
            dlProcess.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            dlProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            dlProcess.EnableRaisingEvents = true;
            dlProcess.ErrorDataReceived += DlOutputHandler;
            dlProcess.OutputDataReceived += DlOutputHandler;
            dlProcess.Exited += DlProcess_Exited;
        }

        private void DlProcess_Exited(object sender, EventArgs e)
        {
            dlProcess.CancelErrorRead();
            dlProcess.CancelOutputRead();
        }

        private void DlOutputHandler(object sender, DataReceivedEventArgs e)
        {
            var text = e.Data;
        }

        private void Start()
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(" --encoding \"UTF-8\" --no-warnings --ignore-errors");

                dlProcess.StartInfo.Arguments = sb.ToString();
                
                dlProcess.Start();
                dlProcess.BeginErrorReadLine();
                dlProcess.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                //outputString.Append(ex.Message);
            }
            finally
            {
            }
        }

        private void AbortDl()
        {
            var plist = Process.GetProcessesByName("ffmpeg");
            foreach (var p in plist) p.Kill();

            try
            {
                // yes, I know it's bad to just kill the process.
                // but currently .NET Core doesn't have an API for sending ^C or SIGTERM to a process
                // see https://github.com/dotnet/runtime/issues/14628
                // To implement a platform-specific solution,
                // we need to use Win32 APIs.
                // see https://stackoverflow.com/questions/283128/how-do-i-send-ctrlc-to-a-process-in-c
                // I would prefer not to use Win32 APIs in the application.
                dlProcess.Kill();
            }
            catch (Exception ex)
            {
                //Output = ex.Message;
            }
        }
    }
}

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
        private Process _process;
        private string _filename;
        private string _arguments;
        private Action<string> _action;

        public ProcessHelper(string filename, string arguments, Action<string> outputAction)
        {
            _filename = filename;
            _arguments = arguments;
            _action = outputAction;

            Init();
        }

        private void Init()
        {
            _process = new Process();
            _process.StartInfo.FileName = _filename;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            _process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            _process.EnableRaisingEvents = true;
            _process.ErrorDataReceived += OutputHandler;
            _process.OutputDataReceived += OutputHandler;
            _process.Exited += ExitedHandler;
        }

        private void ExitedHandler(object sender, EventArgs e)
        {
            _process.CancelErrorRead();
            _process.CancelOutputRead();
        }

        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            _action?.Invoke(e.Data);
        }

        public void Start()
        {
            try
            {
                _process.StartInfo.Arguments = _arguments;

                _process.Start();
                _process.BeginErrorReadLine();
                _process.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                _action?.Invoke(ex.Message);
            }
        }

        public void Abort()
        {
            try
            {
                _process.Kill();
            }
            catch (Exception ex)
            {
                _action?.Invoke(ex.Message);
            }
            finally
            {
                if (_process != null)
                {
                    _process.Dispose();
                }
            }
        }
    }
}

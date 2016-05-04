using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NCraftDisplay.Data.Engine
{
    public class ConsoleRunner : IDisposable
    {
        private readonly Process _proc;

        private readonly StringBuilder _outBuff;

        private StreamWriter inputBuff;

        public ConsoleRunner(string exePath)
        {
            if (!File.Exists(exePath))
                throw new FileNotFoundException($"Missing or invalid executable path: '{exePath}'.");

            _proc = new Process();
            _proc.StartInfo = new ProcessStartInfo
            {
                FileName = exePath,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            _outBuff = new StringBuilder();
        }

        public string GetOutPut()
        {
            var result = _outBuff.ToString();
            _outBuff.Clear();
            return result;
        }

        public void Start()
        {
            _proc.OutputDataReceived += new DataReceivedEventHandler(MessagePosted);
            _proc.Start();
            inputBuff = _proc.StandardInput;
            _proc.BeginOutputReadLine();
        }

        public void WaitForExit(int timeoutMs)
        {
            _proc.WaitForExit(timeoutMs);
            if (_proc != null)
            {
                _proc.Kill();
                _proc.Dispose();
            }
        }

        public void SendMsg(string msg)
        {
            inputBuff.WriteLine("O");
        }

        public void MessagePosted(object sendingProcess, DataReceivedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                _outBuff.Append(args.Data);
            }
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _proc.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden
        }
    }
}

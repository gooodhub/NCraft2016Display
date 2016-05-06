using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace NCraftDisplay.Data.Engine
{
    public class ConsoleRunner : IDisposable
    {
        private readonly Process _proc;

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
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            _proc.EnableRaisingEvents = false;
        }

        public void StartProgram()
        {
            _proc.Start();
        }

        public IObservable<string> ProgramOutput()
        {
            return ProgramOutput(_proc);
        }

        public void SendMessage(string message)
        {
            if (!_proc.HasExited)
                _proc.StandardInput.WriteLine(message);
        }

        private IObservable<string> ProgramOutput(Process proc)
        {
            return Observable.FromAsync(proc.StandardOutput.ReadLineAsync)
                .Sample(TimeSpan.FromMilliseconds(50))
                .Repeat(100)
                .Publish()
                .RefCount()
                .SubscribeOn(Scheduler.Default);
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!_proc.HasExited)
                        _proc.Kill();

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

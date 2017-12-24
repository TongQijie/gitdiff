using System.IO;
using System.Text;
using System.Diagnostics;
using System;
using Guru.DependencyInjection;
using Guru.Logging.Abstractions;

namespace GitDiff
{
    public class ProcessHelper
    {
        private readonly ILogger _Logger;

        public ProcessHelper(string executable)
        {
            Executable = executable;
            _Logger = DependencyContainer.Resolve<IFileLogger>();
        }

        public ProcessHelper(string executable, string arguments) 
        {
            this.Executable = executable;
            this.Arguments = arguments;
        }

        public string Executable { get; private set; }

        public string Arguments { get; private set; }

        public ProcessHelper Add(string argument)
        {
            Arguments += " " + argument;
            return this;
        }

        public string ReadString(string workingDirectory = null)
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = Executable;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.Arguments = Arguments;
                process.StartInfo.WorkingDirectory = workingDirectory;
                process.Start();

                using (var reader = process.StandardOutput)
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"failed to execute command \"{Executable} {Arguments}\".");
                _Logger.LogEvent(nameof(ProcessHelper), Severity.Error, e, $"failed to execute command \"{Executable} {Arguments}\".");
            }

            return string.Empty;
        }

        public StreamReader ReadStream(string workingDirectory = null)
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = Executable;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.Arguments = Arguments;
                process.StartInfo.WorkingDirectory = workingDirectory;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.Start();

                return process.StandardOutput;
            }
            catch (Exception e)
            {
                Console.WriteLine($"failed to execute command \"{Executable} {Arguments}\".");
                _Logger.LogEvent(nameof(ProcessHelper), Severity.Error, e, $"failed to execute command \"{Executable} {Arguments}\".");
            }

            return null;
        }

        public void Execute(string workingDirectory = null)
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = Executable;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.Arguments = Arguments;
                process.StartInfo.WorkingDirectory = workingDirectory;
                process.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine($"failed to execute command \"{Executable} {Arguments}\".");
                _Logger.LogEvent(nameof(ProcessHelper), Severity.Error, e, $"failed to execute command \"{Executable} {Arguments}\".");
            }
        }
    }
}
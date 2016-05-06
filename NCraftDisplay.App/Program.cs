using System;
using System.IO;
using System.Linq;
using NCraftDisplay.Data;
using NCraftDisplay.Data.Engine;

namespace NCraftDisplay.App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any() && args.Count() != 2)
            {
                Console.WriteLine("Usage: BattleshipRunner.exe <directory> <refreshInMilliseconds>");
                Environment.ExitCode = 1;
                return;
            }

            string workingDir = string.Empty;
            int refreshrateMs = 10;

            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("Argument <directory> is not a valid.");
                Environment.ExitCode = 1;
                return;
            }
            else
            {
                workingDir = args[0];
            }

            if (!int.TryParse(args[1], out refreshrateMs))
            {
                Console.WriteLine("Argument <refreshInMilliseconds> should be a positive integer.");
                Environment.ExitCode = 1;
                return;
            }

            Console.WriteLine("**** Execution started ****");

            var repo = new CsvFileRepository(workingDir);
            var execRepo = new ExecReportRepository(workingDir);
            var runner = new EngineExecutor(workingDir, execRepo);
            var results = runner.Process();

            runner.WriteCsvResults(repo, results);

            Console.Write(results[0].Board);

            Console.WriteLine("**** Execution ended ****");
            Console.ReadLine();
        }
    }
}

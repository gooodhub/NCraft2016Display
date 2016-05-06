using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NCraftDisplay.Model;
using System.Threading;
using System.Threading.Tasks;

namespace NCraftDisplay.Data.Engine
{
    public class EngineExecutor
    {
        public EngineExecutor(string workingDirectory, ExecReportRepository reportRepo)
        {
            if (reportRepo == null)
                throw new ArgumentNullException();

            if (!Directory.Exists(workingDirectory))
                throw new DirectoryNotFoundException();

            _workingDir = workingDirectory;
            _reportRepo = reportRepo;
            _players = new ConcurrentBag<Player>();
        }

        private List<Player> ListPlayers()
        {
            var subDirs = Directory.GetDirectories(_workingDir);
            var players = new List<Player>();
            foreach (var dir in subDirs)
            {
                var exeName = Directory.GetFiles(dir, "*.exe").First();
                var fInfo = new FileInfo(exeName);
                var dirName = fInfo.Directory.Name;

                players.Add(new Player()
                {
                    Name = dirName,
                    ExePath = exeName,
                    Score = 0,
                    Rank = 0
                });
            }

            return players;
        }

        public List<RunResult> Process()
        {
            const TaskContinuationOptions opts = TaskContinuationOptions.OnlyOnRanToCompletion;
            var repo = new ExecReportRepository(_workingDir);

            var cts = new CancellationTokenSource();
            var tasks = new List<Task<RunResult>>();
            foreach (var player in ListPlayers())
            {
                var eng = new BattleshipGame(player.Name, player.ExePath, repo);

                var run = Task.Factory.StartNew(() => eng.PrepareGame())
                    .ContinueWith((parent) => eng.StartProgram(), opts)
                    .ContinueWith((parent) => eng.AttachToProgramOutput(), opts)
                    .ContinueWith<RunResult>((parent) => eng.ProcessRunResults(), opts);
                tasks.Add(run);
            }

            Task.WaitAll(tasks.ToArray());

            var results = tasks.Where(x => x.IsCompleted).Select(x => x.Result).ToList();

            return results;
        }

        public void WriteCsvResults(CsvFileRepository repo, List<RunResult> runs)
        {
            var players = runs.Select((x, idx) => new Player() { Name = x.PlayerName, Score = GetScore(x), Rank = idx })
                .OrderByDescending(x => x.Score)
                .ThenBy(x => x.Name)
                .ToList();

            var scoreBoard = new ScoreBoard()
            {
                BatchDateTime = DateTime.Now,
                Players = players
            };
            repo.Save(scoreBoard);
        }

        private int GetScore(RunResult run)
        {
            return (int)((run.NbrHits / 17.0d)
                * (100 / (run.NbrShots + 1.0d))
                * (1 / (run.RunningTimeMs + 1.0d))
                * 1000000);
        }

        string _workingDir;

        ConcurrentBag<Player> _players;

        ExecReportRepository _reportRepo;
    }
}

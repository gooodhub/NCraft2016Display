using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using NCraftDisplay.Data;
using NCraftDisplay.Model;

namespace NCraftDisplay.App
{
    class Program
    {
        private const int Duration = 10;
        private static ILog logger = LogManager.GetLogger("file");
        private static int _duration;

        static void Main(string[] args)
        {
            var playerDirectories = Directory.GetDirectories(ConfigurationManager.AppSettings["ExeRepository"]);
            var scoreHelper = new ScoreHelper();
            var players = new ConcurrentBag<KeyValuePair<Player, string>>();
            var repo = new CsvFileRepository(ConfigurationManager.AppSettings["CsvFileLocation"]);

            foreach (var playerDirectory in playerDirectories)
            {
                Player player = new Player();
                player.Name = playerDirectory.Replace(Path.GetDirectoryName(playerDirectory), String.Empty).Replace('\\', ' ').Trim();

                var exeFiles = Directory.GetFiles(playerDirectory, "*.exe");
                if (!exeFiles.Any())
                {
                    logger.WarnFormat("Player {0} has no exe file in his/her directory ({1})", player.Name, playerDirectory);
                    continue;
                }
                if (exeFiles.Length > 1)
                {
                    logger.WarnFormat("Player {0} has more than one (i.e. {1}) exe files in its directory ({2})", player.Name, exeFiles.Length, playerDirectory);
                    continue;
                }
                var exeFile = exeFiles.Single();

                players.Add(new KeyValuePair<Player, string>(player, exeFile));
            }
            
            Parallel.ForEach(players, p => p.Key.Score = scoreHelper.Execute(p.Value, Duration));

            repo.Save(players.Select(p => p.Key).ToList());
        }
    }
}

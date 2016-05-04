using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCraftDisplay.Model;
using System.IO;

namespace NCraftDisplay.Data
{
    public class CsvFileRepository : IRepository
    {
        private string fileLocation;

        public CsvFileRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public ScoreBoard GetPreviousScores()
        {
            var files = Directory.GetFiles(fileLocation, "*.csv");

            if (files.Count() < 2) return new ScoreBoard();

            var file = Directory.GetFiles(fileLocation, "*.csv").OrderByDescending(f => f).Skip(1).First();

            var context = new LINQtoCSV.CsvContext();
            var players = context.Read<CSVModel.Player>(file);
            int c = 0;

            var castedPlayers = players.OrderByDescending(p => p.Score)
                .Select(p => new Player() { Name = p.Name, Score = p.Score, Rank = ++c }).ToList();

            return new ScoreBoard()
            {
                Players = castedPlayers
            };
        }

        public void Save(ScoreBoard scoreBoard)
        {
            var context = new LINQtoCSV.CsvContext();
            var playersToWrite = scoreBoard.Players.Select(p => new CSVModel.Player(p));
            context.Write(playersToWrite, Path.Combine(fileLocation, DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".csv"));
        }

        public ScoreBoard GetScores()
        {
            var files = Directory.GetFiles(fileLocation, "*.csv").OrderBy(f => f).ToList();
            if (!files.Any()) return new ScoreBoard();

            var file = files.Last();

            var context = new LINQtoCSV.CsvContext();
            var players = context.Read<CSVModel.Player>(file);
            int c = 0;

            var castedPlayers = players.OrderByDescending(p => p.Score)
                .Select(p => new Player() { Name = p.Name, Score = p.Score, Rank = ++c }).ToList();

            return new ScoreBoard()
            {
                Players = castedPlayers
            };
        }

        public void Save(List<Player> players)
        {
            var score = new ScoreBoard()
            {
                Players = players
            };

            Save(score);
        }
    }
}

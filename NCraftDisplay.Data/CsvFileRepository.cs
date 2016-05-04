using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCraftDisplay.Model;
using System.IO;

namespace NCraftDisplay.Data
{
    public class CsvFileRepository : IRepository
    {
        private const string DateTimeFormat = "yyyyMMdd-HHmmss";
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

            return GetScoreBoard(file);
        }



        public void Save(ScoreBoard scoreBoard)
        {
            var context = new LINQtoCSV.CsvContext();
            var playersToWrite = scoreBoard.Players.Select(p => new CSVModel.Player(p));
            context.Write(playersToWrite, Path.Combine(fileLocation, DateTime.Now.ToString(DateTimeFormat) + ".csv"));
        }

        public ScoreBoard GetScores()
        {
            var files = Directory.GetFiles(fileLocation, "*.csv").OrderBy(f => f).ToList();
            if (!files.Any()) return new ScoreBoard();

            var results = files.Select(GetScoreBoard);

            var playersScore = results.SelectMany(r => r.Players)
                .GroupBy(g => g.Name).Select(g => new { name = g.Key, score = g.Max(f => f.Score) });

            var file = files.Last();

            var lastScoreBoard = GetScoreBoard(file);
            var rank = 0;
            return new ScoreBoard()
            {
                BatchDateTime = lastScoreBoard.BatchDateTime,
                Players =
                    playersScore.OrderByDescending(ps => ps.score)
                        .Select(ps => new Player() {Name = ps.name, Score = ps.score, Rank = ++rank})
                        .ToList()
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

        private static ScoreBoard GetScoreBoard(string file)
        {
            var batchTime = DateTime.ParseExact(Path.GetFileNameWithoutExtension(file), DateTimeFormat, CultureInfo.InvariantCulture);
            var context = new LINQtoCSV.CsvContext();
            var players = context.Read<CSVModel.Player>(file);
            int c = 0;

            var castedPlayers = players.OrderByDescending(p => p.Score)
                .Select(p => new Player() { Name = p.Name, Score = p.Score, Rank = ++c }).ToList();

            return new ScoreBoard()
            {
                Players = castedPlayers,
                BatchDateTime = batchTime
            };
        }
    }
}

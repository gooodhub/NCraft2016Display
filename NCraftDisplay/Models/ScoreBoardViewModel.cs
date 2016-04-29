using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCraftDisplay.Model;

namespace NCraftDisplay.Models
{
    public class ScoreBoardViewModel
    {
        private ScoreBoard scoreBoard;

        public List<PlayerViewModel> Players { get; set; }

        public DateTime LatestBatch { get { return DateTime.Now; } }

        public ScoreBoardViewModel(ScoreBoard scoreBoard)
        {
            this.scoreBoard = scoreBoard;
            var rank = 0;

            if (this.scoreBoard != null && this.scoreBoard.Players != null)
                this.Players = this.scoreBoard.Players.OrderByDescending(p => p.Score).Select(p => new PlayerViewModel(p, ++rank)).Take(10).ToList();
            else
                this.Players = new List<PlayerViewModel>();
        }

        public void SetPrevious(ScoreBoard scorePrevious)
        {
            var previousVm = new ScoreBoardViewModel(scorePrevious);

            var previousPlayersNames = previousVm.Players.Select(p => p.Name).ToList();
            var existingPlayers = this.Players.Where(p => previousPlayersNames.Contains(p.Name)).ToList();

            foreach(var p in existingPlayers)
            {
                var previous = previousVm.Players.Single(pa => pa.Name == p.Name);

                p.Evolution = previous.Rank > p.Rank ? Evolution.UP :
                                previous.Rank < p.Rank ? Evolution.DOWN : Evolution.NOTHING;
            }

            var newPlayers = this.Players.Where(p => !previousPlayersNames.Contains(p.Name));

            foreach (var n in newPlayers)
                n.Evolution = Evolution.NEW;
        }
    }
}
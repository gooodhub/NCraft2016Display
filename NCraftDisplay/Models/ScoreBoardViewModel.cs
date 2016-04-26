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

        public ScoreBoardViewModel(ScoreBoard scoreBoard)
        {
            this.scoreBoard = scoreBoard;
            var rank = 0;

            if (this.scoreBoard != null && this.scoreBoard.Players != null)
                this.Players = this.scoreBoard.Players.OrderByDescending(p => p.Score).Select(p => new PlayerViewModel(p, ++rank)).ToList();
            else
                this.Players = new List<PlayerViewModel>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCraftDisplay.Model
{
    public class ScoreBoard
    {
        public ScoreBoard()
        {
            Players = new List<Player>();
        }

        public List<Player> Players { get; set; }
        public DateTime BatchDateTime { get; set; }
    }
}

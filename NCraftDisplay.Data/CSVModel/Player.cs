using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using NCraftDisplay.Model;

namespace NCraftDisplay.Data.CSVModel
{
    class Player
    {
        private Model.Player p;

        public Player()
        {

        }
        public Player(Model.Player p)
        {
            this.p = p;
            this.Name = p.Name;
            this.Score = p.Score;
        }

        [CsvColumn(CanBeNull=false,Name="Name")]
        public string Name { get; set; }
        [CsvColumn(CanBeNull=false,Name="Score")]
        public int Score { get; set; }
    }
}

using NCraftDisplay.Model;

namespace NCraftDisplay.Models
{
    public enum Evolution
    {
        UP,
        DOWN,
        NEW,
        NOTHING
    }
    public class PlayerViewModel
    {
        private Player p;
        private int rank;

        public string Name { get { return p.Name; } }
        public int Score { get { return p.Score; } }
        public int Rank { get { return this.rank; } }

        public Evolution Evolution { get; set; }
        public string EvolutionClass
        {
            get
            {
                return  Evolution == Evolution.UP ?         "glyphicon glyphicon-arrow-up text-success" :
                        Evolution == Evolution.DOWN ?       "glyphicon glyphicon-arrow-down text-danger" :
                        Evolution == Evolution.NOTHING ?    "glyphicon glyphicon-arrow-right text-warning" :
                                                            "glyphicon glyphicon-share-alt text-primary";
            }
        }

        public PlayerViewModel(Player p)
        {
            this.p = p;
        }

        public PlayerViewModel(Player p, int rank) : this(p)
        {
            this.rank = rank;
        }
    }
}
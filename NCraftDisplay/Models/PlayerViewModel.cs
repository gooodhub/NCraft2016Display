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
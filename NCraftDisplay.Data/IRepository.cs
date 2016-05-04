using System.Collections.Generic;
using NCraftDisplay.Model;

namespace NCraftDisplay.Data
{
    public interface IRepository
    {
        ScoreBoard GetScores();
        ScoreBoard GetPreviousScores();
        void Save(ScoreBoard scoreBoard);
        void Save(List<Player> players);
    }
}

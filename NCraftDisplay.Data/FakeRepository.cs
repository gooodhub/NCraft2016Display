using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCraftDisplay.Model;

namespace NCraftDisplay.Data
{
    public class FakeRepository : IRepository
    {
        ScoreBoard IRepository.GetPreviousScore()
        {
            return GenerateScores(DateTime.Now.Millisecond);
        }

        ScoreBoard IRepository.GetScores()
        {
            return GenerateScores(DateTime.Now.Second);
        }

        private static ScoreBoard GenerateScores(int seed)
        {
            var rnd = new Random(seed);
            var nbPlayers = rnd.Next(3, 20);
            var output = new ScoreBoard();
            output.Players = new List<Player>();

            for (int i = 0; i < nbPlayers; i++)
            {
                output.Players.Add(new Player()
                {
                    Name = "Player #" + i,
                    Score = rnd.Next(0, 10000)
                });
            }

            return output;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipRunner.Tests
{
    class BasicAI
    {
        public static void Main(string[] args)
        {
            IEnumerable<string> arrangement = new List<string>
            {
                ".....####.",
                "..........",
                "...#......",
                "...#......",
                "...#...##.",
                "...#......",
                "...#......",
                "#.........",
                "#.....###.",
                "#........."
            };

            foreach (var line in arrangement)
            {
                Console.WriteLine(line);
            }

            int itr = 0;
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "EXIT" && itr < 100)
            {
                Console.WriteLine(Attack(itr));
                itr++;
            }
        }

        private static string Attack(int itr)
        {
            return RowLabels[itr % 10] + ColLabels[itr / 10];
        }

        private static string[] RowLabels = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        private static string[] ColLabels = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
    }
}

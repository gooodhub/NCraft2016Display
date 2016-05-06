using System;
using System.Collections.Generic;
using System.Threading;

namespace BattleshipRunner.Tests
{
    class BasicAI
    {
        public static void Main(string[] args)
        {
            int itr = 0;
            string input = string.Empty;

            while ((input = Console.ReadLine()) != "EXIT" && itr < 100)
            {
                Console.WriteLine(Attack(itr));
                itr++;
                Thread.Sleep(20);
            }
        }

        private static string Attack(int itr)
        {
            return RowLabels[itr % 10] + ColLabels[itr / 10];
        }

        private static List<string> validMsg = new List<string> { "X", "O", "V" };

        private static string[] RowLabels = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        private static string[] ColLabels = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
    }
}

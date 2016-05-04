using System;

namespace NcraftDisplay.Algos.Random
{
    class Program
    {
        private static string[] alpha = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};

        static void Main(string[] args)
        {
            var l = string.Empty;

            while (l != "EMPTY")
            {
                var randomizer = GetRandom();
                Console.WriteLine(randomizer);

                l = Console.ReadLine();
            }
        }

        private static string GetRandom()
        {
            var random = new System.Random((int) DateTime.Now.Ticks);

            var x = random.Next(0, 9);
            var y = random.Next(0, 9);

            return string.Format("{0}{1}", alpha[x], y);
        }
    }
}

using System;
using System.Threading;

namespace BattleshipRunner.Tests
{
    class MirrorsInput
    {
        static void Main(string[] args)
        {
            string line = string.Empty;
            while ((line = Console.ReadLine()) != "EXIT")
            {
                Console.WriteLine(line);
                Thread.Sleep(250);
            }
        }
    }
}

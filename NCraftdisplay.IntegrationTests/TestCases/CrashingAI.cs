using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipRunner.Tests
{
    class CrashingAi
    {
        static void Main(string[] args)
        {
            throw new StackOverflowException();
        }
    }
}

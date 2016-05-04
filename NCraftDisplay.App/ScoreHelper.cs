using System;
using System.Threading;

namespace NCraftDisplay.App
{
    internal class ScoreHelper
    {
        public ScoreHelper()
        {
            
        }

        public int Execute(string exeFile, int duration)
        {
            var startTime = DateTime.Now;
            int output = 0;

            while ((DateTime.Now - startTime).TotalSeconds < duration)
            {
                output += new Random((int) DateTime.Now.Ticks).Next(0, 100);
            }

            return output;
        }
    }
}
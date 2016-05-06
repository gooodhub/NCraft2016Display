using System;
using System.Collections.Generic;

namespace NCraftDisplay.Data.Engine
{
    public enum GameState
    {
        Starting,
        Running,
        Loss,
        Won
    }

    public class RunResult
    {
        public RunResult()
        {
            Moves = new List<string>();
        }

        public RunResult(DateTime runDate, string playerName, bool isVictorious, string board, List<string> moves, int nbrShots, int nbrHits, long runningTimeMs)
        {
            RunDate = runDate;
            PlayerName = playerName;
            IsVictorious = isVictorious;
            Board = board;
            Moves = moves;
            NbrShots = nbrShots;
            NbrHits = nbrHits;
            RunningTimeMs = runningTimeMs;
        }

        public DateTime RunDate { get; set; }

        public string PlayerName { get; set; }

        public bool IsVictorious { get; set; }

        public string Board { get; set; }

        public List<string> Moves { get; set; }

        public int NbrShots { get; set; }

        public int NbrHits { get; set; }

        public long RunningTimeMs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;

namespace NCraftDisplay.Data.Engine
{
    public class BattleshipGame
    {
        public BattleshipGame(string playerName, string exePath, ExecReportRepository repository)
        {
            _repository = repository;
            _exePath = exePath;
            _gen = new BoardGenerator();
            _board = new char[10, 10];
            _playerName = playerName;
            _moves = new List<string>();
            State = GameState.Starting;
        }

        public GameState State { get; private set; }

        public void PrepareGame()
        {
            _gen.MakeBoard();
            _niceBoard = _gen.PrintBoard();
            for (int itr = 0; itr < 100; itr++)
            {
                _board[itr % 10, itr / 10] = _niceBoard[itr];
            }
        }

        public void StartProgram()
        {
            _runner = new ConsoleRunner(_exePath);
            _runner.StartProgram();
            _runner.SendMessage("START");
            _startDate = DateTime.Now;
        }

        public void AttachToProgramOutput()
        {
            var obs = _runner.ProgramOutput();
            obs.Subscribe(OnLinePrinted, OnError, OnSuccess);
            obs.Wait();

            _runner.SendMessage("EXIT");
        }

        public RunResult ProcessRunResults()
        {
            _runner.Dispose();
            long executigTime = (DateTime.Now - _startDate).Milliseconds;
            State = (_nbrHits == 17)
                ? GameState.Won
                : GameState.Loss;

            var results = new RunResult(_startDate, _playerName, State == GameState.Won, _niceBoard, _moves, _nbrShots, _nbrHits, executigTime);
            var userDir = new FileInfo(_exePath).DirectoryName; // TODO: bof...
            _repository.Save(results, userDir);

            return results;
        }

        private void OnLinePrinted(string line)
        {
            _moves.Add(line);
            Tuple<int, int> coordinates;
            if (TryDecodeCoordinates(line, out coordinates))
            {
                _nbrShots++;
                char outcome = Verify(_board, coordinates.Item1, coordinates.Item2);
                if (outcome == 'X')
                {
                    _nbrHits++;
                }
                _runner.SendMessage(outcome.ToString());
            }

            _runner.SendMessage("O");
        }

        private void OnError(Exception e)
        {
            State = GameState.Loss;
        }

        private void OnSuccess()
        {
            Console.WriteLine("Observer exited");
        }

        public static bool TryDecodeCoordinates(string coordinates, out Tuple<int, int> result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(coordinates) || coordinates.Length > 3)
                return false;

            int col = -1;
            if (!colLables.TryGetValue(coordinates[0], out col))
                return false;

            int row = int.Parse(coordinates.Substring(1)) - 1; // Zero based
            if (row < 0 || row > 9)
                return false;

            result = Tuple.Create(row, col);
            return true;
        }

        public static char Verify(char[,] board, int row, int col)
        {
            if (board[row, col] == '#')
                return 'X';

            if (row > 0 && col > 0 && board[row - 1, col - 1] == '#'
                || col > 0 && board[row, col - 1] == '#'
                || row < 9 && col > 0 && board[row + 1, col - 1] == '#'
                || row > 0 && board[row - 1, col] == '#'
                || row < 9 && board[row + 1, col] == '#'
                || row > 0 && col < 9 && board[row - 1, col + 1] == '#'
                || col < 9 && board[row, col + 1] == '#'
                || row < 9 && col < 9 && board[row + 1, col + 1] == '#')
            {
                return 'V';
            }

            return 'O';
        }

        private static Dictionary<char, int> colLables = new Dictionary<char, int>
        {
            { 'A', 0 },
            { 'B', 1 },
            { 'C', 2 },
            { 'D', 3 },
            { 'E', 4 },
            { 'F', 5 },
            { 'G', 6 },
            { 'H', 7 },
            { 'I', 8 },
            { 'J', 9 },
        };

        BoardGenerator _gen;

        ConsoleRunner _runner;

        ExecReportRepository _repository;

        string _exePath;

        char[,] _board;

        string _niceBoard;

        int _nbrHits;

        int _nbrShots;

        List<string> _moves;

        DateTime _startDate;

        string _playerName;
    }
}

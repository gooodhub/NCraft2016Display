using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCraftDisplay.Data.Engine
{
    enum Orientation
    {
        Hori = 0,
        Vert = 1
    }

    class Cell
    {
        internal int Row { get; set; }

        internal int Col { get; set; }

        internal char Symbol { get; set; }
    }

    public class GameGenerator
    {
        private List<Cell> _board;

        private Random _rng;

        public GameGenerator()
        {
            _board = Enumerable.Range(0, 100)
                .Select(x => new Cell { Row = x / 10, Col = x % 10, Symbol = '.' })
                .ToList();
            _rng = new Random();
        }

        public void MakeBoard()
        {
            PlaceShip((Orientation)_rng.Next(0, 1), 5);
            PlaceShip((Orientation)_rng.Next(0, 1), 4);
            PlaceShip((Orientation)_rng.Next(0, 1), 3);
            PlaceShip((Orientation)_rng.Next(0, 1), 3);
            PlaceShip((Orientation)_rng.Next(0, 1), 2);
        }

        private void PlaceShip(Orientation orient, int shipSize)
        {
            IEnumerable<IGrouping<int, Cell>> selector;
            selector = (orient == Orientation.Hori)
                ? _board.GroupBy(x => x.Row)
                : _board.GroupBy(x => x.Col);

            var availablePlaces = selector.SelectMany(gp => gp.FindConsecutives(x => x.Symbol == '.', shipSize))
                .ToList();

            var selectedPlaces = availablePlaces.OrderBy(x => Guid.NewGuid()).First();
            foreach (var cell in selectedPlaces)
            {
                cell.Symbol = '#';
            }
        }
        

        public string PrintBoard()
        {
            var strB = new StringBuilder();
            for (int itr = 0; itr < _board.Count; itr++)
            {
                strB.Append(_board[itr].Symbol);

                if (itr % 10 == 9)
                    strB.AppendLine();
            }

            return strB.ToString();
        }
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> FindConsecutives<T>(this IEnumerable<T> sequence, Predicate<T> predicate, int sequenceSize)
        {
            IEnumerable<T> window = Enumerable.Empty<T>();
            int count = 0;

            foreach (var item in sequence)
            {
                if (predicate(item))
                {
                    window = window.Concat(Enumerable.Repeat(item, 1));
                    count++;

                    if (count == sequenceSize)
                    {
                        yield return window;
                        window = window.Skip(1);
                        count--;
                    }
                }
                else
                {
                    count = 0;
                    window = Enumerable.Empty<T>();
                }
            }
        }
    }
}

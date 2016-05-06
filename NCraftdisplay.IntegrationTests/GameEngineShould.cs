using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCraftDisplay.Data;
using NCraftDisplay.Data.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NCraftdisplay.IntegrationTests
{
    [TestClass]
    public class GameEngineShould
    {
        [TestMethod]
        public void Tester()
        {
            char[,] board = new char[10, 10]
            {
                { '.', '.', '#', '.', '.', '.', '.', '.', '.', '.' },
                { '.', '.', '#', '.', '.', '.', '.', '.', '.', '.' },
                { '.', '.', '#', '.', '.', '.', '.', '.', '.', '.' },
                { '.', '.', '#', '.', '#', '#', '#', '#', '#', '.' },
                { '.', '.', '#', '.', '.', '.', '.', '.', '#', '.' },
                { '.', '#', '#', '#', '#', '#', '.', '.', '#', '.' },
                { '.', '.', '.', '.', '.', '.', '.', '.', '#', '.' },
                { '.', '.', '.', '#', '#', '#', '#', '#', '#', '.' },
                { '.', '.', '.', '.', '.', '.', '.', '.', '#', '.' },
                { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' }
            };

            char[,] expected = new char[10, 10]
            {
                { 'O', 'V', 'X', 'V', 'O', 'O', 'O', 'O', 'O', 'O' },
                { 'O', 'V', 'X', 'V', 'O', 'O', 'O', 'O', 'O', 'O' },
                { 'O', 'V', 'X', 'V', 'V', 'V', 'V', 'V', 'V', 'V' },
                { 'O', 'V', 'X', 'V', 'X', 'X', 'X', 'X', 'X', 'V' },
                { 'V', 'V', 'X', 'V', 'V', 'V', 'V', 'V', 'X', 'V' },
                { 'V', 'X', 'X', 'X', 'X', 'X', 'V', 'V', 'X', 'V' },
                { 'V', 'V', 'V', 'V', 'V', 'V', 'V', 'V', 'X', 'V' },
                { 'O', 'O', 'V', 'X', 'X', 'X', 'X', 'X', 'X', 'V' },
                { 'O', 'O', 'V', 'V', 'V', 'V', 'V', 'V', 'X', 'V' },
                { 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'V', 'V', 'V' }
            };

            Tuple<int,int> coord;
            BattleshipGame.TryDecodeCoordinates("A10", out coord);
            Assert.AreEqual(Tuple.Create(9, 0), coord);

            for (int row = 0; row < 10; row++)
            for (int col = 0; col < 10; col++)
            {
                Assert.AreEqual(expected[row, col], BattleshipGame.Verify(board, row, col));
            }
        }

    }
}

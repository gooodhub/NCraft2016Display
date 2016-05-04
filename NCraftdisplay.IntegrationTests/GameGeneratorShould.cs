using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCraftDisplay.Data.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCraftdisplay.IntegrationTests
{
    [TestClass]
    public class GameGeneratorShould
    {
        [TestMethod]
        public void Board_is_generated_correctly()
        {
            var gen = new GameGenerator();
            gen.MakeBoard();
            var result = gen.PrintBoard();

            Assert.AreEqual(17, result.Count(x => x == '#'));
        }
    }
}

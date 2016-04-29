using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCraftDisplay.Model;
using System.IO;
using NCraftDisplay.Data;
using System.Linq;

namespace NCraftdisplay.IntegrationTests
{
    [TestClass]
    public class CsvFileRepositoryTests
    {
        private CsvFileRepository repo;

        public string TestFile { get { return @"C:\Users\SetaSensei\Documents"; } }

        [TestCleanup]
        public void CleanUp()
        {
            //File.Delete(TestFile);
        }

        [TestInitialize]
        public void TestInit()
        {
            repo = new CsvFileRepository(TestFile);
        }

        [TestMethod]
        [Ignore]
        public void Save_Should_Write_OntoFile()
        {
            var scoreBoard = new ScoreBoard();

            scoreBoard.Players = new System.Collections.Generic.List<Player>()
            { new Player() { Name= "Test", Score=2000 } };

            repo.Save(scoreBoard);

            var files = Directory.GetFiles(TestFile, "*.csv");
            Assert.IsTrue(files.Any());
        }

        [TestMethod]
        [Ignore]
        public void GetScores_Should_Return_ScoreBoard()
        {
            var scoreBoard = repo.GetScores();

            Assert.IsTrue(scoreBoard.Players.Any());
        }

        [TestMethod]
        [Ignore]
        public void GetPreviousScores_Should_Return_ScoreBoard()
        {
            var scoreBoard = repo.GetPreviousScores();

            Assert.IsTrue(scoreBoard.Players.Any());
        }
    }
}

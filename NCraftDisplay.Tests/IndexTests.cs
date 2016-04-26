using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using NCraftDisplay.Models;
using Moq;
using NCraftDisplay.Data;
using System.Linq;

namespace NCraftDisplay.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void DisplayWebSite_Index_Should_Recover_Nothing_If_No_Players()
        {
            var repository = new Mock<IRepository>();
            repository.Setup(r => r.GetScores()).Returns(new Model.ScoreBoard());

            var website = new Controllers.HomeController(repository.Object);

            var result = website.Index() as ViewResult;

            var ScoreBoard = result.Model as ScoreBoardViewModel;

            Assert.AreEqual(0, ScoreBoard.Players.Count);
        }

        [TestMethod]
        public void DisplayWebSite_Index_Should_Recover_Count_If_Players_Found()
        {
            var score = new Model.ScoreBoard();

            score.Players = new System.Collections.Generic.List<Model.Player>()
            {
                new Model.Player() 
            };

            var repository = new Mock<IRepository>();
            repository.Setup(r => r.GetScores()).Returns(score);

            var website = new Controllers.HomeController(repository.Object);

            var result = website.Index() as ViewResult;

            var ScoreBoard = result.Model as ScoreBoardViewModel;

            Assert.AreEqual(1, ScoreBoard.Players.Count);
        }

        [TestMethod]
        public void ScoreBoardViewModel_Should_Know_The_Players_Evolution()
        {
            var player11 = new Model.Player() { Name = "1", Score = 0 };
            var player12 = new Model.Player() { Name = "1", Score = 50 };

            var player21 = new Model.Player() { Name = "2", Score = 40 };
            var player22 = new Model.Player() { Name = "2", Score = 40 };

            var score = new Model.ScoreBoard();

            score.Players = new System.Collections.Generic.List<Model.Player>()
            {
                player12, player22
            };

            var scorePrevious = new Model.ScoreBoard()
            {
                Players = new System.Collections.Generic.List<Model.Player>()
                {
                    player11, player21
                }
            };

            var vm = new ScoreBoardViewModel(score);

            vm.SetPrevious(scorePrevious);

            var first = vm.Players.Single(p => p.Name == "1");

            Assert.AreEqual(Evolution.UP, first.Evolution);
        }

        [TestMethod]
        public void ScoreBoard_Should_Identify_New_Players()
        {
            var player11 = new Model.Player() { Name = "1", Score = 0 };
            var player12 = new Model.Player() { Name = "1", Score = 50 };

            var player22 = new Model.Player() { Name = "2", Score = 40 };

            var score = new Model.ScoreBoard();

            score.Players = new System.Collections.Generic.List<Model.Player>()
            {
                player12, player22
            };

            var scorePrevious = new Model.ScoreBoard()
            {
                Players = new System.Collections.Generic.List<Model.Player>()
                {
                    player11
                }
            };

            var vm = new ScoreBoardViewModel(score);

            vm.SetPrevious(scorePrevious);

            var first = vm.Players.Single(p => p.Name == "2");

            Assert.AreEqual(Evolution.NEW, first.Evolution);
        }
    }
}

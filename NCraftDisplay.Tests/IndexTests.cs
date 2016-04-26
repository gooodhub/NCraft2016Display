using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using NCraftDisplay.Models;
using Moq;
using NCraftDisplay.Data;

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
    }
}

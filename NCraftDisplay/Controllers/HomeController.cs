using NCraftDisplay.Data;
using NCraftDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NCraftDisplay.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public HomeController() : this(new FakeRepository()) {   }

        public ViewResult Index()
        {
            var previousScores = this.repository.GetPreviousScore();
            var currentScore = new ScoreBoardViewModel(this.repository.GetScores());
            currentScore.SetPrevious(previousScores);

            return View(currentScore);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
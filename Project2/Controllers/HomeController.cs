using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project2.DAL;
using Project2.Models;

namespace Project2.Controllers
{
    public class HomeController : Controller
    {
        private Project2Context db = new Project2Context();
        public ActionResult Index()
        {
            return View();
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

        //Controller ActionResult(gets question data in preparation for editing question answer)

        public ActionResult EditQuestionAnswer(int questionID, int missionID)
        {
            ViewBag.QuestionInfo = db.Database.SqlQuery<MissionQuestions>(
            "SELECT * FROM MissionQuestions WHERE MissionQuestioNID = " + questionID).SingleOrDefault();

            ViewBag.MissionID = missionID;

            return View();
        }

        //Controller ActionResult(submits new question answer and redirects back to the mission page)

[HttpPost]
        public ActionResult EditQuestionAnswer(MissionQuestions model)
        {
            db.Database.ExecuteSqlCommand(
            "UPDATE MissionQuestions " +
            "SET Answer = '" + model.Answer +
            "' WHERE MissionQuestionID = " + model.MissionQuestionID);

            Missions redirectMission = new Missions();

            redirectMission.MissionID = model.MissionID;

            return RedirectToAction("RedirectToMission", new { missionID = model.MissionID });
        }
    }
}
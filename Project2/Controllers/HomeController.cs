﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project2.DAL;
using Project2.Models;
using Microsoft.AspNet.Identity;

namespace Project2.Controllers
{
    public class HomeController : Controller
    {
        private Project2Context db = new Project2Context();
        //public ActionResult Index()
        //{
        //    return View();
        //}

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

        [HttpPost]

        [Authorize]
        public ActionResult SelectMission(Missions model)
        {
            var id = model.MissionID;

            IEnumerable<Missions> missionData = db.Database.SqlQuery<Missions>(
                "SELECT * FROM Missions WHERE MissionID = " + model.MissionID).ToList();

            ViewBag.MissionData = missionData;

            IEnumerable<MissionQuestions> missionQuestions = db.Database.SqlQuery<MissionQuestions>(
                "SELECT * FROM MissionQuestions WHERE MissionID = " + model.MissionID).ToList();

            ViewBag.MissionQuestions = missionQuestions;

            ViewBag.UserID = User.Identity.GetUserId();
            ViewBag.UserEmail = User.Identity.GetUserName();

            return View();
        }
        // POST: MissionQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MissionQuestionID,MissionID,UserID,Question,Answer")] MissionQuestions missionQuestions)
        {
            if (ModelState.IsValid)
            {
                db.MissionQuestions.Add(missionQuestions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(missionQuestions);
        }

        // GET: FAQ
        [Authorize]
        public ActionResult FAQ()
        {
            return View(db.MissionQuestions.ToList());
        }

        // GET: MissionQuestions
        public ActionResult Index()
        {
            return View(db.MissionQuestions.ToList());
        }

        // GET: MissionQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissionQuestions missionQuestions = db.MissionQuestions.Find(id);
            if (missionQuestions == null)
            {
                return HttpNotFound();
            }
            return View(missionQuestions);
        }
    }
}
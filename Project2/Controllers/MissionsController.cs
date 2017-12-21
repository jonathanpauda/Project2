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
using Microsoft.AspNet.Identity;

namespace Project2.Controllers
{
    [RequireHttps]
    public class MissionsController : Controller
    {
        private Project2Context db = new Project2Context();

        // GET: Missions
        public ActionResult Index()
        {
            return View(db.Missions.ToList());
        }

        //[HttpPost]
        //public ActionResult MissionInfo(FormCollection form)
        //{
        //    //Variable for mission ID
        //    int MissionID;
        //    //Determines if mission ID is a valid number
        //    bool validMissionID = Int32.TryParse(form["MissionID"], out MissionID);

        //    //Set ViewBag data if the MissionID is a valid number
        //    if (validMissionID && (MissionID == 1 || MissionID == 2 || MissionID == 3))
        //    {
        //        //Brazil Ribeirao Preto Mission
        //        if (MissionID == 1)
        //        {
        //            ViewBag.MissionName = "Brazil Ribeirao Preto";
        //            ViewBag.MissionPresident = "Peter Siegfried Scholz";
        //            ViewBag.MissionAddress = "Rua Sao Sebastiao 1003, Centro, 14015-040 Ribeirao Preto, SP, Brazil";
        //            ViewBag.Language = "Portuguese";
        //            ViewBag.Climate = "Tropical Wet and Dry";
        //            ViewBag.DominateReligion = "Roman Catholic";
        //            ViewBag.FlagImgFileName = "Brazil_Flag.png";
        //        }
        //        //Raleigh North Carolina Mission
        //        else if (MissionID == 2)
        //        {
        //            ViewBag.MissionName = "Raleigh North Carolina";
        //            ViewBag.MissionPresident = "Dennis Roland James";
        //            ViewBag.MissionAddress = "5060 Six Forks Rd, Raleigh, NC 27609";
        //            ViewBag.Language = "English";
        //            ViewBag.Climate = "Humid Subtropical";
        //            ViewBag.DominateReligion = "Protestant Christian";
        //            ViewBag.FlagImgFileName = "USA_Flag.png";
        //        }
        //        //Japan Fukuoka Mission
        //        else if (MissionID == 3)
        //        {
        //            ViewBag.MissionName = "Japan Fukuoka";
        //            ViewBag.MissionPresident = "Bradley C Egan";
        //            ViewBag.MissionAddress = "9-16 Hirao-josuimachi, Chuo-ku, Fukuoka-shi, Fukuoka, 810-0029";
        //            ViewBag.Language = "Japanese";
        //            ViewBag.Climate = "Humid Subtropical";
        //            ViewBag.DominateReligion = "Shinto and Buddhism";
        //            ViewBag.FlagImgFileName = "Japan_Flag.png";
        //        }
        //        //Return view with ViewBag data
        //        return View("MissionInfo");
        //    }
        //    else
        //    {
        //        //Return string stating there was an error
        //        return Content("Error finding data for mission ID " + form["MissionID"]);
        //    }

        //}

        public ActionResult Missions()
        {
            ViewBag.Missions = db.Missions.ToList();

            return View();
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

        // GET: Missions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Missions missions = db.Missions.Find(id);
            if (missions == null)
            {
                return HttpNotFound();
            }
            return View(missions);
        }

        // GET: Missions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Missions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MissionID,MissionName,MissionPresident,MissionAddress,MissionLanguage,MissionClimate")] Missions missions)
        {
            if (ModelState.IsValid)
            {
                db.Missions.Add(missions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(missions);
        }

        // GET: Missions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Missions missions = db.Missions.Find(id);
            if (missions == null)
            {
                return HttpNotFound();
            }
            return View(missions);
        }

        // POST: Missions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MissionID,MissionName,MissionPresident,MissionAddress,MissionLanguage,MissionClimate")] Missions missions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(missions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(missions);
        }

        // GET: Missions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Missions missions = db.Missions.Find(id);
            if (missions == null)
            {
                return HttpNotFound();
            }
            return View(missions);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Missions missions = db.Missions.Find(id);
            db.Missions.Remove(missions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

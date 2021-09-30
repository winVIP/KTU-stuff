using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class ReviewController : Controller
    {
        private AutonuomaDbContext db = new AutonuomaDbContext();
        // GET: Review
        [Authorize(Roles = "Admin")]
        public ActionResult ReviewList()
        {
            return View(db.Reviews.Include("Request"));
        }

        public ActionResult ReviewAccept(int id)
        {
            db.Reviews.Find(id).IsApproved = true;
            db.SaveChanges();
            return RedirectToAction("ReviewList");
        }
        public ActionResult ReviewDeny(int id)
        {
            db.Reviews.Find(id).IsApproved = false;
            db.SaveChanges();
            return RedirectToAction("ReviewList");
        }

        // GET: Review/ReviewCheck/5

        [Authorize(Roles = "Admin")]
        public ActionResult ReviewCheck(int id)
        {
            return View(db.Reviews.Include("Request").Where(b => b.Id == id).First());
        }

        // GET: Review/Create
        [Authorize]
        [HttpGet]
        public ActionResult ReviewCreate()
        {
            Review review = new Review();
            return View(review);
        }

        // POST: Review/Create
        [HttpPost]
        [Authorize(Roles = "Member")]
        [ValidateAntiForgeryToken]
        public ActionResult ReviewCreate(Review review, int requestID)
        {
            try
            {

                //review.RequestID = requestID;
                db.Reviews.Add(review);
                db.SaveChanges();

                return RedirectToAction("ReviewList");
            }
            catch
            {
                return View();
            }
        }

    }
}

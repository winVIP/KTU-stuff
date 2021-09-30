using AutoNuoma.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;


namespace AutoNuoma.Controllers
{
    public class RentPointController : Controller
    {
        private AutonuomaDbContext db = new AutonuomaDbContext();
        // GET: RentPoint
        [AllowAnonymous]
        public ActionResult RentPointList()
        {
            return View(db.RentPoints);
        }

        // GET: RentPoint/Create
        [Authorize(Roles = "Admin")]
        public ActionResult RentPointCreate()
        {
            return View();
        }

        // POST: RentPoint/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult RentPointCreate(RentPoint rentPoint)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Laukai privalo buti uzpildyti.");
                    return View(rentPoint);
                }
                db.RentPoints.Add(rentPoint);
                db.SaveChanges();
                return RedirectToAction("RentPointList");
            }
            catch
            {
                return View();
            }
        }

        // GET: RentPoint/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult RentPointDelete(int id)//int id
        {
            db.RentPoints.Include("Cars");
            var rentpoint = db.RentPoints.Find(id);
            if(rentpoint != null)
            {
                db.Cars.RemoveRange(rentpoint.Cars);
                db.RentPoints.Include("Cars");
                db.RentPoints.Remove(rentpoint);
                db.SaveChanges();
            }
            return RedirectToAction("RentPointList");
        }
    }
}

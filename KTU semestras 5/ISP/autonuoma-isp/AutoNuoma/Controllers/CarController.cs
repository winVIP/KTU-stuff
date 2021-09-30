using AutoNuoma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoNuoma.Controllers
{
    public class CarController : Controller
    {
        // GET: Car/CarList
        // Automobilių sąrašas
        [AllowAnonymous]
        public ActionResult CarList()
        {
            Models.AutonuomaDbContext db = new Models.AutonuomaDbContext();
            return View(db.Cars);
        }

        // GET: Car/CarDetails/5
        [AllowAnonymous]
        public ActionResult CarDetails(int id)//int id
        {
            var db = new AutonuomaDbContext();
            var car = db.Cars.Find(id);
            return View(car);
        }

        // GET: Car/Create
        [Authorize(Roles = "Admin")]
        public ActionResult CarCreate()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("CarList");
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/CarEdit/5
        [Authorize(Roles = "Admin")]
        public ActionResult CarEdit()
        {
            return View();
        }

        // POST: Car/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("CarDetails");
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult CarDelete()
        {
            return RedirectToAction("CarList");
        }

        // POST: Car/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("CarList");
            }
            catch
            {
                return View();
            }
        }
    }
}

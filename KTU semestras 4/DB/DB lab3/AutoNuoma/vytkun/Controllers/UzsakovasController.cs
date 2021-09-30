using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vytkun.Repos;
using vytkun.ViewModels;

namespace vytkun.Controllers
{
    public class UzsakovasController : Controller
    {
        UzsakovasRepository uzsakovasRepository = new UzsakovasRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(uzsakovasRepository.getUzsakovai());
        }

        public ActionResult Create()
        {
            UzsakovasEditViewModel uzsakovasEditViewModel = new UzsakovasEditViewModel();
            //Užpildomi pasirinkimų sąrašai duomenimis iš duomenų saugyklų
            return View(uzsakovasEditViewModel);
        }

        [HttpPost]
        public ActionResult Create(UzsakovasEditViewModel collection)
        {
            try
            {
                //Pridedamas naujas automobilis
                uzsakovasRepository.addUzsakovas(collection);

                //Nukreipia i sąrašą
                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        public ActionResult Edit(int id)
        {
            //Surenkama automobilio informacija iš duomenų bazės
            UzsakovasEditViewModel uzsakovasEditViewModel = uzsakovasRepository.getUzsakovas(id);
            return View(uzsakovasEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, UzsakovasEditViewModel collection)
        {
            try
            {
                // Atnaujinama automobilio informacija
                uzsakovasRepository.updateUzsakovas(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        public ActionResult Delete(int id)
        {
            UzsakovasEditViewModel uzsakovasEditViewModel = uzsakovasRepository.getUzsakovas(id);
            return View(uzsakovasEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                if (uzsakovasRepository.getUzsakovasProjektaiCount(id) > 0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Uzsakovas vis dar turi projekta";
                    return View(uzsakovasRepository.getUzsakovas(id));
                }

                if (uzsakovasRepository.getUzsakovasLesosCount(id) > 0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Uzsakovas vis dar turi pervedamu lesu";
                    return View(uzsakovasRepository.getUzsakovas(id));
                }

                if (!naudojama)
                {
                    uzsakovasRepository.deleteUzsakovas(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
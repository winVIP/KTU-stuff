using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vytkun.Repos;
using vytkun.ViewModels;

namespace vytkun.Controllers
{
    public class RemejasController : Controller
    {
        RemejasRepository remejasRepository = new RemejasRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(remejasRepository.getRemejai());
        }

        public ActionResult Create()
        {
            DaugRemeju daugRemeju = new DaugRemeju();

            return View(daugRemeju);
        }

        [HttpPost]
        public ActionResult Create(DaugRemeju collection)
        {
            try
            {
                foreach (RemejasEditViewModel item in collection.remejai)
                {
                    remejasRepository.addRemejas(item);
                }
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
            RemejasEditViewModel remejasEditViewModel = remejasRepository.getRemejas(id);
            //Užpildomi pasirinkimų sąrašai
            return View(remejasEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, RemejasEditViewModel collection)
        {
            try
            {
                // Atnaujinama automobilio informacija
                remejasRepository.updateRemejas(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        public ActionResult Delete(int id)
        {
            RemejasEditViewModel remejasEditViewModel = remejasRepository.getRemejas(id);
            return View(remejasEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                remejasRepository.deleteRemejas(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
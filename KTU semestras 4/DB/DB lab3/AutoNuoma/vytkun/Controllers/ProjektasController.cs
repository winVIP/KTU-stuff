using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vytkun.Repos;
using vytkun.ViewModels;
using System.Diagnostics;

namespace vytkun.Controllers
{
    public class ProjektasController : Controller
    {
        ProjektasRepository projektasRepository = new ProjektasRepository();
        UzsakovasRepository uzsakovasRepository = new UzsakovasRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(projektasRepository.getProjects());
        }

        public ActionResult Create()
        {
            ProjektasEditViewModel projektasEditViewModel = new ProjektasEditViewModel();
            //Užpildomi pasirinkimų sąrašai duomenimis iš duomenų saugyklų
            PopulateSelections(projektasEditViewModel);
            return View(projektasEditViewModel);
        }

        [HttpPost]
        public ActionResult Create(ProjektasEditViewModel collection)
        {
            try
            {
                //Pridedamas naujas automobilis
                projektasRepository.addProject(collection);

                //Nukreipia i sąrašą
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public ActionResult Edit(int id)
        {
            //Surenkama automobilio informacija iš duomenų bazės
            ProjektasEditViewModel projektasEditViewModel = projektasRepository.getProject(id);
            //Užpildomi pasirinkimų sąrašai
            PopulateSelections(projektasEditViewModel);
            return View(projektasEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, ProjektasEditViewModel collection)
        {
            try
            {
                // Atnaujinama automobilio informacija
                projektasRepository.updateProject(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public ActionResult Delete(int id)
        {
            ProjektasEditViewModel projektasEditViewModel = projektasRepository.getProject(id);
            return View(projektasEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                if (projektasRepository.getProjektasDalyviaiCount(id) > 0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Projekte yra dalyviu, negalima pasalinti";
                    return View(projektasRepository.getProject(id));
                }

                if (projektasRepository.getProjektasGrafikaiCount(id) > 0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Projektas yra susietas su grafiku, negalima pasalinti";
                    return View(projektasRepository.getProject(id));
                }

                if (!naudojama)
                {
                    projektasRepository.deleteProject(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(ProjektasEditViewModel projektasEditViewModel)
        {
            var uzsakovai = uzsakovasRepository.getUzsakovai();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var item in uzsakovai)
            {
                selectListItems.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.vardas + " " + item.pavarde });
            }

            //Sarašai priskiriami vaizdo objektui
            projektasEditViewModel.UzsakovaiList = selectListItems;
        }
    }
}
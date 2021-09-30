using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vytkun.Repos;
using vytkun.ViewModels;

namespace vytkun.Controllers
{
    public class GrafikasController : Controller
    {
        GrafikasRepository grafikasRepository = new GrafikasRepository();
        ProjektasRepository projektasRepository = new ProjektasRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(grafikasRepository.getGrafikai());
        }

        public ActionResult Create()
        {
            DaugGrafiku daugGrafiku = new DaugGrafiku();
            //Užpildomi pasirinkimų sąrašai duomenimis iš duomenų saugyklų
            PopulateSelections(daugGrafiku);
            return View(daugGrafiku);
        }

        [HttpPost]
        public ActionResult Create(DaugGrafiku collection)
        {
            try
            {
                foreach (GrafikasEditViewModel item in collection.grafikai)
                {
                    grafikasRepository.addGrafikas(item);
                }
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
            GrafikasEditViewModel grafikasEditViewModel = grafikasRepository.getGrafikas(id);
            //Užpildomi pasirinkimų sąrašai
            return View(grafikasEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, GrafikasEditViewModel collection)
        {
            try
            {
                // Atnaujinama automobilio informacija
                grafikasRepository.updateGrafikas(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        public ActionResult Delete(int id)
        {
            GrafikasEditViewModel grafikasEditViewModel = grafikasRepository.getGrafikas(id);
            return View(grafikasEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                if (grafikasRepository.getGrafikasUzduotysCount(id) > 0)
                {
                    naudojama = true;
                    System.Diagnostics.Debug.WriteLine(grafikasRepository.getGrafikasUzduotysCount(id));
                    ViewBag.naudojama = "Grafika naudoja uzduotis";
                    return View(grafikasRepository.getGrafikas(id));
                }

                if (!naudojama)
                {
                    grafikasRepository.deleteGrafikas(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(DaugGrafiku daugGrafiku)
        {
            var projektai = projektasRepository.getProjects();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var item in projektai)
            {
                selectListItems.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            //Sarašai priskiriami vaizdo objektui
            daugGrafiku.projektaiList = selectListItems;
        }
    }
}
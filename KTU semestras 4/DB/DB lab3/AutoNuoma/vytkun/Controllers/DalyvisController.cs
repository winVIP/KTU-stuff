using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vytkun.Repos;
using vytkun.ViewModels;

namespace vytkun.Controllers
{
    public class DalyvisController : Controller
    {
        DalyvisRepository dalyvisRepository = new DalyvisRepository();
        ProjektasRepository projektasRepository = new ProjektasRepository();
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(dalyvisRepository.getDalyviai());
        }

        public ActionResult Create()
        {
            DaugDalyviu daugDalyviu = new DaugDalyviu();
            PopulateSelections(daugDalyviu);


            return View(daugDalyviu);
        }

        [HttpPost]
        public ActionResult Create(DaugDalyviu collection)
        {
            try
            {
                foreach(DalyvisEditViewModel item in collection.dalyviai)
                {
                    dalyvisRepository.addDalyvis(item);
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
            DalyvisEditViewModel dalyvisEditViewModel = dalyvisRepository.getDalyvis(id);
            //Užpildomi pasirinkimų sąrašai
            return View(dalyvisEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, DalyvisEditViewModel collection)
        {
            try
            {
                // Atnaujinama automobilio informacija
                dalyvisRepository.updateDalyvis(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        public ActionResult Delete(int id)
        {
            DalyvisEditViewModel dalyvisEditViewModel = dalyvisRepository.getDalyvis(id);
            return View(dalyvisEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                dalyvisRepository.deleteDalyvis(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(DaugDalyviu daugDalyviu)
        {
            var projektai = projektasRepository.getProjects();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var item in projektai)
            {
                selectListItems.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.pavadinimas });
            }

            //Sarašai priskiriami vaizdo objektui
            daugDalyviu.projektaiList = selectListItems;
        }
    }
}
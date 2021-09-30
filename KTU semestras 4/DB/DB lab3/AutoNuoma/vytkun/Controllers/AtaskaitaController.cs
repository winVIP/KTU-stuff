using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vytkun.Repos;
using vytkun.ViewModels;
using System.Web.WebPages;

namespace vytkun.Controllers
{
    public class AtaskaitaController : Controller
    {
        AtaskaituRepository ataskaituRepository = new AtaskaituRepository();

        public ActionResult Index(DateTime? nuo, DateTime? iki, string pavadinimas)
        {
            //Sukuriamas ataskaitos vaizdo objektas ir užpildoma duomenimis
            AtaskaitaViewModel ataskaita = new AtaskaitaViewModel();
            ataskaita.nuo = nuo == null ? null : nuo;
            ataskaita.iki = iki == null ? null : iki;
            ataskaita.pavadinimas = pavadinimas == null ? null : pavadinimas;
            ataskaita.pavadinimas = pavadinimas.IsEmpty() ? null : pavadinimas;
            ataskaita.darbuotojai = ataskaituRepository.getDarbuotojai(ataskaita.nuo, ataskaita.iki, ataskaita.pavadinimas);
            //Suskaiciuojama bendra suma visų sutarčių
            foreach (var item in ataskaita.darbuotojai)
            {
                ataskaita.sumaDarbuotoju++;
            }

            return View(ataskaita);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vytkun.Repos;
using vytkun.ViewModels;

namespace vytkun.Controllers
{
    public class BiudzetasController : Controller
    {
        BiudzetasRepository biudzetasRepository = new BiudzetasRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(biudzetasRepository.getBudgets());
        }
    }
}
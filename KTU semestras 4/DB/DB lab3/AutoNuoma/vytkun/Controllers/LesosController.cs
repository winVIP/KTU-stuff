using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vytkun.Repos;
using vytkun.ViewModels;

namespace vytkun.Controllers
{
    public class LesosController : Controller
    {
        LesosRepository lesosRepository = new LesosRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(lesosRepository.getLesos());
        }
    }
}
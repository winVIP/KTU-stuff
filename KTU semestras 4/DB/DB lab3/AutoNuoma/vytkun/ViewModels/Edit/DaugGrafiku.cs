using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vytkun.ViewModels
{
    public class DaugGrafiku
    {
        public virtual List<GrafikasEditViewModel> grafikai { get; set; }
        public IList<SelectListItem> projektaiList { get; set; }
    }
}
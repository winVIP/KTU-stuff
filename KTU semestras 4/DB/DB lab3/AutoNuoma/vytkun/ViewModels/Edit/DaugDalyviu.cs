using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vytkun.ViewModels
{
    public class DaugDalyviu
    {
        public virtual List<DalyvisEditViewModel> dalyviai { get; set; }
        public IList<SelectListItem> projektaiList { get; set; }
    }
}
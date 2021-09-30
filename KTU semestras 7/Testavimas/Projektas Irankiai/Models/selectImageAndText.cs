using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projektas_Irankiai.Models
{
    public class selectImageAndText
    {
        [Display(Name = "Pasirinkti Paveiksleli")]
        public string SelectedItemId { get; set; }
        [Display(Name = "Parasyti norima teksta paveiksleliui")]
        public string text { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projektas_Irankiai.Models
{
    public class NewTradeOffer
    {
        [Display(Name = "Pasirinkti Naudotoja")]
        public int SelectedUserId { get; set; }
        [Display(Name = "Parasyti norima zinute")]
        public string text { get; set; }
        [Display(Name = "Pasirinkti Paveiksleli")]
        public int SelectedImageId { get; set; }
        public List<SelectListItem> users { get; set; }
        public List<SelectListItem> images { get; set; }
    }
}
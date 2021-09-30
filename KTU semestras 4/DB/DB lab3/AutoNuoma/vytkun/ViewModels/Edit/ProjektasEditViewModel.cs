using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vytkun.ViewModels
{
    public class ProjektasEditViewModel
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }

        [DisplayName("Pavadinimas")]
        [Required]
        public string pavadinimas { get; set; }

        [DisplayName("Nuo kada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime nuo_kada { get; set; }

        [DisplayName("Iki kada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime iki_kada { get; set; }

        [DisplayName("Uzsakovas")]
        [Required]
        public int fk_uzsakovas { get; set; }

        //Sąrašai skirti pasirinkimams 
        public IList<SelectListItem> UzsakovaiList { get; set; }
    }
}
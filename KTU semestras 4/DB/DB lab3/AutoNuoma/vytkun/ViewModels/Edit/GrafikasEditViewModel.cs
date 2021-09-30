using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class GrafikasEditViewModel
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

        [DisplayName("Projektas")]
        [Required]
        public int fk_projektas { get; set; }
    }
}
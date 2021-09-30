using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vytkun.ViewModels
{
    public class DalyvisEditViewModel
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }
        [DisplayName("Vardas")]
        [Required]
        public string vardas { get; set; }
        [DisplayName("Pavarde")]
        [Required]
        public string pavarde { get; set; }
        [DisplayName("Telefonas")]
        [Required]
        public string telefonas { get; set; }
        [DisplayName("El. Pastas")]
        [Required]
        public string el_pastas { get; set; }

        [DisplayName("Projektas")]
        [Required]
        public int fk_projektas { get; set; }
    }
}
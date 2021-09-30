using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class UzsakovasEditViewModel
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
        [DisplayName("Kompanija")]
        [Required]
        public string kompanija { get; set; }
        [DisplayName("El. Pastas")]
        [Required]
        public string el_pastas { get; set; }
        [DisplayName("Adresas")]
        [Required]
        public string adresas { get; set; }

        
    }
}
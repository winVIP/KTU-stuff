using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class DarbuotojasEditViewModel
    {
        [DisplayName("ID")]
        [MaxLength(8)]
        [Required]
        public int id { get; set; }
        [DisplayName("Vardas")]
        [MaxLength(255)]
        [Required]
        public string vardas { get; set; }
        [DisplayName("Pavarde")]
        [MaxLength(255)]
        [Required]
        public string pavarde { get; set; }
        [DisplayName("Telefonas")]
        [MaxLength(255)]
        [Required]
        public string telefonas { get; set; }
        [DisplayName("El. Pastas")]
        [MaxLength(255)]
        [Required]
        public string el_pastas { get; set; }
        [DisplayName("Adresas")]
        [MaxLength(255)]
        [Required]
        public string adresas { get; set; }
        [DisplayName("Uzduotis")]
        [MaxLength(8)]
        [Required]
        public int fk_uzduotis { get; set; }
        [DisplayName("Role")]
        [MaxLength(8)]
        [Required]
        public int fk_role { get; set; }
    }
}
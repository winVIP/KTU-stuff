using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class DarbuotojasListViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Vardas")]
        public string vardas { get; set; }
        [DisplayName("Pavarde")]
        public string pavarde { get; set; }
        [DisplayName("Telefonas")]
        public string telefonas { get; set; }
        [DisplayName("El. Pastas")]
        public string el_pastas { get; set; }
        [DisplayName("Adresas")]
        public string adresas { get; set; }
        [DisplayName("Role")]
        public string role { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class AtaskaitaDarbuotojaiViewModel
    {
        [DisplayName("Projekto pavadinimas")]
        public string projektoPavadinimas { get; set; }

        [DisplayName("Vardas")]
        public string Vardas { get; set; }

        [DisplayName("Pavarde")]
        public string Pavarde { get; set; }

        [DisplayName("Uzduoties pavadinimas")]
        public string uzdPavadinimas { get; set; }

        [DisplayName("Nuo kada")]
        public DateTime nuo { get; set; }

        [DisplayName("Iki kada")]
        public DateTime iki { get; set; }

        [DisplayName("Darbuotoju kiekis:")]
        public int sumaDarbuotoju { get; set; }

        [DisplayName("Projektu kiekis:")]
        public int sumaProjektu { get; set; }

        [DisplayName("Maziausia data:")]
        public DateTime minDate { get; set; }

        [DisplayName("Didziausia data:")]
        public DateTime maxDate { get; set; }
    }
}
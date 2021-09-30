using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vytkun.ViewModels
{
    public class AtaskaitaViewModel
    {
        public List<AtaskaitaDarbuotojaiViewModel> darbuotojai { get; set; }
        public int sumaDarbuotoju { get; set; }

        public int sumaProjektu { get; set; }

        [DisplayName("Projekto pavadinimas")]
        public string pavadinimas { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ?nuo { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ?iki { get; set; }
    }
}
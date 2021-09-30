using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class UzduotisListViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Pavadinimas")]
        public string pavadinimas { get; set; }

        [DisplayName("Nuo kada")]
        public DateTime nuo_kada { get; set; }

        [DisplayName("Iki kada")]
        public DateTime iki_kada { get; set; }
    }
}
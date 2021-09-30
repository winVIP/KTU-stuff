using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class BiudzetasListViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Esamas Biudzetas")]
        public double esamas_biudzetas { get; set; }

        [DisplayName("Projektas")]
        public string projektas { get; set; }
    }
}
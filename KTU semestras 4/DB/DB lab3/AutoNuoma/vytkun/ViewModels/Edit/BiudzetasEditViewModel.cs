using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vytkun.ViewModels
{
    public class BiudzetasEditViewModel
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }

        [DisplayName("Esamas Biudzetas")]
        [Required]
        public double esamas_biudzetas { get; set; }
        [DisplayName("Uzsakovo skiriamos lesos")]
        [Required]
        public double lesos { get; set; }
        [DisplayName("Uzsakovas")]
        [Required]
        public int projektas { get; set; }

        public IList<SelectListItem> ProjektaiList { get; set; }
    }
}
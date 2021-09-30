using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class UzduotisEditViewModel
    {
        [DisplayName("ID")]
        [MaxLength(8)]
        [Required]
        public int id { get; set; }

        [DisplayName("Pavadinimas")]
        [MaxLength(255)]
        [Required]
        public string pavadinimas { get; set; }

        [DisplayName("Grafikas")]
        [MaxLength(8)]
        [Required]
        public int fk_grafikas { get; set; }
    }
}
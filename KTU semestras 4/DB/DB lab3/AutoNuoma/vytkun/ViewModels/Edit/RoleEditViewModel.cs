using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace vytkun.ViewModels
{
    public class RoleEditViewModel
    {
        [DisplayName("ID")]
        [MaxLength(8)]
        [Required]
        public int id { get; set; }

        [DisplayName("Pavadinimas")]
        [MaxLength(255)]
        [Required]
        public string pavadinimas { get; set; }

        [DisplayName("Uzduotis")]
        [MaxLength(255)]
        [Required]
        public string uzduotis { get; set; }

        [DisplayName("Nuo kada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime nuo_kada { get; set; }

        [DisplayName("Iki kada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime iki_kada { get; set; }
    }
}
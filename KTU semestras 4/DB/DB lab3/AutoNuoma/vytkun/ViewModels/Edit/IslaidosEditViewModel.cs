using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class IslaidosEditViewModel
    {
        [DisplayName("ID")]
        [MaxLength(8)]
        [Required]
        public int id { get; set; }

        [DisplayName("Suma")]
        [Required]
        public double suma { get; set; }

        [DisplayName("Paskirtis")]
        [MaxLength(255)]
        [Required]
        public string paskirtis { get; set; }

        [DisplayName("Data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime data { get; set; }

        [DisplayName("Biudzetas")]
        [MaxLength(8)]
        [Required]
        public int fk_biudzetas { get; set; }

        [DisplayName("Uzduotis")]
        [MaxLength(8)]
        [Required]
        public int fk_uzduotis { get; set; }
    }
}
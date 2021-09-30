using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    [Table("RentPoints")]
    public class RentPoint
    {
        public int Id { get; set; }

        [Display(Name = "Adresas")]
        public string Address { get; set; }

        [Display(Name = "Šalis")]
        public string Country { get; set; }

        [Display(Name = "Pavadinimas")]
        public string Name { get; set; }

        [Display(Name = "Telefono numeris")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ilguma")]
        public float Longitude { get; set; }

        [Display(Name = "Platuma")]
        public float Latitude { get; set; }

        [Display(Name = "Darbo pradžia")]
        public string BeginningOfWork { get; set; }

        [Display(Name = "Darbo pabaiga")]
        public string EndingOfWork { get; set; }

        [Display(Name = "Darbo dienos")]
        public string WorkingDays { get; set; }

        public virtual List<Car> Cars { get; set; }

    }
}
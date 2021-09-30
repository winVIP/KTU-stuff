using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    [Table("Cars")]
    public class Car
    {
        public int Id { get; set; }

        [Display(Name = "Nuomos kaina")]
        public double RentCost { get; set; }

        [Display(Name = "Gamintojas")]
        public string Manufacturer { get; set; }

        [Display(Name = "Modelis")]
        public string Model { get; set; }

        [Display(Name = "Registracijos numeris")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Įregistravimo data")]
        public DateTime FirstRegistration { get; set; }

        [Display(Name = "Kuras")]
        public FuelType FuelType { get; set; }

        [Display(Name = "Rida")]
        public int OdometerReading { get; set; }

        [Display(Name = "Galia")]
        public int Power { get; set; }

        [Display(Name = "Sėdimų vietų skaičius")]
        public int SeatCount { get; set; }

        [Display(Name = "Langų tipas")]
        public WindowType WindowType { get; set; }

        [Display(Name = "Durų skaičius")]
        public int DoorCount { get; set; }

        [Display(Name = "Tipas")]
        public BodyType BodyType { get; set; }

        [Display(Name = "Valdymo tipas")]
        public GearboxType GearboxType { get; set; }

        [Display(Name = "Degalų naudojimas")]
        public double FuelConsumption { get; set; }

        [Display(Name = "Navigacijos sistema")]
        public bool HasNavigationSystem { get; set; }

        [Display(Name = "Vaikiška kėdutė")]
        public bool HasChildChair { get; set; }

        [Display(Name = "Kondicionierius")]
        public bool HasAC { get; set; }

        [Display(Name = "USB jungtis")]
        public bool HasUSB { get; set; }

        [Display(Name = "Daužta")]
        public bool IsDamaged { get; set; }

        [Display(Name = "Nuomos punktas")]
        public virtual RentPoint RentPoint { get; set; }

        [Display(Name = "Nuomos punktas")]
        [ForeignKey("RentPoint")]
        public int RentPointId { get; set; }


        public virtual List<Request> Requests { get; set; }

        [Display(Name = "Aikštelėje")]
        [NotMapped]
        public bool IsAvailable
        {
            get
            {
                var IsNotReturned = Requests.Any(r => r.IsReturned == false);
                return !IsNotReturned;
            }
        }
    }
}
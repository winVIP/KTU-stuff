using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    [Table("Requests")]
    public class Request
    {
        public int Id { get; set; }

        [Display(Name = "Rida pradžioje")]
        public int OdometerReadingAtStart { get; set; }

        [Display(Name = "Rida pabaigoje")]
        public int OdometerReadingAtEnd { get; set; }

        [Display(Name = "Pradžia")]
        public DateTime StartingDate { get; set; }

        [Display(Name = "Pabaiga")]
        public DateTime EndingDate { get; set; }

        [Display(Name = "Depozito suma")]
        public double Deposit { get; set; }

        [Display(Name = "Patvirtinimas")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "Grąžinimas")]
        public bool IsReturned { get; set; }

        [Display(Name = "Apmokėjimas")]
        public bool IsPaid { get; set; }

        public virtual Subscription Subscription { get; set; }
        [ForeignKey("Subscription")]
        public int? SubscriptionId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual Car Car { get; set; }
        public int CarId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    [Table("Subscriptions")]
    public class Subscription
    {
        public int Id { get; set; }
        public int Frequency { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public double Sum { get; set; }

        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual List<Request> Requests { get; set; }
    }
}
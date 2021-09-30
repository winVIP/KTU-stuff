using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Member : User
    {
        [Display(Name = "Asmens kodas")]
        public string IdentityCode { get; set; }

        [Display(Name = "Gimtadienis")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Telefono numeris")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Adresas")]
        public string Address { get; set; }

        [Display(Name = "Miestas")]
        public string City { get; set; }

        [Display(Name = "Šalis")]
        public string Country { get; set; }

        [Display(Name = "Tautybė")]
        public string Nationality { get; set; }

        [Display(Name = "Naujienlaiškis")]
        public bool HasNewsletter { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projektas_Irankiai.Models
{
    public class TradeOffer
    {
        [Display(Name = "Zinute")]
        public string message { get; set; }

        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Siuntejas")]
        public int user1 { get; set; }

        [Display(Name = "Siuntejas Vardas")]
        public string user1Name { get; set; }

        [Display(Name = "Gavejas")]
        public int user2 { get; set; }

        [Display(Name = "Gavejas Vardas")]
        public string user2Name { get; set; }

        [Display(Name = "Paveikslelio id")]
        public int imageId { get; set; }

        [Display(Name = "Paveikslelio pavadinimas")]
        public string imageName { get; set; }

        public string toSqlValues()
        {
            return String.Format("('{0}', {1}, '{2}', {3})", message, id, user1, user2);
        }
    }
}
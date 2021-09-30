using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projektas_Irankiai.Models
{
    public class TradableImage
    {
        [Display(Name = "Pavadinimas")]
        public string name { get; set; }

        [Display(Name = "Saugojimo_vieta")]
        public string path { get; set; }

        [Display(Name = "id_Mainomas_Paveikslelis")]
        public int id { get; set; }

        [Display(Name = "fk_Naudotojasid_Naudotojas")]
        public int userid { get; set; }

        [Display(Name = "fk_Mainu_uzklausaid_Mainu_uzklausa")]
        public int tradeid { get; set; }

        public string toSqlValues()
        {
            return String.Format("('{0}', {1}, '{2}', {3}, {4})", name, path, id, userid, tradeid);
        }
    }
}
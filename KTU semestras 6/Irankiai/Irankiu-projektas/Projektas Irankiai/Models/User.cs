using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projektas_Irankiai.Models
{
    public class User
    {
        [Display(Name = "Vardas")]
        public string name { get; set; }

        [Display(Name = "El_Pastas")]
        public string email { get; set; }

        [Display(Name = "Slaptazodis")]
        public string password { get; set; }//Ar yra tikslas čia saugoti?

        [Display(Name = "Pavarde")]
        public string lastname { get; set; }

        [Display(Name = "Role")]
        public int role { get; set; }

        [Display(Name = "ID")]
        public int id { get; set; }

        public string toSqlValues()
        {
            return String.Format("('{0}', {1}, '{2}', {3}, {4}, '{5}')", name, email, password, lastname, role, id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vytkun.Models
{
    public class Projektas
    {
        public int id { get; set; }
        public string pavadinimas { get; set; }
        public DateTime nuo_kada { get; set; }
        public DateTime iki_kada { get; set; }
        public int fk_uzsakovas { get; set; }
    }
}
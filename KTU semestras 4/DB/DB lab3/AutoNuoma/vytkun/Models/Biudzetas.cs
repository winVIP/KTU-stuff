using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vytkun.Models
{
    public class Biudzetas
    {
        public int id { get; set; }
        public double esamas_biudzetas { get; set; }
        public double imamas_kiekis { get; set; }
        public double dedamas_kiekis { get; set; }
        public DateTime data { get; set; }
    }
}
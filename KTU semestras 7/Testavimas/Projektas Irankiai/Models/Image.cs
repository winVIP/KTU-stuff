using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projektas_Irankiai.Models
{
    public class Image
    {
        [Display(Name = "Pavadinimas")]
        public string name { get; set; }

        [Display(Name = "Ar Pazymetas")]
        public bool isMarked { get; set; }

        [Display(Name = "Saugojimo vieta")]
        public string path { get; set; }

        [Display(Name = "Ivertinimas")]
        public double rating { get; set; }

        [Display(Name = "Ar Sablonas")]
        public bool isTemplate { get; set; }

        [Display(Name = "Tekstas")]
        public string text { get; set; }

        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Ikelejas")]
        public int uploader { get; set; }

        [Display(Name = "Zymetojas")]
        public int marker { get; set; }

        [Display(Name = "Žymė")]
        public string tag { get; set; }
        [Display(Name = "Failas")]
        public HttpPostedFileBase imageFile { get; set; }

        public System.Drawing.Image image { get; set; }

        public string toSqlValues()
        {
            int arPazymeta = 0;
            if (isMarked == true) arPazymeta = 1;

            int arSablonas = 0;
            if (isTemplate == true) arSablonas = 1;

            return String.Format("('{0}', {1}, '{2}', {3}, {4}, '{5}', {6}, {7}, {8})", name, arPazymeta, path, rating, arSablonas, text, id, uploader, marker);
        }
    }
}
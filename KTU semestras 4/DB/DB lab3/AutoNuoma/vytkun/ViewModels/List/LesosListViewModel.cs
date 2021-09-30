using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace vytkun.ViewModels
{
    public class LesosListViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Suma")]
        public double suma { get; set; }

        [DisplayName("Data")]
        public DateTime data { get; set; }

        [DisplayName("Uzsakovas")]
        public string uzsakovas { get; set; }

        [DisplayName("Remejas")]
        public string remejas { get; set; }
    }
}
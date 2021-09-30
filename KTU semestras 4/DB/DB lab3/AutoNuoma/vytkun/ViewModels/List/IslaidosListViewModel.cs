using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace vytkun.ViewModels
{
    public class IslaidosListViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Suma")]
        public double suma { get; set; }

        [DisplayName("Paskirtis")]
        public string paskirtis { get; set; }

        [DisplayName("Data")]
        public DateTime data { get; set; }

        [DisplayName("Uzduotis")]
        public string uzduotis { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Admin : User
    {
        public Level Level { get; set; }
    }
}
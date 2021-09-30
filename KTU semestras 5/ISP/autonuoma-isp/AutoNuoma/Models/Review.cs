using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Display(Name = "Pavadinimas")]
        public string Title { get; set; }

        [Display(Name = "Turinys")]
        public string Text { get; set; }

        [Display(Name = "Vertinimas")]
        public int Evaluation { get; set; }

        public virtual Request Request { get; set; }

        [Display(Name = "Patvirtinimas")]
        public bool IsApproved { get; set; }
    }
}
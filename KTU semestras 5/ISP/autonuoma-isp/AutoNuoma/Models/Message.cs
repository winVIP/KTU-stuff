using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    [Table("Messages")]
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public virtual Chat Chat { get; set; }
        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
    }
}
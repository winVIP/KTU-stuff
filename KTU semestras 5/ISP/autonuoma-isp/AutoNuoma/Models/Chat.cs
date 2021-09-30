using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    [Table("Chats")]
    public class Chat
    {
        public int Id { get; set; }

        public int CaseNumber { get; set; }
        public bool IsCaseClosed { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
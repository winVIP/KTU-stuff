using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public abstract class User
    {
        public int Id { get; set; }

        [Display(Name = "Pašto adresas")]
        public string Email { get; set; }

        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [Display(Name = "Vardas")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        public string LastName { get; set; }

        [Required]
        public int Type { get; set; }
        public virtual List<Chat> Chats { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual List<Request> Requests { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; }

    }
}
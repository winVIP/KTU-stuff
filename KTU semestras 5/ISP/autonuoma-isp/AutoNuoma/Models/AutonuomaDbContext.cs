using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class AutonuomaDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RentPoint> RentPoints { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
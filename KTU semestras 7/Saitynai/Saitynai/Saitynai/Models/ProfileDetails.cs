using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Saitynai.Models
{
    public class ProfileDetails
    {
        public ObjectId _id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime birthday { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<ObjectId> writtenComments { get; set; }
        public List<ObjectId> postsMade { get; set; }
        public List<ObjectId> ratingsMade { get; set; }
    }
}
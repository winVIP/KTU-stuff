using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saitynai.Models
{
    public class Rating
    {
        public ObjectId _id { get; set; }
        public ObjectId comment { get; set; }
        public ObjectId post { get; set; }
        public int amount { get; set; }
    }
}
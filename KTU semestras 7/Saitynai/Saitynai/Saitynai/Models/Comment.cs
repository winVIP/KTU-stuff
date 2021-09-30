using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saitynai.Models
{
    public class Comment
    {
        public ObjectId _id { get; set; }
        public string text { get; set; }
    }
}
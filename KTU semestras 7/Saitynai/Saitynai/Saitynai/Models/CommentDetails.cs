using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saitynai.Models
{
    public class CommentDetails
    {
        public ObjectId _id { get; set; }
        public ObjectId author { get; set; }
        public ObjectId post { get; set; }
        public string text { get; set; }
    }
}
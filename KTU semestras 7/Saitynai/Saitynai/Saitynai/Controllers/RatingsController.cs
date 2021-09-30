using MongoDB.Bson;
using MongoDB.Driver;
using Saitynai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Saitynai.Controllers
{
    public class RatingsController : ApiController
    {
        // GET: api/Ratings
        public IEnumerable<Rating> Get()
        {
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<Rating>("Reitingas");
            return collection.Find(new BsonDocument()).ToList().Select(x => new Rating() { _id = x._id, comment = x.comment, post = x.post, amount = x.amount }).ToList();
        }

        // GET: api/Ratings/5
        public Rating Get(string id)
        {
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<Rating>("Reitingas");
            Rating details = collection.Find(x => x._id == new ObjectId(id)).ToList().First();
            return details;
        }

        // POST: api/Ratings
        public void Post(Rating value)
        {
            value._id = ObjectId.GenerateNewId();
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<Rating>("Reitingas");
            collection.InsertOne(value);
        }

        // PUT: api/Ratings/5
        public void Put(string id, Rating value)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                value._id = new ObjectId(id);
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<Rating>("Reitingas");
                collection.ReplaceOne(x => x._id == new ObjectId(id), value);
            }
        }

        // DELETE: api/Ratings/5
        public void Delete(string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<Rating>("Reitingas");
                collection.DeleteOne(x => x._id == new ObjectId(id));
            }
        }
    }
}

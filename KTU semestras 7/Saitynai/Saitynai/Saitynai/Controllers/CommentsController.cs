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
    public class CommentsController : ApiController
    {
        // GET: api/Comments
        public IEnumerable<Comment> Get()
        {
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<CommentDetails>("Komentaras");
            return collection.Find(new BsonDocument()).ToList().Select(x => new Comment() { _id = x._id, text = x.text }).ToList();
        }

        // GET: api/Comments/5
        public Comment Get(string id)
        {
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<CommentDetails>("Komentaras");
            CommentDetails details = collection.Find(x => x._id == new ObjectId(id)).ToList().First();
            Comment comment = new Comment() { _id = details._id, text = details.text };
            return comment;
        }

        // POST: api/Comments
        public void Post(CommentDetails value)
        {
            value._id = ObjectId.GenerateNewId();
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<CommentDetails>("Komentaras");
            collection.InsertOne(value);
        }

        [Authorize]
        // PUT: api/Comments/5
        public void Put(string id, CommentDetails value)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                value._id = new ObjectId(id);
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<CommentDetails>("Komentaras");
                collection.ReplaceOne(x => x._id == new ObjectId(id), value);
            }
        }

        [Authorize]
        // DELETE: api/Comments/5
        public void Delete(string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<CommentDetails>("Komentaras");
                collection.DeleteOne(x => x._id == new ObjectId(id));
            }
        }

        [Route("api/Comments/{id}/Details")]
        [Authorize]
        [HttpGet]
        public CommentDetails Details(string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<CommentDetails>("Komentaras");
                return collection.Find(x => x._id == new ObjectId(id)).ToList().First();
            }
            return null;
        }
    }
}

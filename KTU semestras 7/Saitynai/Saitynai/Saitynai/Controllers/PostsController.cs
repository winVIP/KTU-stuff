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
    public class PostsController : ApiController
    {
        // GET api/posts
        public IEnumerable<Post> Get()
        {
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<Post>("Skelbimas");
            return collection.Find(new BsonDocument()).ToList().Select(x => new Post() { _id = x._id, title = x.title, text = x.text, photos = x.photos }).ToList();
        }

        // GET api/posts/5
        public Post Get(string id)
        {
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<Post>("Skelbimas");
            Post post = collection.Find(x => x._id == new ObjectId(id)).ToList().First();
            return post;
        }

        // POST api/posts
        public void Post(Post value)
        {
            value._id = ObjectId.GenerateNewId();
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<Post>("Skelbimas");
            collection.InsertOne(value);
        }

        // PUT api/posts/5
        public void Put(string id, Post value)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                value._id = new ObjectId(id);
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<Post>("Skelbimas");
                collection.ReplaceOne(x => x._id == new ObjectId(id), value);
            }
        }

        // DELETE api/posts/5
        public void Delete(string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<Post>("Skelbimas");
                collection.DeleteOne(x => x._id == new ObjectId(id));
            }
        }
    }
}

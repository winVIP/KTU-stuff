using Saitynai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Ajax.Utilities;
using System.Security.Claims;

namespace Saitynai.Controllers
{
    public class ProfilesController : ApiController
    {
        // GET api/profiles
        public IEnumerable<Profile> Get()
        {
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<ProfileDetails>("Profilis");
            return collection.Find(new BsonDocument()).ToList().Select(x => new Profile() { _id=x._id, firstName = x.firstName, lastName = x.lastName, birthday = x.birthday, email = x.email }).ToList();
        }

        // GET api/profiles/5
        public Profile Get(string id)
        {
            var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
            var database = client.GetDatabase("SkelbimuPuslapis");
            var collection = database.GetCollection<ProfileDetails>("Profilis");
            ProfileDetails details = collection.Find(x => x._id == new ObjectId(id)).ToList().First();
            Profile profile = new Profile() { _id = details._id, firstName = details.firstName, lastName = details.lastName, birthday = details.birthday, email = details.email };
            return profile;
        }

        [Authorize]
        // POST api/profiles
        public void Post(ProfileDetails value)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                //ProfileDetails newProfile = JsonSerializer.Deserialize<ProfileDetails>(value);
                value._id = ObjectId.GenerateNewId();
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<ProfileDetails>("Profilis");
                collection.InsertOne(value);
            }
        }

        [Authorize]
        // PUT api/profiles/5
        public IHttpActionResult Put(string id, ProfileDetails value)
        {
            var identity = User.Identity as ClaimsIdentity;
            string identityString = identity.Claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
            if (identity != null && identityString.Equals("admin"))
            {
                value._id = new ObjectId(id);
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<ProfileDetails>("Profilis");
                collection.ReplaceOne(x => x._id == new ObjectId(id), value);
                return Ok();
            }
            return Unauthorized();
        }

        [Authorize]
        // DELETE api/profiles/5
        public IHttpActionResult Delete(string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            string identityString = identity.Claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
            if (identity != null &&  identityString.Equals("admin"))
            {
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<ProfileDetails>("Profilis");
                collection.DeleteOne(x => x._id == new ObjectId(id));
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [Route("api/Profiles/{id}/Details")]
        [Authorize]
        [HttpGet]
        public ProfileDetails Details(string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            string identityString = identity.Claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
            if (identity != null && identityString.Equals("admin"))
            {
                var client = new MongoClient("mongodb+srv://admin:admin@saitynaicluster.blaug.mongodb.net/SkelbimuPuslapis?retryWrites=true&w=majority");
                var database = client.GetDatabase("SkelbimuPuslapis");
                var collection = database.GetCollection<ProfileDetails>("Profilis");
                return collection.Find(x => x._id == new ObjectId(id)).ToList().First();
            }
            return null;
        }
    }
}

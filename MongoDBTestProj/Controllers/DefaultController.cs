using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBTestProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoDBTestProj.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            using (BlogDBContext _db = new BlogDBContext())
            {
                var comments = _db.Collection<comments>();
                var result = comments.Find(x => true).ToList();

                var data = new List<CommentModel>();
                result.ForEach(x => data.Add(new CommentModel {
                    _id = x._id,
                    name = x.name,
                    type = x.type,
                    comment = x.comment
                }));

                return View("Index", data);
            }
        }

        [HttpPost]
        public ActionResult Insert(CommentModel model)
        {
            // Connection
            var client = new MongoClient("mongodb://localhost:27017");
            // Get DB
            var db = client.GetDatabase("blog");
            // Get Collection (Table)
            // You can use BsonDocument or Your Model Object
            var collection = db.GetCollection<CommentModel>("comments");
            collection.InsertOne(model);

            var data = collection.Find(x => true).ToList();

            return View("Index", data);
        }

        [HttpPost]
        public ActionResult Update(string _id, CommentModel model)
        {
            // Connection
            var client = new MongoClient("mongodb://localhost:27017");
            // Get DB
            var db = client.GetDatabase("blog");
            // Get Collection (Table)
            // You can use BsonDocument or Your Model Object
            var collection = db.GetCollection<CommentModel>("comments");
            var update = Builders<CommentModel>.Update.Set(x => x.name, model.name).Set(x => x.comment, model.comment);
            ObjectId id = new ObjectId(_id);

            var result = collection.UpdateOne(x => x._id == id, update);

            var data = collection.Find(x => true).ToList();

            return View("Index", data);
        }

        public ActionResult Delete(string _id)
        {
            // Connection
            var client = new MongoClient("mongodb://localhost:27017");
            // Get DB
            var db = client.GetDatabase("blog");
            // Get Collection (Table)
            // You can use BsonDocument or Your Model Object
            var collection = db.GetCollection<CommentModel>("comments");
            ObjectId id = new ObjectId(_id);

            var result = collection.DeleteOne(x => x._id == id);

            var data = collection.Find(x => true).ToList();

            return View("Index", data);
        }
    }
}
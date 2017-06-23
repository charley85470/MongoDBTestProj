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

                return View("Index", result);
            }
        }

        public ActionResult Query(comments model)
        {
            using (BlogDBContext _db = new BlogDBContext())
            {
                var comments = _db.Collection<comments>();

                #region Query Parameters
                var filterBuilder = Builders<comments>.Filter;
                var filter = filterBuilder.Empty;
                if (model != null)
                {
                    if (!string.IsNullOrEmpty(model.name))
                    {
                        filter = filter & filterBuilder.Eq(x => x.name, model.name);
                    }
                    if (!string.IsNullOrEmpty(model.comment))
                    {
                        filter = filter & filterBuilder.Eq(x => x.comment, model.comment);
                    }
                }
                #endregion

                var result = comments.Find(filter).ToList();

                return View("Index", result);
            }
        }

        [HttpPost]
        public ActionResult Insert(comments model)
        {
            using (BlogDBContext _db = new BlogDBContext())
            {
                var comments = _db.Collection<comments>();
                comments.InsertOne(model);
                var result = comments.Find(x => true).ToList();

                return View("Index", result);
            }
        }

        [HttpPost]
        public ActionResult Update(string _id, comments model)
        {
            using (BlogDBContext _db = new BlogDBContext())
            {
                var comments = _db.Collection<comments>();
                var update = Builders<comments>.Update.Set(x => x.name, model.name).Set(x => x.comment, model.comment);
                ObjectId id = new ObjectId(_id);
                var result = comments.UpdateOne(x => x._id == id, update);
                var data = comments.Find(x => true).ToList();

                return View("Index", data);
            }
        }

        public ActionResult Delete(string _id)
        {
            using (BlogDBContext _db = new BlogDBContext())
            {
                var comments = _db.Collection<comments>();
                ObjectId id = new ObjectId(_id);
                var result = comments.DeleteOne(x => x._id == id);
                var data = comments.Find(x => true).ToList();

                return View("Index", data);
            }
        }
    }
}
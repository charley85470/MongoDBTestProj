using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBTestProj.Models
{
    public class comments
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string comment { get; set; }
        public string type { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(CommentModel))
            {
                var o = obj as CommentModel;
                var bl = !(name ?? "").Equals(o.name) ? false :
                         !(comment ?? "").Equals(o.comment) ? false :
                         !(type ?? "").Equals(o.type) ? false : true;
                return bl;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(comments lhs, comments rhs)
        {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(comments lhs, comments rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
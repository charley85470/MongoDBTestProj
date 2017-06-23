using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBTestProj.Models
{
    public class BlogDBContext : IDisposable
    {
        /// <summary>
        /// Connection String
        /// </summary>
        private const string conn = @"mongodb://localhost:27017";
        /// <summary>
        /// Client
        /// </summary>
        MongoClient client = null;

        public BlogDBContext()
        {
            client = new MongoClient(conn);
            Database = client.GetDatabase("blog");
        }

        public IMongoDatabase Database { get; }

        public IMongoCollection<TDocument> Collection<TDocument>()
        {
            return Database.GetCollection<TDocument>(typeof(TDocument).Name);
        }

        #region IDisposable Support
        public void Dispose()
        {
        }
        #endregion
    }
}
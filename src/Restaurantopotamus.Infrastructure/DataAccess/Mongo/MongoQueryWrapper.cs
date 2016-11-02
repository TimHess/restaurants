using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess.Mongo
{
    public class MongoQueryWrapper : IDataQueries
    {
        MongoClient mongoClient;
        IMongoDatabase mongoDb;

        public MongoQueryWrapper(string connString)
        {
            mongoClient = new MongoClient(connString);
            mongoDb = mongoClient.GetDatabase("Restaurants");
        }

        public IQueryable<Restaurant> Restaurants
        {
            get
            {
                var collection = mongoDb.GetCollection<BsonDocument>("Restaurant");
                var filter = Builders<BsonDocument>.Filter.Exists("Name");
                var results = collection.Find(filter).As<Restaurant>();
                return results.ToList().AsQueryable();
            }
        }

        public async Task<AppUser> FindUserAsync(string UserName)
        {
            var results = await mongoDb.GetCollection<BsonDocument>("AppUser").FindAsync(Builders<BsonDocument>.Filter.Eq("UserName", UserName));
            var result = results.FirstOrDefault();
            return BsonSerializer.Deserialize<AppUser>(result);
        }

        public Task<RatingSummary> GetRatingSummaryAsync(Guid RestaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
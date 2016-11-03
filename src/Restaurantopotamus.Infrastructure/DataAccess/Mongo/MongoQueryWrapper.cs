using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Concurrent;
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
                var toReturn = new ConcurrentQueue<Restaurant>();
                var restaurants = mongoDb.GetCollection<BsonDocument>("Restaurant");
                var ratings = mongoDb.GetCollection<BsonDocument>("Rating");
                var resFilter = Builders<BsonDocument>.Filter.Exists("Name") & Builders<BsonDocument>.Filter.Ne("Archived", true);
                var results = restaurants.Find(resFilter).As<Restaurant>();
                Parallel.ForEach(results.ToList(), (r) =>
                {
                    var relatedRatings = ratings.Find(Builders<BsonDocument>.Filter.Eq("RestaurantId", r.Id)).As<Rating>().ToList<Rating>();
                    toReturn.Enqueue(new Restaurant { Id = r.Id, Name = r.Name, CuisineType = r.CuisineType, Ratings = relatedRatings });
                });
                return toReturn.AsQueryable();
            }
        }

        public async Task<AppUser> FindUserAsync(string UserName)
        {
            var results = await mongoDb.GetCollection<BsonDocument>("AppUser").FindAsync(Builders<BsonDocument>.Filter.Eq("UserName", UserName));
            var result = results.FirstOrDefault();
            if (result != null)
            {
                return BsonSerializer.Deserialize<AppUser>(result);
            }
            else
            {
                return null;
            }
        }

        public async Task<RatingSummary> GetRatingSummaryAsync(string RestaurantId)
        {
            var relatedRatings = await mongoDb.GetCollection<BsonDocument>("Rating").Find(Builders<BsonDocument>.Filter.Eq("RestaurantId", RestaurantId)).As<Rating>().ToListAsync<Rating>();

            var toReturn = new RatingSummary {
                RestaurantId = RestaurantId,
                NumberOfRatings = relatedRatings.Count,
                AverageRating = relatedRatings.Count > 0 ? relatedRatings.Average(r => r.Value) : 0
            };

            return toReturn;
        }
    }
}
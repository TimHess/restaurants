using MongoDB.Bson;
using MongoDB.Driver;
using Restaurantopotamus.Core.Helpers;
using Restaurantopotamus.Core.Models;
using System;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess.Mongo
{
    public class MongoCommandWrapper : IDataCommands
    {
        MongoClient mongoClient;
        IMongoDatabase mongoDb;

        public MongoCommandWrapper(string connString)
        {
            mongoClient = new MongoClient(connString);
            mongoDb = mongoClient.GetDatabase("Restaurants");
        }

        public async Task<T> Add<T>(T toAdd)
        {
            var typeName = toAdd.GetType().Name;
            var collection = mongoDb.GetCollection<BsonDocument>(typeName);
            BsonDocument dbModel = new BsonDocument();
            var filterBuilder = Builders<BsonDocument>.Filter;
            switch (typeName)
            {
                case "Rating":
                    var rateTyped = DynamicTyping.ConvertValue<Rating>(toAdd);
                    dbModel = new BsonDocument { { "RestaurantId", rateTyped.RestaurantId }, { "Value", rateTyped.Value } };
                    break;
                case "Restaurant":
                    var resTyped = DynamicTyping.ConvertValue<Restaurant>(toAdd);
                    dbModel = new BsonDocument { { "Name", resTyped.Name }, { "CuisineType", resTyped.CuisineType } };
                    var existingRestaurants = await mongoDb.GetCollection<BsonDocument>("Restaurant").FindAsync(filterBuilder.Eq("Name", resTyped.Name) & filterBuilder.Ne("Archived", "true"));
                    if (existingRestaurants.Any())
                    {
                        throw new ArgumentException("Sorry, that restaurant is already in the system");
                    }
                    await collection.InsertOneAsync(dbModel);

                    // return the id back out to the client in case they want to turn around and edit without reloading the data
                    ((Restaurant)(object)toAdd).Id = dbModel.GetValue("_id").ToString();
                    return toAdd;

                case "AppUser":
                    var userTyped = DynamicTyping.ConvertValue<AppUser>(toAdd);
                    dbModel = new BsonDocument { { "UserName", userTyped.UserName }, { "PassHash", userTyped.PassHash } };

                    var existingUsers = await mongoDb.GetCollection<BsonDocument>("AppUser").FindAsync(filterBuilder.Eq("UserName", userTyped.UserName));
                    if (existingUsers.Any())
                    {
                        throw new ArgumentException("Sorry, that username is already in use");
                    }
                    break;

            }
            await collection.InsertOneAsync(dbModel);
            return toAdd;
        }

        public async Task Remove<T>(T toRemove)
        {
            var typeName = toRemove.GetType().Name;
            var collection = mongoDb.GetCollection<BsonDocument>(typeName);
            switch (typeName)
            {
                case "Restaurant":
                    var resTyped = DynamicTyping.ConvertValue<Restaurant>(toRemove);
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(resTyped.Id));
                    var update = Builders<BsonDocument>.Update.Set("Archived", true);
                    var result = await collection.UpdateOneAsync(filter, update);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<T> Update<T>(T toUpdate)
        {
            var typeName = toUpdate.GetType().Name;
            var collection = mongoDb.GetCollection<BsonDocument>(typeName);
            switch (typeName)
            {
                case "Restaurant":
                    var resTyped = DynamicTyping.ConvertValue<Restaurant>(toUpdate);
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(resTyped.Id));
                    var update = Builders<BsonDocument>.Update.Set("Name", resTyped.Name).Set("CuisineType", resTyped.CuisineType);
                    var result = await collection.UpdateOneAsync(filter, update);
                    return toUpdate;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RestaurantQueries : IRestaurantQueries
    {
        private readonly IDataQueries queries;

        public RestaurantQueries(IDataQueries context)
        {
            queries = context;
        }

        public async Task<Restaurant> Get(string Id)
        {
            return await Task.FromResult(queries.Restaurants.Where(x => !x.Archived).FirstOrDefault(x => x.Id == Id));
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await Task.FromResult(queries.Restaurants.Where(r => !r.Archived));
        }

        public async Task<IEnumerable<Restaurant>> Query(Expression<Func<Restaurant, bool>> query)
        {
            return await Task.FromResult(from r in queries.Restaurants.Where(query)
                          where !r.Archived
                          select r);
        }
    }
}
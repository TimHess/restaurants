using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RestaurantQueries : IRestaurantQueries
    {
        private readonly QueryContext queries;

        public RestaurantQueries(QueryContext context)
        {
            queries = context;
        }

        public async Task<Restaurant> Get(Guid Id)
        {
            return await queries.Restaurants.FindAsync(Id);
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await Task.FromResult(queries.Restaurants.Where(r => !r.Archived));
        }
    }
}
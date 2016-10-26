using Restaurantopotamus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurantopotamus.Core.Models;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RestaurantQueries : IRestaurantQueries
    {
        public Task<Restaurant> Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Restaurant>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

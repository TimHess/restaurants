using Restaurantopotamus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurantopotamus.Core.Models;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RestaurantCommands : IRestaurantCommands
    {
        public Task<Restaurant> AddRestaurant(Restaurant toAdd)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRestaurant(Guid toDelete)
        {
            throw new NotImplementedException();
        }

        public Task<Restaurant> UpdateRestaurant(Restaurant toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

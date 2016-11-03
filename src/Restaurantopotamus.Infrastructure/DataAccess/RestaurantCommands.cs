using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RestaurantCommands : IRestaurantCommands
    {
        private readonly IDataCommands commands;

        public RestaurantCommands(IDataCommands data)
        {
            commands = data;
        }

        public async Task<Restaurant> AddRestaurant(Restaurant toAdd)
        {
            return await commands.Add(toAdd);
        }

        public async Task DeleteRestaurant(string resId)
        {
            await commands.Remove(new Restaurant { Id = resId });
        }

        public async Task<Restaurant> UpdateRestaurant(Restaurant toUpdate)
        {
            return await commands.Update(toUpdate);
        }
    }
}

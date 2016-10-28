using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RestaurantCommands : IRestaurantCommands
    {
        private readonly CommandContext commands;
        private readonly QueryContext queries;

        public RestaurantCommands(CommandContext context, QueryContext qc)
        {
            commands = context;
            queries = qc;
        }

        public async Task<Restaurant> AddRestaurant(Restaurant toAdd)
        {
            commands.Restaurants.Add(toAdd);
            await commands.SaveChangesAsync();
            return toAdd;
        }

        public async Task DeleteRestaurant(Guid resId)
        {
            // get the record
            var toDelete = await queries.Restaurants.FindAsync(resId);
            toDelete.Archived = true;

            await commands.SaveChangesAsync();
        }

        public async Task<Restaurant> UpdateRestaurant(Restaurant toUpdate)
        {
            commands.Restaurants.Attach(toUpdate);
            commands.Entry(toUpdate).State = EntityState.Modified;
            await commands.SaveChangesAsync();
            return toUpdate;
        }
    }
}

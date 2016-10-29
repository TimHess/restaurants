using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RestaurantCommands : IRestaurantCommands
    {
        private readonly RestaurantContext context;

        public RestaurantCommands(RestaurantContext context)
        {
            this.context = context;
        }

        public async Task<Restaurant> AddRestaurant(Restaurant toAdd)
        {
            context.Restaurants.Add(toAdd);
            await context.SaveChangesAsync();
            return toAdd;
        }

        public async Task DeleteRestaurant(Guid resId)
        {
            // get the record
            var toDelete = await context.Restaurants.FindAsync(resId);
            toDelete.Archived = true;
            await context.SaveChangesAsync();
        }

        public async Task<Restaurant> UpdateRestaurant(Restaurant toUpdate)
        {
            context.Restaurants.Attach(toUpdate);
            context.Entry(toUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return toUpdate;
        }
    }
}

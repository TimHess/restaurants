using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RatingCommands : IRatingCommands
    {
        private readonly RestaurantContext commands;

        public RatingCommands(RestaurantContext context)
        {
            commands = context;
        }

        public async Task AddRating(Guid RestaurantId, int RatingValue)
        {
            commands.Ratings.Add(new Rating { RestaurantId = RestaurantId, Value = RatingValue });
            await commands.SaveChangesAsync();
        }
    }
}
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RatingCommands : IRatingCommands
    {
        private readonly IDataCommands commands;

        public RatingCommands(IDataCommands data)
        {
            commands = data;
        }

        public async Task AddRating(string RestaurantId, int RatingValue)
        {
            await commands.Add<Rating>(new Rating { RestaurantId = RestaurantId, Value = RatingValue });
        }
    }
}
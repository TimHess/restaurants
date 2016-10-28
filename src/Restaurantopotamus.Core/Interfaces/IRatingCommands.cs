using Restaurantopotamus.Core.Models;
using System;
using System.Threading.Tasks;

namespace Restaurantopotamus.Core.Interfaces
{
    public interface IRatingCommands
    {
        /// <summary>
        /// Record a new rating for a restaurant
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <param name="RatingValue"></param>
        /// <returns></returns>
        Task AddRating(Guid RestaurantId, int RatingValue);
    }
}
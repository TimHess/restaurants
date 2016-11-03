using Restaurantopotamus.Core.Models;
using System;
using System.Threading.Tasks;

namespace Restaurantopotamus.Core.Interfaces
{
    public interface IRatingQueries
    {
        /// <summary>
        /// Lookup the average and number of ratings for a given restaurant
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <returns></returns>
        Task<RatingSummary> GetSummary(string RestaurantId);
    }
}
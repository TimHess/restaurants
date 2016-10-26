using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RatingCommands : IRatingCommands
    {
        private IList<Rating> ratings;

        public RatingCommands()
        {
            ratings = new List<Rating>();
        }

        public async Task AddRating(Guid RestaurantId, int RatingValue)
        {
            await Task.Run(() => ratings.Add(new Rating { RestaurantId = RestaurantId, Value = RatingValue }));
        }

        /// <summary>
        /// in-memory poc... TODO: delete once there's a persistent data store
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <returns></returns>
        public async Task<RatingSummary> GetSummary(Guid RestaurantId)
        {
            IEnumerable<Rating> relevant = ratings.Where(r => r.RestaurantId == RestaurantId);
            return new RatingSummary { RestaurantId = RestaurantId, NumberOfRatings = relevant.Count(), AverageRating = relevant.Average(r => r.Value) };
        }
    }
}
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RatingQueries : IRatingQueries
    {
        private readonly QueryContext queries;

        public RatingQueries(QueryContext context)
        {
            queries = context;
        }

        public async Task<RatingSummary> GetSummary(Guid RestaurantId)
        {
            RatingSummary toReturn = new RatingSummary { RestaurantId = RestaurantId };
            var ratings = queries.Ratings.Where(r => r.RestaurantId == RestaurantId);
            toReturn.NumberOfRatings = ratings.Count();
            if (toReturn.NumberOfRatings != 0)
            {
                toReturn.AverageRating = ratings.Average(r => r.Value);
            }
            else
            {
                toReturn.AverageRating = 0;
            }

            return await Task.FromResult(toReturn);
        }
    }
}

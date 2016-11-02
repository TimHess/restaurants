using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RatingQueries : IRatingQueries
    {
        private IDataQueries queries;

        public RatingQueries(IDataQueries data)
        {
            queries = data;
        }

        public async Task<RatingSummary> GetSummary(Guid RestaurantId)
        {
            return await queries.GetRatingSummaryAsync(RestaurantId);
        }
    }
}

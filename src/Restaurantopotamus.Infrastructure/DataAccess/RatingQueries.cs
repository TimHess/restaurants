using Restaurantopotamus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurantopotamus.Core.Models;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RatingQueries : IRatingQueries
    {
        public Task<RatingSummary> GetSummary(Guid RestaurantId)
        {
            throw new NotImplementedException();
        }
    }
}

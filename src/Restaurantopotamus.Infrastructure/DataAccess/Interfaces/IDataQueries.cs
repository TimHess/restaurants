using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public interface IDataQueries
    {
        Task<RatingSummary> GetRatingSummaryAsync(Guid RestaurantId);

        Task<AppUser> FindUserAsync(string UserName);

        IQueryable<Restaurant> Restaurants { get; }
    }
}
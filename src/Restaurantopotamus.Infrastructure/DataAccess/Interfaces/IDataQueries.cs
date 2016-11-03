using Restaurantopotamus.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public interface IDataQueries
    {
        Task<RatingSummary> GetRatingSummaryAsync(string RestaurantId);

        Task<AppUser> FindUserAsync(string UserName);

        IQueryable<Restaurant> Restaurants { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurantopotamus.Core.Models;

namespace Restaurantopotamus.Infrastructure.DataAccess.EF
{
    public class EFQueryWrapper : IDataQueries
    {
        private readonly RestaurantContext context;

        public EFQueryWrapper(RestaurantContext context)
        {
            this.context = context;
        }

        public IQueryable<Restaurant> Restaurants
        {
            get
            {
                return context.Restaurants;
            }
        }

        public async Task<AppUser> FindUserAsync(string Username)
        {
            return await context.Users.FindAsync(Username);
        }

        public async Task<RatingSummary> GetRatingSummaryAsync(string RestaurantId)
        {
            RatingSummary toReturn = new RatingSummary { RestaurantId = RestaurantId };
            var ratings = context.Ratings.Where(r => r.RestaurantId == RestaurantId);
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

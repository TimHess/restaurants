using Restaurantopotamus.Core.Models;
using System.Data.Entity;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(string connString) : base(connString)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Rating> Ratings { get; set; }
    }
}

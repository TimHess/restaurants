using Restaurantopotamus.Core.Models;
using System.Data.Entity;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class QueryContext : DbContext
    {
        public QueryContext(string connString) : base(connString)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Rating> Ratings { get; set; }
    }
}

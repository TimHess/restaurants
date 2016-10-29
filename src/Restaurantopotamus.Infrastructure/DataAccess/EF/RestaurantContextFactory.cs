using Microsoft.Azure;
using System.Data.Entity.Infrastructure;

namespace Restaurantopotamus.Infrastructure.DataAccess.EF
{
    public class RestaurantContextFactory : IDbContextFactory<RestaurantContext>
    {
        public RestaurantContext Create()
        {
            return new RestaurantContext(CloudConfigurationManager.GetSetting("RestaurantConnectionstring"));
        }
    }
}
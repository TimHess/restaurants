using Microsoft.Azure;
using System.Data.Entity.Infrastructure;

namespace Restaurantopotamus.Infrastructure.DataAccess.EF
{
    public class QueryContextFactory : IDbContextFactory<QueryContext>
    {
        public QueryContext Create()
        {
            return new QueryContext(CloudConfigurationManager.GetSetting("CommandConnectionstring"));
        }
    }
}

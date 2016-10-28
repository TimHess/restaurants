using Microsoft.Azure;
using System.Data.Entity.Infrastructure;

namespace Restaurantopotamus.Infrastructure.DataAccess.EF
{
    public class CommandContextFactory : IDbContextFactory<CommandContext>
    {
        public CommandContext Create()
        {
            return new CommandContext(CloudConfigurationManager.GetSetting("CommandConnectionstring"));
        }
    }
}
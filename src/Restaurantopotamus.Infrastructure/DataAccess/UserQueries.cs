using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class UserQueries : IUserQueries
    {
        private RestaurantContext context;

        public UserQueries(RestaurantContext context)
        {
            this.context = context;
        }

        public async Task<bool> Login(string UserName, string Password)
        {
            AppUser user = await context.Users.FindAsync(UserName);
            if (BCrypt.Net.BCrypt.Verify(Password, user.PassHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
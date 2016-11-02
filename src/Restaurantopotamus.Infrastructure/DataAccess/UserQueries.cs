using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class UserQueries : IUserQueries
    {
        private readonly IDataQueries queries;

        public UserQueries(IDataQueries data)
        {
            this.queries = data;
        }

        public async Task<bool> Login(string UserName, string Password)
        {
            AppUser user = await queries.FindUserAsync(UserName);
            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user?.PassHash))
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
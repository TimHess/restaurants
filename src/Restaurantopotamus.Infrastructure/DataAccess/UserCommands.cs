using Restaurantopotamus.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Restaurantopotamus.Core.Models;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class UserCommands : IUserCommands
    {
        private RestaurantContext context;

        public UserCommands(RestaurantContext context)
        {
            this.context = context;
        }

        public async Task<AppUser> Register(string UserName, string Password)
        {
            if (context.Users.Any(x => x.UserName.Equals(UserName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Username already in use");
            }

            var toAdd = new AppUser
            {
                UserName = UserName,
                PassHash = BCrypt.Net.BCrypt.HashPassword(Password, 10),
                Password = "NotMapped"
            };
            context.Users.Add(toAdd);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    throw new ArgumentException("Username already in use");
                }
                else
                {
                    throw;
                }
            }
            return toAdd;
        }
    }
}
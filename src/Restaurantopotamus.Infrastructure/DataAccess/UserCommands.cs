using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    public class UserCommands : IUserCommands
    {
        private readonly IDataCommands commands;

        public UserCommands(IDataCommands data)
        {
            this.commands = data;
        }

        public async Task<AppUser> Register(string UserName, string Password)
        {
            var toAdd = new AppUser
            {
                UserName = UserName,
                PassHash = BCrypt.Net.BCrypt.HashPassword(Password, 10),
                Password = "NotMapped" // TODO: remove hack to bypass validation
            };

            try
            {
                toAdd = await commands.Add(toAdd);
            }
            catch (Exception ex)
            {
                SqlException innerException = null;
                try
                {
                    innerException = ex.InnerException.InnerException as SqlException;
                }
                catch (Exception e)
                {
                    Trace.TraceError("Non-sql error message encountered while registering: {0}", e.ToString());
                }

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
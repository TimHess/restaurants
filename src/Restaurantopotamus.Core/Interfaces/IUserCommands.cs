using Restaurantopotamus.Core.Models;
using System.Threading.Tasks;

namespace Restaurantopotamus.Core.Interfaces
{
    public interface IUserCommands
    {
        Task<AppUser> Register(string UserName, string Password);
    }
}

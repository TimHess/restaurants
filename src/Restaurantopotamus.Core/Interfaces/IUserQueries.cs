using System.Threading.Tasks;

namespace Restaurantopotamus.Core.Interfaces
{
    public interface IUserQueries
    {
        Task<bool> Login(string UserName, string Password);
    }
}

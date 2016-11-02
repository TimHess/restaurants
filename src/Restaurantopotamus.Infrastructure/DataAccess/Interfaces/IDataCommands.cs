using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess
{
    /// <summary>
    /// abstraction layer to cover datastore
    /// </summary>
    public interface IDataCommands
    {
        Task<T> Add<T>(T toAdd);

        Task<T> Update<T>(T toUpdate);

        Task Remove<T>(T toRemove);
    }
}
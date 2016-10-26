using Restaurantopotamus.Core.Models;
using System;
using System.Threading.Tasks;

namespace Restaurantopotamus.Core.Interfaces
{
    public interface IRestaurantCommands
    {
        /// <summary>
        /// Record a new restaurant
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns></returns>
        Task<Restaurant> AddRestaurant(Restaurant toAdd);

        /// <summary>
        /// Update a restaurant
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        Task<Restaurant> UpdateRestaurant(Restaurant toUpdate);

        /// <summary>
        /// Soft-delete a restaurant
        /// </summary>
        /// <param name="toDelete"></param>
        /// <returns></returns>
        Task DeleteRestaurant(Guid toDelete);
    }
}
using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurantopotamus.Core.Interfaces
{
    public interface IRestaurantQueries
    {
        /// <summary>
        /// Get one restaurant
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Restaurant> Get(Guid Id);

        /// <summary>
        /// Get all ACTIVE restaurants ----------- TODO: needs partitioning and/or paging scheme
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Restaurant>> GetAll();
    }
}
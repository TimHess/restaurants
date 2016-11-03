using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        Task<Restaurant> Get(string Id);

        /// <summary>
        /// Get all ACTIVE restaurants ----------- TODO: needs partitioning and/or paging scheme
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Restaurant>> GetAll();

        /// <summary>
        /// Search for restaurants
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        Task<IEnumerable<Restaurant>> Query(Expression<Func<Restaurant, bool>> Query);
    }
}
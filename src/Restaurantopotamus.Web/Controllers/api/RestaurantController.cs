using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System.Diagnostics;

namespace Restaurantopotamus.Controllers.api
{
    [Produces("application/json")]
    [Route("api/Restaurant")]
    public class RestaurantController : Controller
    {
        private IRestaurantQueries restaurantQueries;
        private IRestaurantCommands restaurantCommands;

        public RestaurantController(IRestaurantQueries queries, IRestaurantCommands commands)
        {
            restaurantQueries = queries;
            restaurantCommands = commands;
        }

        /// <summary>
        /// Get all restaurants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Restaurant>> Get()
        {
            return await restaurantQueries.GetAll();
        }

        /// <summary>
        /// Get a specific restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Restaurant> Get(Guid id)
        {
            return await restaurantQueries.Get(id);
        }

        /// <summary>
        /// Add a restaurant
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(Restaurant toAdd)
        {
            Restaurant added;

            try
            {
                added = await restaurantCommands.AddRestaurant(toAdd);
            }
            catch(Exception e)
            {
                Trace.TraceError("Failed to add a restaurant: {0}", e);
                throw new NotImplementedException();
            }

            return CreatedAtRoute("Get", added);
        }

        /// <summary>
        /// Update a restaurant
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Restaurant toUpdate)
        {
            await restaurantCommands.UpdateRestaurant(toUpdate);
            return NoContent();
        }

        /// <summary>
        /// Remove a restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await restaurantCommands.DeleteRestaurant(id);
            return NoContent();
        }
    }
}
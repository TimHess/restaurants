using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurantopotamus.Controllers.api
{
    [Produces("application/json")]
    [Route("api/Restaurant")]
    public class RestaurantController : Controller
    {
        private IRestaurantQueries restaurantQueries;
        private IRestaurantCommands restaurantCommands;
        private IRatingQueries ratingQueries;

        public RestaurantController(IRestaurantQueries queries, IRestaurantCommands commands, IRatingQueries ratequeries)
        {
            restaurantQueries = queries;
            restaurantCommands = commands;
            ratingQueries = ratequeries;
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
        [HttpGet("{id}", Name = "GetSingle")]
        public async Task<Restaurant> Get(Guid id)
        {
            return await restaurantQueries.Get(id);
        }

        /// <summary>
        /// Add a restaurant
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Restaurant toAdd)
        {
            // make sure the request looks good
            if (!ModelState.IsValid)
            {
                Trace.TraceInformation("User attempted to add an invalid restaurant");
                return BadRequest(ModelState);
            }

            // make sure we don't already have this restaurant
            var existing = await restaurantQueries.Query(x => x.Name.Equals(toAdd.Name, StringComparison.InvariantCultureIgnoreCase) &&
                                                                x.CuisineType.Equals(toAdd.CuisineType, StringComparison.InvariantCultureIgnoreCase));
            if (existing.Any())
            {
                Trace.TraceWarning("User attempted to save a duplicate restaurant: {0}/{1}", toAdd.Name, toAdd.CuisineType);
                Response.StatusCode = StatusCodes.Status409Conflict;
                return new EmptyResult();
            }

            // now go ahead and try to actually save it
            Restaurant added;
            try
            {
                added = await restaurantCommands.AddRestaurant(toAdd);
                var username = User.Claims.First(x => x.Type.Contains("nameidentifier"));
                Trace.TraceInformation("Restaurant {0} added by {1}", toAdd.Id, username.Value);
            }
            catch (Exception e)
            {
                Trace.TraceError("Failed to add a restaurant: {0}", e);
                return new StatusCodeResult(500);
            }

            return CreatedAtRoute("GetSingle", new { id = added.Id }, added);
        }

        /// <summary>
        /// Update a restaurant
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Restaurant toUpdate)
        {
            if (!ModelState.IsValid)
            {
                Trace.TraceInformation("User attempted to update restaurant with invalid data");
                return BadRequest(ModelState);
            }

            try
            {
                var username = User.Claims.First(x => x.Type.Contains("nameidentifier"));
                Trace.TraceInformation("Restaurant {0} updated by {1}", toUpdate.Id, username.Value);
                await restaurantCommands.UpdateRestaurant(toUpdate);
            }
            catch (Exception e)
            {
                Trace.TraceError("Failed to update a restaurant: {0}", e.ToString());
                return new StatusCodeResult(500);
            }

            return NoContent();
        }

        /// <summary>
        /// Remove a restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await restaurantCommands.DeleteRestaurant(id);
                var username = User.Claims.First(x => x.Type.Contains("nameidentifier"));
                Trace.TraceInformation("Restaurant {0} deleted by {1}", id, username.Value);
            }
            catch (Exception e)
            {
                Trace.TraceError("Failed to update a restaurant: {0}", e.ToString());
                return new StatusCodeResult(500);
            }

            return NoContent();
        }
    }
}
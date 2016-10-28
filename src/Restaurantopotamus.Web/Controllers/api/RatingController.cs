using Microsoft.AspNetCore.Mvc;
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Restaurantopotamus.Controllers.api
{
    [Produces("application/json")]
    [Route("api/Rating")]
    public class RatingController : Controller
    {
        private IRatingQueries ratingQueries;
        private IRatingCommands ratingCommands;

        public RatingController(IRatingQueries queries, IRatingCommands commands)
        {
            ratingQueries = queries;
            ratingCommands = commands;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Json(await ratingQueries.GetSummary(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Rating rating)
        {
            if (!ModelState.IsValid)
            {
                Trace.TraceInformation("User attempted to post a rating with invalid data");
                return BadRequest(ModelState);
            }

            await ratingCommands.AddRating(rating.RestaurantId, rating.Value);
            return Created("Not Available", rating);
        }
    }
}
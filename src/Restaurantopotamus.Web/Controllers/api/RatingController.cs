using Microsoft.AspNetCore.Mvc;
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using System;
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
            return new JsonResult(await ratingCommands.GetSummary(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await ratingCommands.AddRating(rating.RestaurantId, rating.Value);
            return Created("n/a", rating);
        }
    }
}
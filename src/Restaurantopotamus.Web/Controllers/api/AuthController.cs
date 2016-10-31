using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using Restaurantopotamus.Middlewares;
using Restaurantopotamus.Web.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Restaurantopotamus.Controllers.api
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IUserQueries queries;
        private IUserCommands commands;
        private TokenProviderOptions options;

        public AuthController(IUserQueries queries, IUserCommands commands, IOptions<TokenProviderOptions> options)
        {
            this.queries = queries;
            this.commands = commands;
            this.options = options.Value;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]AppUser user)
        {
            if (!ModelState.IsValid)
            {
                Trace.TraceInformation("Failed registration with invalid data");
                return BadRequest(ModelState);
            }
            try
            {
                var registered = await commands.Register(user.UserName, user.Password);
                var helper = new JWTHelper(options);

                return Created("N/A", helper.GetJwt(user.UserName));
            }
            catch (ArgumentException)
            {
                return BadRequest($"Username {user.UserName} is already in use, please try another name");
            }
        }
    }
}
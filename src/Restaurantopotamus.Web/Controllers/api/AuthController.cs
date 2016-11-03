using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Core.Models;
using Restaurantopotamus.Middlewares;
using Restaurantopotamus.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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

        public AuthController(IUserQueries queries, IUserCommands commands)
        {
            this.queries = queries;
            this.commands = commands;

            // gross hack
            var secretKey = "Restaurantoppatomus!123";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            options = new TokenProviderOptions
            {
                Audience = "TheWorld",
                Issuer = "Restaurantoppotamus",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };
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
                var errors = new Dictionary<string, string>();
                errors.Add("error", $"Username {user.UserName} is already in use, please try another name");
                return BadRequest(errors);
            }
        }
    }
}
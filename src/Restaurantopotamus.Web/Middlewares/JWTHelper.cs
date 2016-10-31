using Newtonsoft.Json;
using Restaurantopotamus.Web.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurantopotamus.Middlewares
{
    public class JWTHelper
    {
        private TokenProviderOptions options;
        
        public JWTHelper(TokenProviderOptions options)
        {
            this.options = options;
        }

        public object GetJwt(string username)
        {
            var now = DateTimeOffset.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now.UtcDateTime,
                expires: now.UtcDateTime.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)options.Expiration.TotalSeconds
            };
            return response;
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurantopotamus.Core.Interfaces;
using Restaurantopotamus.Infrastructure.DataAccess;
using Restaurantopotamus.Middlewares;
using Restaurantopotamus.Web.Models;
using System;
using System.Text;

namespace Restaurantopotamus
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add application services.
            services.AddScoped<RestaurantContext>(_ => new RestaurantContext(Configuration.GetConnectionString("Restaurants")));
            services.AddTransient<IRatingCommands, RatingCommands>();
            services.AddTransient<IRatingQueries, RatingQueries>();
            services.AddTransient<IRestaurantCommands, RestaurantCommands>();
            services.AddTransient<IRestaurantQueries, RestaurantQueries>();
            services.AddTransient<IUserCommands, UserCommands>();
            services.AddTransient<IUserQueries, UserQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Setup token validation middleware
            var secretKey = "Restaurantoppatomus!123";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "Restaurantoppotamus",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "TheWorld",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AuthenticationScheme = "Active",
                AutomaticAuthenticate = true, 
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            // Add JWT generation endpoint:
            var options = new TokenProviderOptions
            {
                Audience = "TheWorld", 
                Issuer = "Restaurantoppotamus",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

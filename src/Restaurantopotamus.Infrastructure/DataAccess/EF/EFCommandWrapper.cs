﻿using Restaurantopotamus.Core.Helpers;
using Restaurantopotamus.Core.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Restaurantopotamus.Infrastructure.DataAccess.EF
{
    public class EFCommandWrapper : IDataCommands
    {
        private readonly RestaurantContext context;

        public EFCommandWrapper(RestaurantContext context)
        {
            this.context = context;
        }

        public async Task<T> Add<T>(T toAdd)
        {
            switch (toAdd.GetType().Name)
            {
                case "Rating":
                    var dbRat = DynamicTyping.ConvertValue<Rating>(toAdd);
                    dbRat.Id = Guid.NewGuid().ToString();
                    context.Ratings.Add(dbRat);
                    break;
                case "Restaurant":
                    var dbRes = DynamicTyping.ConvertValue<Restaurant>(toAdd);
                    dbRes.Id = Guid.NewGuid().ToString();
                    context.Restaurants.Add(dbRes);
                    break;
                case "AppUser":
                    context.Users.Add(DynamicTyping.ConvertValue<AppUser>(toAdd));
                    break;
            }
            await context.SaveChangesAsync();
            return toAdd;
        }

        public async Task Remove<T>(T toRemove)
        {
            switch (toRemove.GetType().Name)
            {
                case "Restaurant":
                    var typedUpdate = DynamicTyping.ConvertValue<Restaurant>(toRemove);
                    var existing = await context.Restaurants.FindAsync(typedUpdate.Id);
                    existing.Archived = true;
                    context.Restaurants.Attach(existing);
                    context.Entry(existing).State = EntityState.Modified;
                    break;
                default:
                    throw new NotImplementedException();
            }
            await context.SaveChangesAsync();
        }

        public async Task<T> Update<T>(T toUpdate)
        {
            switch (toUpdate.GetType().Name)
            {
                case "Restaurant":
                    var typedUpdate = DynamicTyping.ConvertValue<Restaurant>(toUpdate);
                    context.Restaurants.Attach(typedUpdate);
                    context.Entry(typedUpdate).State = EntityState.Modified;
                    break;
                default:
                    throw new NotImplementedException();
            }
            await context.SaveChangesAsync();
            return toUpdate;
        }
    }
}

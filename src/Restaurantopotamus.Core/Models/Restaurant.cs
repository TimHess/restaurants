using System;

namespace Restaurantopotamus.Core.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }

        /// <summary>
        /// What is the establishment named
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// What type of food is served here
        /// </summary>
        public string CuisineType { get; set; }
    }
}
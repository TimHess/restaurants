using System;

namespace Restaurantopotamus.Core.Models
{
    public class RatingSummary
    {
        /// <summary>
        /// What's the Id of the restaurant
        /// </summary>
        public Guid RestaurantId { get; set; }

        /// <summary>
        /// What is the average of all ratings for this restaurant
        /// </summary>
        public double AverageRating { get; set; }

        /// <summary>
        /// How many times has the restaurant been rated
        /// </summary>
        public int NumberOfRatings { get; set; }
    }
}
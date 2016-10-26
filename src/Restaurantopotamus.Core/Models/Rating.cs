using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurantopotamus.Core.Models
{
    public class Rating
    {
        /// <summary>
        /// Which restaurant
        /// </summary>
        [Required]
        public Guid RestaurantId { get; set; }

        /// <summary>
        /// How many stars
        /// </summary>
        [Required]
        [Range(1,5)]
        public int Value { get; set; }
    }
}
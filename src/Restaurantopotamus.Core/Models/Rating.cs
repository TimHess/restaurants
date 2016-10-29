using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurantopotamus.Core.Models
{
    public class Rating
    {
        /// <summary>
        /// Primary key in database
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// What is the Id of the restaurant
        /// </summary>
        [Required]
        public Guid RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        /// <summary>
        /// How many stars
        /// </summary>
        [Required]
        [Range(1,5)]
        public int Value { get; set; }
    }
}
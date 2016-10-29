using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Restaurantopotamus.Core.Models
{
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// What is the establishment named
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// What type of food is served here
        /// </summary>
        [Required]
        public string CuisineType { get; set; }

        /// <summary>
        /// Existing rating information
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Rating> Ratings { get; set; }

        [NotMapped]
        public int TotalRatings
        {
            get { return Ratings != null && Ratings?.Count > 0 ? Ratings.Count : 0; }
        }

        [NotMapped]
        public double AverageRating
        {
            get { return Ratings != null && Ratings?.Count > 0 ? Ratings.Average(r => r.Value) : 0; }
        }

        /// <summary>
        /// If Archived == True then somebody "deleted" the record
        /// </summary>
        [JsonIgnore]
        public bool Archived { get; set; }
    }
}
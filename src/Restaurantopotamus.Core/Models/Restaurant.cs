using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Name { get; set; }

        /// <summary>
        /// What type of food is served here
        /// </summary>
        public string CuisineType { get; set; }

        /// <summary>
        /// If Archived == True then somebody "deleted" the record
        /// </summary>
        public bool Archived { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}
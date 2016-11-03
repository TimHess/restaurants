using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurantopotamus.Core.Models
{
    [BsonIgnoreExtraElements]
    public class Rating
    {
        /// <summary>
        /// Primary key in database
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonIgnore]
        public string Id { get; set; }

        /// <summary>
        /// What is the Id of the restaurant
        /// </summary>
        [Required]
        public string RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        /// <summary>
        /// How many stars
        /// </summary>
        [Required]
        [Range(1,5)]
        public int Value { get; set; }
    }
}
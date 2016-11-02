using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurantopotamus.Core.Models
{
    [BsonIgnoreExtraElements]
    public class AppUser
    {
        [Key]
        [Required]
        public string UserName { get; set; }

        [NotMapped]
        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        public string PassHash { get; set; }
    }
}
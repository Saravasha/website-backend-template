using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAppBackend.Models
{

    //[Index(IsUnique =true)]
    public class Category
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [JsonIgnore]
        public List<Asset> Assets { get; set; } = new List<Asset>();

    }
}

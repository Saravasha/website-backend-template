using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GoshehArtWebApp.Models
{
    public class Font
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Style { get; set; }

        public string Weight { get; set; }

        public string FileUrl { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile FileUp { get; set; }


    }
}

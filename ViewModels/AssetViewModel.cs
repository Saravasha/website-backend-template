using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GoshehArtWebApp.Models;

namespace GoshehArtWebApp.ViewModels
{
    public class AssetViewModel 
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Asset Name")]
        public string Name { get; set; }
        public string? Author { get; set; }
        public string? Location { get; set; }
        [JsonIgnore]
        public DateOnly? Date { get; set; }
        public string? DateString => Date?.ToString("yyyy-MM-dd");
        [DataType(DataType.Date)]
        public DateOnly? FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? ToDate { get; set; }
        public Category Category { get; set; }
        public List<int>? CategoryIds { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Asset> Assets { get; set; } = new();
    }
}

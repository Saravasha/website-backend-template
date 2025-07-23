using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GoshehArtWebApp.Models;

namespace GoshehArtWebApp.ViewModels
{
    public class CatalogViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Asset Name")]
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public Asset? Asset { get; set; }
        public Category? Category { get; set; }
        public List<string?>? CategoryNames { get; set; } = new();
        public List<string?>? CatalogNames { get; set; }
        public List<Category?>? Categories { get; set; } = new();
        public List<Asset?>? Assets { get; set; } = new();
        public string? FileUrl { get; set; }
        public string? CategoryName { get; set; }
        [NotMapped]
        public IFormFile FileUp { get; set; }
    }
}

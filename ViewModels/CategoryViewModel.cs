using GoshehArtWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace GoshehArtWebApp.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Asset> Assets { get; set; } = new List<Asset>();

    }
}
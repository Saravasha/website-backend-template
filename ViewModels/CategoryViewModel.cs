using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models;

namespace WebAppBackend.ViewModels
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
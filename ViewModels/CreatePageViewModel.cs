using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models;

namespace WebAppBackend.ViewModels
{
    public class CreatePageViewModel
    {
        
        [Required(ErrorMessage = "Page Title is required")]
        [Display(Name = "Page Title:")]
        public string Title { get; set; }
        [Display(Name = "Page Main Content")]
        public string? Container { get; set; }
        [Display(Name = "Content:")]
        public List<int>? ContentIds { get; set; } = new();
        public List<Content>? Contents { get; set; } = new();
    }
}

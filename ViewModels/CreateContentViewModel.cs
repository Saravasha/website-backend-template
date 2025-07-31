using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebAppBackend.Models;

namespace WebAppBackend.ViewModels
{
    public class CreateContentViewModel
    {

        [Required(ErrorMessage = "Content Title is required")]
        [Display(Name = "Content Title:")]
        public string Title { get; set; }
        [JsonIgnore]
        public DateOnly? Date { get; set; }
        public string? DateString => Date?.ToString("yyyy-MM-dd");
        [Display(Name = "Content Header Image")]
        public string? Container { get; set; }
        [Required(ErrorMessage = "Parent Page is required")]
        [Display(Name = "Parent Page")]
        public int? PageId { get; set; }
        public List<int>? PageIds { get; set; } = new();
        public List<Page>? Pages { get; set; } = new();
    }
}

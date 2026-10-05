using System.ComponentModel.DataAnnotations;

namespace WebAppBackend.ViewModels.Pages
{
    public class CreatePageViewModel
    {
        [Required(ErrorMessage = "Page Title is required")]
        [Display(Name = "Page Title:")]
        public string Title { get; set; } = string.Empty;
        public DateOnly? Date { get; set; }

        [Display(Name = "Page Body")]
        public string? Body { get; set; }

        [Display(Name = "Parent Page")]
        public int? ParentId { get; set; }
        public bool Visible { get; set; } = true;
    }
}
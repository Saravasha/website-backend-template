using System.ComponentModel.DataAnnotations;

namespace WebAppBackend.ViewModels.Pages
{
    public class PageViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Page Title")]
        public string? Title { get; set; }

        [Display(Name = "Page Body")]
        public string? Body { get; set; }

        public int? ParentId { get; set; }

        public int SortOrder { get; set; }

        public DateOnly? Date { get; set; }
        public string? DateString => Date?.ToString("yyyy-MM-dd");
        public List<PageViewModel> Children { get; set; } = new();


        public bool IsOrphan { get; set; }
        public bool Visible { get; set; }

        public bool CanMoveUp { get; set; }
        public bool CanMoveDown { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models;

namespace WebAppBackend.ViewModels
{
    public class PageViewModel
    {

        [Display(Name = "Page Title")]
        public string? Title { get; set; }
        [Display(Name = "Page Container Body")]
        public string? Container { get; set; }
        public Content? Content { get; set; }
        [Display(Name = "Contents")]
        public List<int>? ContentIds { get; set; } = new();
        public List<Content>? Contents { get; set; } = new();
        public List<Page>? Pages { get; set; } = new();
    }
}

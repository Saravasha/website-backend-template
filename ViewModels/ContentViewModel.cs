using System.ComponentModel.DataAnnotations;
using WebAppBackend.Models;

namespace WebAppBackend.ViewModels
{

    public class ContentViewModel
    {

        [Display(Name = "Content Title")]
        public string? Title { get; set; }
        [Display(Name = "Container Body")]
        public string? Container { get; set; }
        public Page? Page { get; set; }
        public int? PageId { get; set; }
        public List<int>? PageIds { get; set; } = new();
        public List<Page>? Pages { get; set; } = new();
        public List<Content>? Contents { get; set; } = new();

    }
}

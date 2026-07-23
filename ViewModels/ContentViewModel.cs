using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebAppBackend.Models;

namespace WebAppBackend.ViewModels
{

    public class ContentViewModel
    {


        public int Id { get; set; }
        [Required]
        [Display(Name = "Content Title")]
        public string Title { get; set; }
        [JsonIgnore]
        public DateOnly? Date { get; set; }
        public string? DateString => Date?.ToString("yyyy-MM-dd");
        [Display(Name = "Container Body")]
        [JsonIgnore]
        public string? Container { get; set; }
        public int? PageId { get; set; }
        public Page? Page { get; set; }

        public List<int>? PageIds { get; set; } = new();
        public List<Page>? Pages { get; set; } = new();
        public List<Content>? Contents { get; set; } = new();

    }
}

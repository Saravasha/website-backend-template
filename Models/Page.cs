using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace WebAppBackend.Models
{
    public class Page
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Page Title")]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "Page Body")]
        public string? Body { get; set; }
        public int? ParentId { get; set; }
        public Page? Parent { get; set; }
        public List<Page> Children { get; set; } = new();
        [JsonIgnore]
        public DateOnly? Date { get; set; }
        public string? DateString => Date?.ToString("yyyy-MM-dd");
        public int SortOrder { get; set; }
        public bool Visible { get; set; }
        public bool IsOrphan { get; set; }

    }
}

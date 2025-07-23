using System.ComponentModel.DataAnnotations;


namespace GoshehArtWebApp.Models
{
    public class Page
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Page Title")]
        public string Title { get; set; }
        [Display(Name = "Container Body")]
        public string? Container { get; set; }
        public List<Content>? Contents { get; set; } = new List<Content>();
    }
}

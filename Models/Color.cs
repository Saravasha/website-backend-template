using System.ComponentModel.DataAnnotations;


namespace GoshehArtWebApp.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string StartColor { get; set; } = "#ffffff";

        public string EndColor { get; set; } = "#ffffff";

        public float Opacity { get; set; } = 1.0f;

        public string DarkStartColor { get; set; } = "#000000";

        public string DarkEndColor { get; set; } = "#000000";

        public float DarkOpacity { get; set; } = 1.0f;

    }
}

using System.ComponentModel.DataAnnotations;


namespace WebAppBackend.ViewModels
{
    public class CreateColorViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Color Name is required")]
        [Display(Name = "Color Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start Color is required")]
        [Display(Name = "Start Color")]
        public string StartColor { get; set; }

        [Required(ErrorMessage = "End Color is required")]
        [Display(Name = "End Color (for Gradient)")]
        public string EndColor { get; set; }

        [Required(ErrorMessage = "Opacity is required")]
        [Display(Name = "Opacity")]
        public float Opacity { get; set; } = 1.0f;

        [Required(ErrorMessage = "Dark Start Color is required")]
        [Display(Name = "Start Color in Dark Mode")]
        public string DarkStartColor { get; set; }
        [Required(ErrorMessage = "Dark End Color is required")]
        [Display(Name = "End Color in Dark Mode (for Gradient)")]
        public string DarkEndColor { get; set; }

        [Required(ErrorMessage = "Dark Opacity is required")]
        [Display(Name = "Opacity in Dark Mode")]
        public float DarkOpacity { get; set; } = 1.0f;
    }
}

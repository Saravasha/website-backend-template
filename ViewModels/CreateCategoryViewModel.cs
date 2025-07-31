using System.ComponentModel.DataAnnotations;
namespace WebAppBackend.ViewModels
{
    public class CreateCategoryViewModel
    {
        //public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Category Name:")]
        public string Name { get; set; }


    }
}

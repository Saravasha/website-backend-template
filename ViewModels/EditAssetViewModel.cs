using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebAppBackend.Models;
namespace WebAppBackend.ViewModels;

public class EditAssetViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Asset Name is required")]
    [Display(Name = "Asset Name:")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Author is required")]
    [Display(Name = "Author:")]
    public string? Author { get; set; }

    [Display(Name = "Description:")]
    public string? Description { get; set; }

    public string? Location { get; set; }
    [JsonIgnore]
    public DateOnly? Date { get; set; }
    public string? DateString => Date?.ToString("yyyy-MM-dd");
    [Display(Name = "Asset Url")]
    public string? FileUrl { get; set; }

    [Display(Name = "Asset File")]
    [NotMapped]

    public IFormFile? FileUp { get; set; }
    public AssetType Type { get; set; }

    public List<int>? CategoryIds { get; set; } = new();

    [Display(Name = "Category:")]
    public List<Category> Categories { get; set; } = new();
    public string? ThumbnailUrl { get; set; }
}
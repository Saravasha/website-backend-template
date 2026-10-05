using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class CreateMultipleAssetsViewModel
{
    [Required(ErrorMessage = "Please upload at least one file.")]
    public List<IFormFile>? FileUp { get; set; }

    public string? Description { get; set; }

    public string? Author { get; set; }

    public string? Location { get; set; }

    public DateOnly? Date { get; set; }

    public string? FileUrl { get; set; }

    [Required(ErrorMessage = "At least one category is required.")]
    public List<string> Categories { get; set; } = new();

    public bool IsResponse { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public string? ThumbnailUrl { get; set; }
}

using GoshehArtWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace GoshehArtWebApp.ViewModels
{
    public class UploadAssetsViewModel : ResponseModel
    {


        [Required(ErrorMessage = "Select files to upload to the server")]
        public List<IFormFile> FilesUp { get; set; }
        public List<Category>? Categories { get; set; }
    }
}

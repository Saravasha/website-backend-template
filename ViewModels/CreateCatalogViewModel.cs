using GoshehArtWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Azure;

namespace GoshehArtWebApp.ViewModels
{
    public class CreateCatalogViewModel : ResponseModel
    {
         
        [Required(ErrorMessage = "Select files to upload to the server")]
        public List<IFormFile> FilesUp { get; set; }

  
    }
}

using WebAppBackend.Models;

namespace WebAppBackend.ViewModels
{
    public class DetailsAssetViewModel
    {

        public Asset Asset { get; set; } = null!;
        public string? TextContent { get; set; }
    }
}

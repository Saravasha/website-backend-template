using WebAppBackend.Models;

namespace WebAppBackend.ViewModels.Assets
{
    public class DetailsAssetViewModel
    {

        public Asset Asset { get; set; } = null!;
        public string? TextContent { get; set; }
    }
}

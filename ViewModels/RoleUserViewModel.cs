using Microsoft.AspNetCore.Identity;

namespace GoshehArtWebApp.ViewModels
{
    public class RoleUserViewModel : IdentityUser
    {
        public List<string> Roles { get; set; }
    }
}

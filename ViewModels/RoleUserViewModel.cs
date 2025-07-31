using Microsoft.AspNetCore.Identity;

namespace WebAppBackend.ViewModels
{
    public class RoleUserViewModel : IdentityUser
    {
        public List<string> Roles { get; set; }
    }
}

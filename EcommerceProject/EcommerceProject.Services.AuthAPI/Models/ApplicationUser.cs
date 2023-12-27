using Microsoft.AspNetCore.Identity;
namespace EcommerceProject.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

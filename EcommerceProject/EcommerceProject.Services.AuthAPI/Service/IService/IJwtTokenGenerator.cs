using EcommerceProject.Services.AuthAPI.Models;

namespace EcommerceProject.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}

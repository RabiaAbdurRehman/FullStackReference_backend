using FullStackReference.Service.AuthAPI.Models;

namespace FullStackReference.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}

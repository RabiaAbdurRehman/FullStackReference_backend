using FullStackReference.Service.AuthAPI.Models.Dto;

namespace FullStackReference.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        //AllUserDto UserInfo();
        Task<AllUserDto>  UserInfo();
        

    }
}

using FullStackReference.Service.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FullStackReference.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        //AllUserDto UserInfo();
        Task<String> UserInfo();
        

    }
}

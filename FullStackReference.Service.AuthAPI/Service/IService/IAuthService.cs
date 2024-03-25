using FullStackReference.Service.AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FullStackReference.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
      
        Task<String> UserInfo(string UserId);
        Task<bool> UpdateUserInfo(UserDto updateRequestDto);
        Task<bool> DeleteUser(string userid);
        Task<bool> UpdatePassword(string userid, string currentPasswd, string newPassword);


    }
}

using Azure;
using FullStackReference.Service.AuthAPI.Data;
using FullStackReference.Service.AuthAPI.Models;
using FullStackReference.Service.AuthAPI.Models.Dto;
using FullStackReference.Service.IService;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace FullStackReference.Service.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
                _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            try
            {
                if (user != null)
                {
                    if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                    {
                        //create role if it does not exist
                        _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                    }
                    await _userManager.AddToRoleAsync(user, roleName);
                   

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(string userid)
        {
            bool status=false;
            try
            {
               // var user = _db.ApplicationUsers.FirstOrDefault(u => u.Id.ToLower() == userid);
                var user = await _userManager.FindByIdAsync(userid);
                //var roles = _userManager.GetRolesAsync(user);
                //var result = await _userManager.RemoveFromRolesAsync(user, (IEnumerable<string>)roles);
                if (user!=null)
                {
                    var resultdelete = await _userManager.DeleteAsync(user);
                    if (resultdelete.Succeeded)
                    {
                        status = true;
                    }
                   
                }
                return status;


            }
            catch(Exception ex)
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
           // var user2 = _db.ApplicationUsers.ToListAsync();

            bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

            if(user==null || isValid == false)
            {
                return new LoginResponseDto() { User = null,Token="" };
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user,roles);
           

            UserDto userDTO = new()
            {
                ID = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Role = roles[0]
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                FirstName = registrationRequestDto.FirstName,
                LastName = registrationRequestDto.LastName,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            try
            {
                var result =await  _userManager.CreateAsync(user,registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    await AssignRole(registrationRequestDto.Email, "Normal");
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        FirstName = userToReturn.FirstName,
                        LastName = userToReturn.LastName,
                        PhoneNumber = userToReturn.PhoneNumber,
                    };

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }

        public async  Task<bool> UpdatePassword(string userid, string currentPasswd,string newPassword)
        {
            bool status = false;
            try
            {
                var user = await _userManager.FindByIdAsync(userid);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, currentPasswd, newPassword);
                    if (result.Succeeded)
                    {
                        status = true;
                    }

                }
            }
            catch
            { status = false; }
            return status;  
            
        }

        public async Task<bool> UpdateUserInfo(UserDto updateRequestDto)
        {
            bool status = false;
            var userToReturn = _db.ApplicationUsers.First(u => u.Id == updateRequestDto.ID);
           // var user = await _userManager.FindByIdAsync(id);
            try
            {
                if (userToReturn != null)
                {
                    userToReturn.Email = updateRequestDto.Email;
                    userToReturn.NormalizedEmail = updateRequestDto.Email.ToUpper();
                    userToReturn.FirstName = updateRequestDto.FirstName;
                    userToReturn.LastName = updateRequestDto.LastName;
                    userToReturn.PhoneNumber = updateRequestDto.PhoneNumber;
                    var result = await _userManager.UpdateAsync(userToReturn);
                    if (result.Succeeded)
                    {
                        status= true;
                    }
                    else
                    {
                        status= false;
                    }
                }
               
            }
            catch (Exception ex)
            {
                status= false;
            }

            return status;
        }

        public async Task<string> UserInfo(string UserId)
        {

           // var odel = await _db.ApplicationUsers.Include(x => x.UserRoles).ThenInclude(x=>x.Role).ToListAsync();
            // var user = _db.ApplicationUsers.ToList();
           


            AllUserDto user_w = new();

           /* var objList2 = await (from user2 in _db.Users
                                  join userRole in _db.UserRoles
                                  on user2.Id equals userRole.UserId
                                  join role in _db.Roles
                                  on userRole.RoleId equals role.Id
                                  select new AllUserDto
                                  {
                                      Name = user2.Name,
                                      ID = user2.Id,
                                      Email = user2.Email,
                                      Role = role.Name,
                                      PhoneNumber = user2.PhoneNumber
                                  }).ToListAsync(); */
            //foreach (var obj in objList2)
            //{
            //    user_w=new AllUserDto()
            //    {
            //        Name = obj.Name,
            //        ID = obj.ID,
            //        Email = obj.Email,
            //        Role = obj.Role,
            //        PhoneNumber = obj.PhoneNumber
            //    };
            //}

            //var users = await _userManager.Users
            //                      .Include(r => r.UserRoles)
            //                      .ThenInclude(r => r.Role)
            //                      // .OrderBy(u => u.UserName)
            //                      .Select(u => new
            //                      {
            //                          // u.Id,
            //                          Username = u.UserName,
            //                          Name = u.Name,
            //                          Role = u.UserRoles.Select(r => r.Role.Name).ToList()
            //                      })
            //                      .ToListAsync();

            return "";

            //var user3 = await _db.ApplicationRoles.ToListAsync();
            //string roleAssign = "";
            //    var roles = await _userManager.GetRolesAsync(user);
            //    if(roles.Any())
            //    {
            //        foreach(var role in roles)
            //        {
            //            roleAssign = roleAssign + ",";
            //        }
            //    }
            //     userDTO = new()
            //    {
            //        Email = user.Email,
            //        ID = user.Id,
            //        Name = user.Name,
            //        PhoneNumber = user.PhoneNumber,
            //        Role = roleAssign
            //    };

            //}


            //var users2 = await _db.AspNetUsers
            //             .Include(x => x.AspNetUserRoles)
            //             .ThenInclude(x => x.AspNetRoles)
            //             .AsNoTracking()
            //              .ToListAsync();

            //foreach (var user in users)
            //{
            //    allUserDtos.Role = user.UserName;
            //    allUserDtos.Add(new AllUserDto(UserName = user.UserName,
            //        Id = user.Id,
            //        Email = user.Email,
            //        Role = user.Role,));

            //    allUserDtos.Add(new AllUserDto
            //    {
            //        UserName= user.UserName,
            //        Id= user.Id,
            //        Email = user.Email,
            //        Role= user.Role,
            //    });


            //}




            // var users = _db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Name == "USER"))


            // var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() != "");
            //// var roles = await _userManager.GetRolesAsync(user);
            //UserDto userDTO = new()
            //{
            //    Email = "",
            //    ID = "",
            //    Name = "",
            //    PhoneNumber = ""
            //};

            //LoginResponseDto allUserDtos = new LoginResponseDto()
            //{
            //    User = userDTO,
            //    Token = ""
            //};

            //return null ;
        }
    }
}

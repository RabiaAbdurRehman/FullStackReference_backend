
using FullStackReference.Service.AuthAPI.Data;
using FullStackReference.Service.AuthAPI.Models.Dto;
using FullStackReference.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackReference.Service.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        //private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        protected ResponseDto _response;
        private readonly AppDbContext _db;
        public AuthAPIController(IAuthService authService, IConfiguration configuration, AppDbContext db)
        {
            _db = db;
            _authService = authService;
            _configuration = configuration;
           // _messageBus = messageBus;
            _response = new();
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {

            var errorMessage = await _authService.Register(model);
           
            if (!string.IsNullOrEmpty(errorMessage) )
            {
                _response.IsSuccess = false;
                _response.Message= errorMessage;
                _response.Result = "Faild";
                return BadRequest(_response);
            }
            // await _messageBus.PublishMessage(model.Email, _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"));
            _response.Message = "User Created Successfully";
            _response.Result = "Success";
          return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }
        [HttpPut("updateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserDto model)
        {
            var updateResponse = await _authService.UpdateUserInfo(model);
            if (!updateResponse)
            {
                _response.IsSuccess = false;
                _response.Message = "Not Updated. Please Try Again!";
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.Message = "User Info Updated";
            _response.Result = updateResponse;
            return Ok(_response);

        }
        [HttpGet("AllUsers/{userId}")]
        public async Task<IActionResult> AllUsers(string userId)
        {
            //var odel = await _db.ApplicationUsers.Include(x => x.UserRoles).ThenInclude(x => x.Role).ToListAsync();
            var objList2 = await (from user2 in _db.Users
                                  join userRole in _db.UserRoles
                                  on user2.Id equals userRole.UserId
                                  join role in _db.Roles
                                  on userRole.RoleId equals role.Id
                                  where user2.Id == userId
                                  select new AllUserDto
                                  {
                                      ID = user2.Id,
                                      FirstName = user2.FirstName,
                                      LastName = user2.LastName,
                                      Email = user2.Email,
                                      Role = role.Name,
                                      PhoneNumber = user2.PhoneNumber
                                  }).ToListAsync();
           // var loginResponse = _authService.UserInfo();
            
            //if (loginResponse.User == null)
            //{
            //    _response.IsSuccess = false;
            //    _response.Message = "User";
            //    //return BadRequest(_response);
            //}
            _response.Result = objList2;
            return Ok(_response);

        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);

        }
        [HttpPut("updatePassword/{userId}/{currentPassword}/{newPassword}")]
        public async Task<IActionResult> UpdatePassword(string userId,string currentPassword,string newPassword)
        {

            var callResponse = await _authService.UpdatePassword(userId, currentPassword, newPassword);
            if (!callResponse)
            {
                _response.IsSuccess = false;
                _response.Message = "Not Deleted. Please Try Again!";
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.Message = "User Deleted";
            _response.Result = callResponse;
            return Ok(_response);
        }


    }
}

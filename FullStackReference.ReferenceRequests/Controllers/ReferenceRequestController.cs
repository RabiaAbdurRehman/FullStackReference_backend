using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FullStackReference.ReferenceRequests.Service;
using FullStackReference.ReferenceRequests.Models;
using FullStackReference.ReferenceRequests.IService;


namespace FullStackReference.ReferenceRequests.Controllers

{
    //attributes and controller declaration
    [Route("api")]
    [ApiController]
    public class ReferenceRequestController : ControllerBase //inheriting from controllerbase
    {
        private readonly IReferenceRequestsService _referenceRequestsService;
        private readonly IConfiguration _configuration;
        //protected ResponseDto _response;

        //constructor

        public ReferenceRequestController(
            IReferenceRequestsService referenceRequestsService,
            IConfiguration configuration
        )
        {
            _referenceRequestsService = referenceRequestsService;
            _configuration = configuration;
            _response = new();
        }

        // API end points to get current user. such as a GET request to the endpoint /api/current_user

        [HttpGet("current_user")]
        public async Task<IActionResult> GetCurrentUserData()
        {
            try
            {
                var userData = await _referenceRequestsService.GetCurrentUserData();
                if (userData != null)
                {
                    return Ok(userData);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // a GET request to the endpoint /api/reference-requests

        [HttpGet("reference-requests")]
        public async Task<IActionResult> GetReferenceRequest()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not logged in");
            }
            if (!User.IsInRole("Mentor"))
            {
                return Forbid("User does not have the required role");
            }
            try
            {
                var referenceRequest = await _referenceRequestService.GetReferenceRequest();
                return Ok(referenceRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET request to the endpoint /api/mentor-requests/{mentorId}

        [HttpGet("mentor-requests/{mentorId}")]
        public async Task<IActionResult> GetMentorReferenceRequest(int mentorId)
        {
            try
            {
                var mentorReferenceRequests = await _referenceRequestService.GetMentorReferenceRequests(mentorId);

                return Ok(mentorReferenceRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //a POST request to the endpoint /api/create-request

        [HttpPost("create-request")]
        public async Task<IActionResult> CreateReferenceRequest(
            [FromBody] ReferenceRequest model
        )
        {
            // Implement logic to create a new reference request
            var errorMessage = await _referenceRequestService.CreateReferenceRequest(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                _response.Result = "Failed";
                return BadRequest(_response);
            }

            _response.Message = "Reference Request Created Successfully";
            _response.Result = "Success";
            return Ok(_response);
        }
    }
}

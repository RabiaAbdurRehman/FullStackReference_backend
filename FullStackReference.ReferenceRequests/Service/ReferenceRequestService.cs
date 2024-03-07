using System;
using System.Security.Claims; // for ClaimTypes
using System.Threading.Tasks;
using FullStackReference.ReferenceRequests.Data;
using FullStackReference.ReferenceRequests.IService;
using FullStackReference.ReferenceRequests.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; // for IActionResult, UnauthorizedObjectResult, ForbidResult, OkObjectResult

namespace FullStackReference.ReferenceRequests.Service
{
    public class ReferenceRequestService : IReferenceRequestsService //interface
    {
        private readonly IReferenceRequestsRepository _repository; // storing and retrieving data from a database.
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReferenceRequestService(
            IReferenceRequestsRepository repository,
            IHttpContextAccessor httpContextAccesso
        )
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        //member of the class.

        public async Task<IActionResult> CreateReferenceRequest(ReferenceRequest model)
        {
            try
            {
                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return new UnauthorizedObjectResult("User is not logged in");
                }
                if (!_httpContextAccessor.HttpContext.User.IsInRole("Mentor"))
                {
                    return new ForbidResult("User does not have the required role");
                }

                var currentUserData = await GetCurrentUserData();
                var referenceRequest = new ReferenceRequest
                {
                    NewbId = model.NewbId,
                    MentorId = int.Parse(currentUserData.UserId), // Assuming UserId is a string
                    DateRequested = DateTime.Now,
                    DateProvided = null,
                    Accepted = null,
                    Rejected = null
                };
                //Call the repository to store the reference request in the database
                await _repository.CreateReferenceRequest(referenceRequest);

                return new OkObjectResult("Reference request created successfully");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        // public async Task<string> AssignRole(string email, string roleName)
        // {
        //     // This method may not be necessary for your reference request functionality
        //     // Keep it if you plan to extend this service for handling roles in the reference request context

        //     try
        //     {
        //         // Implement any logic related to role assignment if needed
        //         // You may need to extend your reference request model to include role-related information
        //         return "Role assigned successfully";
        //     }
        //     catch (Exception ex)
        //     {
        //         // Log the exception or handle it as needed
        //         return "Error encountered while assigning role";
        //     }
        // }
        //for a single reference request by id,
        public async Task<IActionResult> GetReferenceRequest(int referenceRequestId)
        {
            try
            {
                var referenceRequest = await _repository.GetReferenceRequest(referenceRequestId);

                return referenceRequest != null
                    ? Ok(referenceRequest)
                    : NotFound($"Reference request with ID {referenceRequestId} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        public async Task<UserDataDto> GetCurrentUserData()
        {
            var currentUser = _httpContextAccessor.HttpContext.User; //current user associated with the current request.
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = currentUser.Identity?.Name;
            var userRole = currentUser.FindFirst(ClaimTypes.Role)?.Value;

            var userData = new UserDataDto
            {
                UserId = userId,
                UserName = userName,
                UserRole = userRole
            };

            return userData;
        }

        public async Task<IActionResult> GetMentorReferenceRequests(int mentorId)
        {
            try
            {
                var mentorReferenceRequests = await _repository.GetMentorReferenceRequests(
                    mentorId
                );

                return mentorReferenceRequests != null
                    ? Ok(mentorReferenceRequests)
                    : NotFound("Mentor reference requests not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

// methods related to managing reference requests,
// such as creating a reference request, getting reference requests, and handling mentor-specific requests.
// Example assuming these types are in the "FullStackReference.ReferenceRequests.Models" namespace
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackReference.ReferenceRequests.Models;
using Microsoft.AspNetCore.Mvc; // for IActionResult, UnauthorizedObjectResult, ForbidResult, OkObjectResult
using System.Security.Claims;     // for ClaimTypes
namespace FullStackReference.ReferenceRequests.IService
{

public interface IReferenceRequestsService
{
    Task<IActionResult> CreateReferenceRequest(ReferenceRequest model);
    Task<IActionResult> GetReferenceRequest(int referenceRequestId);
    Task<IEnumerable<ReferenceRequest>> GetMentorReferenceRequests(int mentorId);
    Task<UserDataDto> GetCurrentUserData();

}
}
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackReference.ReferenceRequests.IService;
using FullStackReference.ReferenceRequests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc; // for IActionResult, UnauthorizedObjectResult, ForbidResult, OkObjectResult
using System.Security.Claims;     // for ClaimTypes

namespace FullStackReference.ReferenceRequests.Data
{
    public class ReferenceRequestsRepository : IReferenceRequestsRepository
    {
        private readonly ApplicationDbContext _context;

        public ReferenceRequestsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateReferenceRequest(ReferenceRequest referenceRequest)
        {
            _context.ReferenceRequest.Add(referenceRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReferenceRequest>> GetReferenceRequests()
        {
            return await _context.ReferenceRequest.ToListAsync();
        }

        public async Task<IEnumerable<ReferenceRequest>> GetMentorReferenceRequests(int mentorId)
        {
            return await _context
                .ReferenceRequest.Where(request => request.MentorId == mentorId)
                .ToListAsync();
        }

        public async Task<ReferenceRequest> GetReferenceRequest(int referenceRequestId)
        {
            return await _context.ReferenceRequest.FirstOrDefaultAsync(request =>
                request.Id == referenceRequestId
            );
        }
    }
}

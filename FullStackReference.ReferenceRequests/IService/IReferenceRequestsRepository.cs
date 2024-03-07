//creating, reading, updating, or deleting reference requests in the database.
// Example assuming these types are in the "FullStackReference.ReferenceRequests.Models" namespace
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackReference.ReferenceRequests.Models;

namespace FullStackReference.ReferenceRequests.IService
{

public interface IReferenceRequestsRepository
{
    Task CreateReferenceRequest(ReferenceRequest referenceRequest);
    //Task<IEnumerable<ReferenceRequest>> GetReferenceRequest();
    Task<IEnumerable<ReferenceRequest>> GetMentorReferenceRequests(int mentorId);
    Task<ReferenceRequest> GetReferenceRequest(int referenceRequestId);

}
}
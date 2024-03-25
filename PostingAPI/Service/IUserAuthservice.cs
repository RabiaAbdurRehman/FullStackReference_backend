using PostingAPI.Models.Dto;

namespace PostingAPI.Service
{
    public interface IUserAuthservice
    {
        Task<IEnumerable<UserAuthDto>> GetUsersInfo(string UserId);
    }
}

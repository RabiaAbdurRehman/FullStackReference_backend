using Newtonsoft.Json;
using PostingAPI.Models.Dto;

namespace PostingAPI.Service
{
    public class UserAuthService : IUserAuthservice
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserAuthService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<IEnumerable<UserAuthDto>> GetUsersInfo()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("UsersInfo");
                var response = await client.GetAsync($"api/auth/AllUsers");
                var apiContet = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
                if (resp.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<UserAuthDto>>(Convert.ToString(resp.Result));
                }
                return new List<UserAuthDto>();
            }
            catch (Exception ex)
            {
                return new List<UserAuthDto>();
            }
           
        }
    }
}

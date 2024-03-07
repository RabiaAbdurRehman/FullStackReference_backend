using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingAPI.Data;
using PostingAPI.Models;
using PostingAPI.Models.Dto;

namespace PostingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostingDetailsAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public PostingDetailsAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<PostingDetails> objList = _db.PostingDetails.ToList();
                _response.Result = _mapper.Map<IEnumerable<PostingDetailsDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost]
        //   [Authorize(Roles = "ADMIN")]
        public ResponseDto Post(PostingDetailsDto PostingDetailsDto)
        {
            try
            {
                PostingDetails postingDetails = _mapper.Map<PostingDetails>(PostingDetailsDto);
                _db.PostingDetails.Add(postingDetails);
                _db.SaveChanges();
                _response.Result = _mapper.Map<PostingDetails>(postingDetails);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}

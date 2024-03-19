using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostingAPI.Data;
using PostingAPI.Models;
using PostingAPI.Models.Dto;
using PostingAPI.Service;

namespace PostingAPI.Controllers
{
    [Route("api/Posting")]
    [ApiController]
    public class PostingAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        // For User Services
        private IUserAuthservice _userAuthservice;

        public PostingAPIController(AppDbContext db, IMapper mapper, IUserAuthservice userAuthservice)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
            _userAuthservice = userAuthservice;
        }
        

        [HttpGet]
        // public async ResponseDto Get()
        public async Task<ResponseDto>? Get()
        {
            IEnumerable<Posting> objList;
            try
            {
                //IEnumerable<Posting> objList = _db.Posting.ToList();
                //_response.Result = _mapper.Map<IEnumerable<PostingDto>>(objList);
                // var cusomter = await _db.Posting
                //    .Include(_ => _.PostingDetails).ToListAsync();
                IEnumerable<UserAuthDto> userAuthDtos = await _userAuthservice.GetUsersInfo() ;
                objList = _db.Posting.Include(p => p.PostDetails).ToList();
                
                _response.Result = _mapper.Map<IEnumerable<Posting>>(objList);
               // return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
           
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            IEnumerable<Posting> objList;
           
            try
            {
               
                //objList = _db.Posting.First(u => u.PostingId == id).Include(p => p.PostDetails).ToList();
                objList = _db.Posting.Include(p => p.PostDetails).Where(_=>_.PostingId==id).ToList();
                _response.Result = _mapper.Map<IEnumerable<Posting>>(objList);
                // return Ok(_response);
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
        public ResponseDto Post(PostInsertDto postingDto)
        {
            try
            {
                Posting posting = _mapper.Map<Posting>(postingDto);
                _db.Posting.Add(posting);
                _db.SaveChanges();
                _response.Result = _mapper.Map<PostInsertDto>(posting);
                _response.Message = "Post Succesfully Inserted";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostingDetails")]
        public async Task<ResponseDto> PostingDetails([FromBody] PostingDetailsDto postingDetailsDto)
        {
            try
            {
                //OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == orderId);
               
                Posting posting = _db.Posting.FirstOrDefault(u => u.PostingId == postingDetailsDto.PostingId);
                if(posting != null)
                {
                    _response.IsSuccess = true;
                    PostingDetails postingDetails = _mapper.Map<PostingDetails>(postingDetailsDto);
                    _db.PostingDetails.Add(postingDetails);
                    _db.SaveChanges();
                   // _response.Result = _mapper.Map<PostInsertDto>(posting);
                    _response.Message = "Post Succesfully Inserted";
                }
                
               // _response.Result = "";
                _response.Message = "Post Succesfully Inserted";
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

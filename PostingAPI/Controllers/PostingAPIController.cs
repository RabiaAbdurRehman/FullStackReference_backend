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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<ResponseDto>? Get(
             int page = 1,
        int pageSize = 50)
        {
            IEnumerable<Posting> objList;
           try
            {
                objList = _db.Posting.Include(p => p.PostDetails).AsQueryable();
                objList = objList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                foreach (var posting in objList)
                {
                    var userDto1 = await _userAuthservice.GetUsersInfo(posting.UserId);
                    posting.userdto = userDto1;
                    foreach (var postDetail in posting.PostDetails)
                    {
                        var userDto2 = await _userAuthservice.GetUsersInfo(postDetail.UserId);
                        postDetail.userdtoDetails = userDto2;
                    }

                }
               
                _response.Result = _mapper.Map<IEnumerable<Posting>>(objList);
               _response.Result= objList;
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

       
        [HttpPut("editPost")]
        public ResponseDto EditPost([FromBody]EditPostDto editPostDto)
        {
            try
            {
                var entityToUpdate = _db.Posting.FirstOrDefault(_ => _.PostingId == editPostDto.Id);

                if (entityToUpdate != null)
                {
                    // Step 2: Modify the properties of the retrieved entity
                    entityToUpdate.PostContent = editPostDto.Content;
                    entityToUpdate.PostingDate = editPostDto.PostingDate;
                    // Update other properties as needed

                    // Step 3: Save the changes back to the database
                    _db.SaveChanges();
                }
                else
                {
                    // Handle case when entity is not found
                }
                
                //_response.Result = _mapper.Map<PostingDto>(posting);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete("deletePost/{PostingId}")]
        public ResponseDto deletePost(int PostingId)
        {
            try
            {
                var entityToUpdate = _db.Posting.FirstOrDefault(_ => _.PostingId == PostingId);

                if (entityToUpdate != null)
                {
                    // Step 2: Modify the properties of the retrieved entity
                    entityToUpdate.DeletionFlag = 1;
                    entityToUpdate.PostingDate= DateTime.Now;
                   
                    // Update other properties as needed

                    // Step 3: Save the changes back to the database
                    _db.SaveChanges();
                }
                else
                {
                    // Handle case when entity is not found
                }

                //_response.Result = _mapper.Map<PostingDto>(posting);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("deletePostingComment/{PostingDetailId}")]
        public ResponseDto DeletePostingComment(int PostingDetailId)
        {
            try
            {
                var entityToUpdate = _db.PostingDetails.FirstOrDefault(_ => _.PostingDetailsId == PostingDetailId);

                if (entityToUpdate != null)
                {
                   _db.PostingDetails.Remove(entityToUpdate);
                    _db.SaveChanges();
                }
                else
                {
                    // Handle case when entity is not found
                }

                //_response.Result = _mapper.Map<PostingDto>(posting);

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

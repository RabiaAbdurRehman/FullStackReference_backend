using AutoMapper;
using Azure;
using FullStackReference.Service.PostingAPI;
using FullStackReference.Service.PostingAPI.Data;
using FullStackReference.Service.PostingAPI.Modal;
using FullStackReference.Service.PostingAPI.Modal.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FullStackReference.Service.PostingAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class PostingAPIController : ControllerBase
    {

        [HttpGet]
        public string GetAllPost()
        {
            return "All Post";
        }
        [HttpGet("{id}")]
        public string GetPostById(int id)
        {
            return "Post By ID";
        }
        [HttpPost]
        public string CreatePost()
        {
            return "Create Post";
        }
    }
}

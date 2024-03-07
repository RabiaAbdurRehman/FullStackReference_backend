using AutoMapper;
using FullStackReference.Service.PostingAPI.Modal;
using FullStackReference.Service.PostingAPI.Modal.Dto;

namespace FullStackReference.Service.PostingAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PostingDto, Posting>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}

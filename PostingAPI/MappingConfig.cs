using AutoMapper;
using PostingAPI.Models;
using PostingAPI.Models.Dto;

namespace PostingAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PostingDto, Posting>().ReverseMap();
                //config.CreateMap<PostingDetailsDto,PostingDetails>().ReverseMap();
                config.CreateMap<PostInsertDto, Posting>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}

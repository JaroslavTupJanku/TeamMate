using AutoMapper;
using TeamMateApi.Models.DTOs;
using TeamMateServer.Data.Entities;

namespace TeamMateApi.Models
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<PlayerDTO, PlayerEntity>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PlayerEntity, PlayerDTO>();
            CreateMap<TeamEntity, TeamDTO>().ReverseMap();
        }
    }
}

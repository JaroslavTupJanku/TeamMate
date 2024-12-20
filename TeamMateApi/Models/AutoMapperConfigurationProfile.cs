﻿using AutoMapper;
using TeamMateApi.Models.DTOs;
using TeamMateServer.Data.Entities;

namespace TeamMateApi.Models
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<PlayerEntity, PlayerDTO>().ReverseMap();
            CreateMap<TeamEntity, TeamDTO>().ReverseMap();
        }
    }
}

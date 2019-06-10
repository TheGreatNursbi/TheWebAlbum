using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attempt2.DTOs;
using Attempt2.Models;

namespace Attempt2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, ApplicationUserDTO>();
            Mapper.CreateMap<ApplicationUserDTO, ApplicationUser>();
            Mapper.CreateMap<ImageDTO, Image>();
            Mapper.CreateMap<Image, ImageDTO>();
        }
    }
}
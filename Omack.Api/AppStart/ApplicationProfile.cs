using AutoMapper;
using Omack.Data.Models;
using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omack.Api.AppStart
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Group, GroupServiceModel>().ReverseMap();
            CreateMap<Item, ItemServiceModel>().ReverseMap();
            CreateMap<Media, MediaServiceModel>().ReverseMap();
            CreateMap<User, UserServiceModel>().ReverseMap();
        }
    }
}

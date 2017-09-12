using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Omack.Api.ViewModels;
using Omack.Api.ViewModels.Group;
using Omack.Data.Models;
using Omack.Services.Models;
using Omack.Services.Models.Group;
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
            CreateMap<GroupViewPM, GroupServicePM>().ReverseMap();
            CreateMap<GroupViewGM, GroupServiceGM>().ReverseMap();

            CreateMap<ItemServiceModel, Item>().ReverseMap();
            CreateMap<ItemServiceModel, ItemVM>().ReverseMap();

            CreateMap<Media, MediaServiceModel>().ReverseMap();

            CreateMap<User, UserServiceModel>().ReverseMap();

            CreateMap<Transaction, TransactionServiceModel>().ReverseMap();

            CreateMap<Notification, NotificationServiceModel>().ReverseMap();

            CreateMap(typeof(JsonPatchDocument<>), typeof(JsonPatchDocument<>));
            CreateMap(typeof(Operation<>), typeof(Operation<>));
        }
    }
}

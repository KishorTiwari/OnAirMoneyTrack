using AutoMapper;
using Omack.Data.Models;
using Omack.Services.Models;
using Omack.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omack.Web.AppStart
{
    public class ApplicationProfile: Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Group, GroupServiceModel>().ReverseMap();
            CreateMap<GroupViewModel, GroupServiceModel>().ReverseMap();
        }
    }
}

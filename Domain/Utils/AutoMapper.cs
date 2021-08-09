using System;
using AutoMapper;
using Domain.Common.ApplicationTask;
using Domain.Common.ApplicationUser;
using Domain.Entities;

namespace Domain.Utils
{
    // TODO: Have to separate all Entities automapper.
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<ApplicationTask, CreateApplicationTask>();
            CreateMap<CreateApplicationTask, ApplicationTask>()
                .BeforeMap((updateTask,applicationTask)=>applicationTask.CreatedDateTime = DateTime.Now);
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
        }
    }
}
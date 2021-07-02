using AutoMapper;
using Domain.Common.ApplicationTask;
using Domain.Entities;

namespace Domain.Utils
{
    // TODO: Have to separate all Entities automapper.
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<ApplicationTask, CreateApplicationTask>();
            CreateMap<CreateApplicationTask, ApplicationTask>();
            CreateMap<UpdateApplicationTask, ApplicationTask>();
        }
    }
}
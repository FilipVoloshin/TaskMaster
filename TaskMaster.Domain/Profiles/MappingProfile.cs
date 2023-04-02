using AutoMapper;
using TaskMaster.Application.ViewModels;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskList, TaskListVm>().ReverseMap();
        }
    }
}

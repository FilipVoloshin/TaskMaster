using AutoMapper;
using TaskMaster.Application.MediatR.TaskLists.Commands;
using TaskMaster.Application.ViewModels;
using TaskMaster.Infrastructure.Entities;

namespace TaskMaster.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskList, TaskListVm>().ReverseMap();
            CreateMap<CreateTaskListCommand, TaskList>()
                .ForMember(dest => dest.CreatedAtUtc, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}

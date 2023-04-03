using AutoMapper;
using TaskMaster.Application.MediatR.AssignedTaskLists.Command;
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
                .ForMember(dest => dest.CreatedAtUtc, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));
            CreateMap<UpdateTaskListCommand, TaskList>().ReverseMap();
            CreateMap<CreateAssignedTaskListCommand, AssignedTaskList>();

            CreateMap<AssignedTaskList, AssigneeVm>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Assignee.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Assignee.Id));
        }
    }
}

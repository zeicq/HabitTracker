using Application.Features.Habits.Queries;
using Application.Features.Habits.Queries.All;
using Application.Features.Schedules.Queries;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Habit, HabitViewModel>().ReverseMap();
        CreateMap<Schedule, ScheduleViewModel>().ReverseMap();
    }

}
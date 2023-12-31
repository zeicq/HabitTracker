using Application.Features.Habits.Command.Create;
using Application.Features.Habits.Command.Update;
using Application.Features.Habits.Queries;
using Application.Features.Schedules.Command.Create;
using Application.Features.Schedules.Command.Update;
using Application.Features.Schedules.Queries;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HabitViewModel, Habit>().ReverseMap();
        CreateMap<CreateHabitCommand, Habit>().ReverseMap();
        CreateMap<UpdateHabitCommand, Habit>().ReverseMap();

        CreateMap<ScheduleViewModel, Schedule>().ReverseMap();
        CreateMap<UpdateScheduleCommand, Schedule>().ReverseMap();
        CreateMap<CreateScheduleCommand, Schedule>().ReverseMap();
    }
}
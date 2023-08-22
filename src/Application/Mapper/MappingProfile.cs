using Application.Features.Habits.Queries;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Habit, HabitViewModel>().ReverseMap();
    }

}
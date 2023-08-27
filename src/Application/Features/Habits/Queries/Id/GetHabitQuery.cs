using Application.Features.Habits.Queries.All;
using Domain.Entity;
using MediatR;

namespace Application.Features.Habits.Queries.Id;

public class GetHabitQuery : IRequest<HabitViewModel>
{
    public int Id { get; set; }
}
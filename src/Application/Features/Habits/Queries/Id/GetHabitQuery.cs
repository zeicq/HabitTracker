using Domain.Entity;
using MediatR;

namespace Application.Features.Habits.Queries.Id;

public class GetHabitQuery : IRequest<Habit>
{
    public int Id { get; set; }
}
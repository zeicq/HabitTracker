using Application.Shared;
using Domain.Entity;
using MediatR;

namespace Application.Features.Habits.Command.Create;

public class CreateHabitCommand : IRequest<Response<Habit>>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
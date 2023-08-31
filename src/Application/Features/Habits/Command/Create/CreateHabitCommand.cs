using Application.Features.Habits.Queries;
using Application.Shared;
using Domain.Entity;
using MediatR;

namespace Application.Features.Habits.Command.Create;

public class CreateHabitCommand : IRequest<Response<HabitViewModel>>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
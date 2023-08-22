using MediatR;

namespace Application.Features.Habits.Command.Create;

public class CreateHabitCommand : IRequest
{
    public string Name { get; set; }
}
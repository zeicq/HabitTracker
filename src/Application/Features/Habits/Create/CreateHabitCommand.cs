using MediatR;

namespace Application.Features.Habit.Create;

public class CreateHabitCommand : IRequest
{
    public string Name { get; set; }
}
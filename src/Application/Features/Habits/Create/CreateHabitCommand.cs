using MediatR;

namespace Application.Features.Habits.Create;

public class CreateHabitCommand : IRequest
{
    public string Name { get; set; }
}
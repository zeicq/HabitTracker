using MediatR;

namespace Application.Features.Habits.Command.Update;

public class UpdateHabitCommand: IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
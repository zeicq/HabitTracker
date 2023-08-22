using MediatR;

namespace Application.Features.Habits.Command.Delete;

public class DeleteHabitCommand:IRequest
{
    public int Id { get; set; }
}
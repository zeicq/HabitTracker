using Application.Shared;
using MediatR;

namespace Application.Features.Habits.Command.Update;

public class UpdateHabitCommand: IRequest<Response<Unit>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
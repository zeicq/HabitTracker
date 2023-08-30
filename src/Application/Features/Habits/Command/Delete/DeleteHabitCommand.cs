using Application.Shared;
using MediatR;

namespace Application.Features.Habits.Command.Delete;

public class DeleteHabitCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
}
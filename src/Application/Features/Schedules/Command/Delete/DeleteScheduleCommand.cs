using Application.Shared;
using MediatR;

namespace Application.Features.Schedules.Command.Delete;

public class DeleteScheduleCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
}
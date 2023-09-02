using Application.Features.Habits.Command.Delete;
using Application.Shared;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Schedules.Command.Delete;

public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, Response<Unit>>
{
    private readonly IScheduleRepository _repository;

    public DeleteScheduleCommandHandler(IScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<Unit>> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _repository.GetByIdAsync(request.Id) ??
                       throw new KeyNotFoundException("Not schedule found");
        
        await _repository.DeleteAsync(schedule);
        return new Response<Unit>(Unit.Value);
    }
}
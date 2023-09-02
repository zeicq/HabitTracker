using Application.Shared;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Schedules.Command.Update;

public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, Response<Unit>>
{
    private readonly IScheduleRepository _repository;
    private readonly IMapper _mapper;

    public UpdateScheduleCommandHandler(IScheduleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _repository.GetByIdAsync(request.HabitId) ??
                       throw new KeyNotFoundException("Schedule not found");
        _mapper.Map(request, schedule);
        await _repository.UpdateAsync(schedule);
        return new Response<Unit>(Unit.Value);
    }
}
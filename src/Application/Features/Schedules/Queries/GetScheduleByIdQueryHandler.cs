using System.Net;
using Application.Exceptions;
using Application.Features.Habits.Queries.All;
using Application.Features.Habits.Queries.Id;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Schedules.Queries;

public class GetScheduleQueryHandler : IRequestHandler<GetScheduleByIdQuery, ScheduleViewModel>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public GetScheduleQueryHandler(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<ScheduleViewModel> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(request.Id) ??
                       throw new KeyNotFoundException();

        return _mapper.Map<ScheduleViewModel>(schedule);
    }
}
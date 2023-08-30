using Application.Features.Habits.Queries.All;
using Application.Features.Habits.Queries.Id;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Schedules.Queries;

public class GetScheduleQueryHandler : IRequestHandler<GetScheduleQuery, ScheduleViewModel>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public GetScheduleQueryHandler(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<ScheduleViewModel> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(request.Id);
        return _mapper.Map<ScheduleViewModel>(schedule);
    }
}
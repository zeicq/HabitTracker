using System.Globalization;
using Application.Features.Habits.Command.Create;
using Application.Features.Schedules.Queries;
using Application.Helpers;
using Application.Shared;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Schedules.Command.Create;

public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, Response<ScheduleViewModel>>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public CreateScheduleCommandHandler(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Response<ScheduleViewModel>> Handle(CreateScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var schedule = _mapper.Map<Schedule>(request);

        var createdSchedule = await _scheduleRepository.AddAsync(schedule);
        var viewModelSchedule = _mapper.Map<ScheduleViewModel>(createdSchedule);

        return new Response<ScheduleViewModel>(viewModelSchedule);
    }
}
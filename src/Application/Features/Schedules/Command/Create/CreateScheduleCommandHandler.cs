using System.Globalization;
using Application.Features.Habits.Command.Create;
using Application.Helpers;
using Application.Shared;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Schedules.Command.Create;

public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, Response<Schedule>>
{
    private readonly IScheduleRepository _scheduleRepository;

    public CreateScheduleCommandHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Response<Schedule>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
      
        var newSchedule = new Schedule
        {
            DaysOfWeek = request.DaysOfWeek,
            HabitId = request.HabitId,
            TimeOfDay = TimeSpan.ParseExact((request.TimeOfDay), "HH:mm", null)
        };

        var addedSchedule = await _scheduleRepository.AddAsync(newSchedule);

        var response = new Response<Schedule>(addedSchedule);
        return await Task.FromResult(response);
    }
}
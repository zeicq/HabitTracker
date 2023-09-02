using Application.Features.Habits.Queries;
using Application.Features.Schedules.Notifiaction;
using Application.Shared;
using AutoMapper;
using Domain.Entity;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.ScheduleEntries.Add;

public class CreateScheduleEntryNotificationHandler : INotificationHandler<ScheduleCreatedNotification>
{
    private readonly IScheduleEntryRepository _repository;

    public CreateScheduleEntryNotificationHandler(IScheduleEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ScheduleCreatedNotification request, CancellationToken cancellationToken)
    {
        var daysEnum = request.ScheduleViewModel.DaysOfWeek;
        var startDate = request.ScheduleViewModel.StartData;
        var timeOfDay = request.ScheduleViewModel.TimeOfDay;
        var scheduleId = request.ScheduleViewModel.Id;

        int maxEntries = 66;
        int dayCount = 1;

        while (maxEntries > 0)
        {
            if (daysEnum.Contains((DaysOfWeekEnum)startDate.DayOfWeek))
            {
                await _repository.AddAsync(new ScheduleEntry
                {
                    Date = startDate + timeOfDay,
                    Day = dayCount,
                    IsCompleted = false,
                    ScheduleId = scheduleId
                });

                maxEntries--;
                dayCount++;
            }

            startDate = startDate.AddDays(1);
        }
    }
}
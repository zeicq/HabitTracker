using Application.Features.Schedules.Queries;
using Application.Shared;
using Domain.Entity;
using MediatR;

namespace Application.Features.Schedules.Notifiaction;

public class ScheduleCreatedNotification:INotification
{
    public ScheduleViewModel ScheduleViewModel { get; set; }
}
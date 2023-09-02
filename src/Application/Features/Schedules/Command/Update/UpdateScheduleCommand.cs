using System.ComponentModel;
using Application.Shared;
using Domain.Enums;
using MediatR;

namespace Application.Features.Schedules.Command.Update;

public class UpdateScheduleCommand: IRequest<Response<Unit>>
{
    public int HabitId { get; set; }
    public ICollection<DaysOfWeekEnum> DaysOfWeek { get; set; }
    [DefaultValue("HH:MM")] 
    public string TimeOfDay { get; set; }
 
}
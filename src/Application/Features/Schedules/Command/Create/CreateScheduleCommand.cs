using System.ComponentModel;
using Application.Shared;
using Domain.Entity;
using Domain.Enums;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;


namespace Application.Features.Schedules.Command.Create;

public class CreateScheduleCommand : IRequest<Response<Schedule>>
{
    public ICollection<DaysOfWeekEnum> DaysOfWeek { get; set; }
    
    [DefaultValue("HH:MM")] 
    public string TimeOfDay { get; set; }
    public int HabitId { get; set; }
}
using System.Text.RegularExpressions;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Schedules.Command.Update;

public class UpdateScheduleValidator : AbstractValidator<UpdateScheduleCommand>
{
    private readonly IScheduleRepository _scheduleRepository;

    public UpdateScheduleValidator(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;

        RuleFor(schedule => schedule.DaysOfWeek)
            .NotEmpty()
            .Must(daysOfWeek => daysOfWeek.All(day => (int)day >= 0 && (int)day <= 6))
            .WithMessage("Days of the week must be between 0 and 6 and separated by commas, example: [1,2]");

        RuleFor(schedule => schedule.TimeOfDay)
            .NotEmpty()
            .WithMessage("Time of day must not be empty")
            .Must(BeValidTimeFormat)
            .WithMessage("Invalid time format. Use HH:mm");
    }


    private bool BeValidTimeFormat(string time)
    {
        return Regex.IsMatch(time, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
    }
}
using System.Text.RegularExpressions;
using Application.Features.Habits.Command.Create;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Schedules.Command.Create;

public class CreateScheduleValidation : AbstractValidator<CreateScheduleCommand>
{
    private readonly IScheduleRepository _scheduleRepository;

    public CreateScheduleValidation(IScheduleRepository scheduleRepository)
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

        RuleFor(schedule => schedule.HabitId)
            .MustAsync(async (habitId, cancellationToken) => await IsUniqueScheduleAsync(habitId))
            .WithMessage("Schedule with the HabitId already exists");
    }

    private async Task<bool> IsUniqueScheduleAsync(int habitId)
    {
        return await _scheduleRepository.IsUniqueScheduleAsync(habitId);
    }

    private bool BeValidTimeFormat(string time)
    {
        return Regex.IsMatch(time, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
    }
}
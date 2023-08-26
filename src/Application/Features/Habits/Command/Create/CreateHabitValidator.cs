using System.Text.RegularExpressions;
using Domain.Entity;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Habits.Command.Create;

public class CreateHabitValidator : AbstractValidator<CreateHabitCommand>
{
    private readonly IHabitRepository _habitRepository;

    public CreateHabitValidator(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;

        RuleFor(habit => habit.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("{PropertyName} must  exceed 3 characters.")
            .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.")
            .MustAsync(IsUniqueHabit).WithMessage("{PropertyName} already exists.")
            .Matches(new Regex("^[A-Za-z0-9]*$")).WithMessage("{PropertyName} can only contain letters and numbers.")
            .NotEqual("string").WithMessage("{PropertyName} cannot be 'string'.Change the name.")
            .Must(name => char.IsUpper(name[0])).WithMessage("{PropertyName} must start with an uppercase letter.")
            .Must(name => !name.Contains("@")).WithMessage("{PropertyName} cannot contain the letter '@'.");
    }

    private async Task<bool> IsUniqueHabit(string habit, CancellationToken cancellationToken)
    {
        return await _habitRepository.IsUniqueHabitAsync(habit);
    }
}
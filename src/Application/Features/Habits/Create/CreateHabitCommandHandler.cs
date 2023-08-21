using Domain.Interfaces;
using MediatR;
using Domain.Entity;

namespace Application.Features.Habits.Create;

public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand>
{
    private readonly IHabitRepository _habitRepository;

    public CreateHabitCommandHandler(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public async Task Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = new Habit()
        {
            Name = request.Name
        };

        await _habitRepository.AddAsync(habit);
    }
}
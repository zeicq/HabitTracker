using Domain.Interfaces;
using MediatR;
using Domain.Entity;

namespace Application.Features.Habit.Create;

public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand>
{
    private readonly IHabitRepository _habitRepository;

    public CreateHabitCommandHandler(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public async Task Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = new Domain.Entity.Habit()
        {
            Name = request.Name
        };

        await _habitRepository.AddAsync(habit);

        return Unit.Value;
    }
}
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Command.Update;

public class UpdateHabitCommandHandler : IRequestHandler<UpdateHabitCommand>
{
    private readonly IHabitRepository _repository;

    public UpdateHabitCommandHandler(IHabitRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = await _repository.GetByIdAsync(request.Id);

        if (habit == null)
        {
            throw new Exception("Habit not found");
        }

        habit.Name = request.Name;
        habit.Description = request.Description;

        await _repository.UpdateAsync(habit);
    }
}
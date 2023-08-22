using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Command.Delete;

public class DeleteHabitCommandHandler : IRequestHandler<DeleteHabitCommand>
{
    private readonly IHabitRepository _repository;

    public DeleteHabitCommandHandler(IHabitRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        var existingHabit = await _repository.GetByIdAsync(request.Id);

        if (existingHabit == null)
        {
            throw new Exception("Habit not found");
        }

        await _repository.DeleteAsync(existingHabit);
    
    }
}
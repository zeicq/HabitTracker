using Application.Exceptions;
using Application.Shared;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Command.Delete;

public class DeleteHabitCommandHandler : IRequestHandler<DeleteHabitCommand,  Response<Unit>>
{
    private readonly IHabitRepository _repository;

    public DeleteHabitCommandHandler(IHabitRepository repository)
    {
        _repository = repository;
    }

    public async Task <Response<Unit>> Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = await _repository.GetByIdAsync(request.Id);
        if (habit == null) throw new KeyNotFoundException("Not habit found");
        await _repository.DeleteAsync(habit);
        return new Response<Unit>(Unit.Value);
    }
}
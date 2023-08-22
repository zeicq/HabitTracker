using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Queries;

public class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, List<Habit>>
{
    private readonly IHabitRepository _repository;
    public GetHabitsQueryHandler(IHabitRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Habit>> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
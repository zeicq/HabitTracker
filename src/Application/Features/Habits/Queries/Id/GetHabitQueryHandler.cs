using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Queries.Id;

public class GetHabitQueryHandler : IRequestHandler<GetHabitQuery, Habit>
{
    private readonly IHabitRepository _habitRepository;

    public GetHabitQueryHandler(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public async Task<Habit> Handle(GetHabitQuery query, CancellationToken cancellationToken)
    {
        var habit = await _habitRepository.GetByIdAsync(query.Id);
        return habit;
    }
}
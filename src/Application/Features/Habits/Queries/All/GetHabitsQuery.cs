using Domain.Entity;
using MediatR;

namespace Application.Features.Habits.Queries.All;

public class GetHabitsQuery:IRequest<List<HabitViewModel>>
{
    
}
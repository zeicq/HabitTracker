using Domain.Entity;
using MediatR;

namespace Application.Features.Habits.Queries;

public class GetHabitsQuery:IRequest<List<Habit>>
{
    
}
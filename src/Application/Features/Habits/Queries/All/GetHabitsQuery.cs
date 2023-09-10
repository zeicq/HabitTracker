using Application.Shared;
using Domain.Entity;
using MediatR;

namespace Application.Features.Habits.Queries.All;

public class GetHabitsQuery: IRequest<Response<PageResponse<List<HabitViewModel>>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
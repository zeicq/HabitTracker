using Application.Features.Habits.Queries.All;
using Domain.Entity;
using MediatR;

namespace Application.Features.Schedules.Queries;

public class GetScheduleByIdQuery: IRequest<ScheduleViewModel>
{
    public int Id { get; set; }
}
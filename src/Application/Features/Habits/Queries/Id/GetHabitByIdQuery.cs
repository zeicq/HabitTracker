using Application.Features.Habits.Queries.All;
using Application.Shared;
using Domain.Entity;
using MediatR;

namespace Application.Features.Habits.Queries.Id;

public class GetHabitByIdQuery : IRequest<Response<HabitViewModel>>
{
    public int Id { get; set; }
}
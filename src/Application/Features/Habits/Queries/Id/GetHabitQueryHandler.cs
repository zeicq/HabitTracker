using Application.Features.Habits.Queries.All;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Queries.Id;

public class GetHabitQueryHandler : IRequestHandler<GetHabitQuery, HabitViewModel>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IMapper _mapper;

    public GetHabitQueryHandler(IHabitRepository habitRepository, IMapper mapper)
    {
        _habitRepository = habitRepository;
        _mapper = mapper;
    }

    public async Task<HabitViewModel> Handle(GetHabitQuery query, CancellationToken cancellationToken)
    {
        var habit = await _habitRepository.GetByIdAsync(query.Id);
        return _mapper.Map<HabitViewModel>(habit);
    }
}
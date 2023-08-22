using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Queries.All;

public class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, List<HabitViewModel>>
{
    private readonly IHabitRepository _repository;
    private readonly IMapper _mapper;

    public GetHabitsQueryHandler(IHabitRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<HabitViewModel>> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        var habitLists= await _repository.GetAllAsync();
        return _mapper.Map<List<HabitViewModel>>(habitLists);
    }
}
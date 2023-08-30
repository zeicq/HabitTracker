using Application.Features.Habits.Queries.All;
using Application.Shared;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Queries.Id;

public class GetHabitByIdQueryHandler : IRequestHandler<GetHabitByIdQuery, Response<HabitViewModel>>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IMapper _mapper;

    public GetHabitByIdQueryHandler(IHabitRepository habitRepository, IMapper mapper)
    {
        _habitRepository = habitRepository;
        _mapper = mapper;
    }

    public async Task<Response<HabitViewModel>> Handle(GetHabitByIdQuery query, CancellationToken cancellationToken)
    {
        var habit = await _habitRepository.GetByIdAsync(query.Id);
       var mapObject= _mapper.Map<HabitViewModel>(habit);
       
       return new Response<HabitViewModel>(mapObject);
    }
}
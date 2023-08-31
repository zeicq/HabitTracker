using Application.Features.Habits.Queries;
using Application.Shared;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Domain.Entity;

namespace Application.Features.Habits.Command.Create;

public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand, Response<HabitViewModel>>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IMapper _mapper;

    public CreateHabitCommandHandler(IHabitRepository habitRepository, IMapper mapper)
    {
        _habitRepository = habitRepository;
        _mapper = mapper;
    }

    public async Task<Response<HabitViewModel>> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var newHabit = _mapper.Map<Habit>(request);
        var createdHabit = await _habitRepository.AddAsync(newHabit);
    
        var viewModelHabit = _mapper.Map<HabitViewModel>(createdHabit);
    
        var response = new Response<HabitViewModel>(viewModelHabit);
        return response;
    }
}
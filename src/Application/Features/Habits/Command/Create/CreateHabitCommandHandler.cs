using Application.Features.Habits.Queries;
using Application.Shared;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Domain.Entity;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Features.Habits.Command.Create;

public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand, Response<HabitViewModel>>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public CreateHabitCommandHandler(IHabitRepository habitRepository, IMapper mapper,IMemoryCache memoryCache)
    {
        _habitRepository = habitRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public async Task<Response<HabitViewModel>> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var userId =  _memoryCache.Get<string>("UserId");;
        var newHabit = new Habit
        {
            UserId = userId,
            Name = request.Name,
            Description = request.Description
        };

        var createdHabit = await _habitRepository.AddAsync(newHabit);
        var viewModelHabit = _mapper.Map<HabitViewModel>(createdHabit);

        return new Response<HabitViewModel>(viewModelHabit);
    }
}
using Application.Shared;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Command.Update;

public class UpdateHabitCommandHandler : IRequestHandler<UpdateHabitCommand, Response<Unit>>
{
    private readonly IHabitRepository _repository;
    private readonly IMapper _mapper;

    public UpdateHabitCommandHandler(IHabitRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(UpdateHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException("Habit not found");
        _mapper.Map(request, habit);
        await _repository.UpdateAsync(habit);
        return new Response<Unit>(Unit.Value);
    }
}
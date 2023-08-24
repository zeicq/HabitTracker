using Application.Shared;
using Domain.Interfaces;
using MediatR;
using Domain.Entity;

namespace Application.Features.Habits.Command.Create;

public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand, Response<Habit>>
{
    private readonly IHabitRepository _habitRepository;

    public CreateHabitCommandHandler(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public async Task<Response<Habit>> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var newHabit = new Habit { Name = request.Name };
        var response = new Response<Habit>(newHabit);
        return await Task.FromResult(response);
    }
}
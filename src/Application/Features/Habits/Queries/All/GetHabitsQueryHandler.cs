using Application.Shared;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Habits.Queries.All;

public class GetHabitsQueryHandler : IRequestHandler<GetHabitsQuery, Response<PageResponse<List<HabitViewModel>>>>
{
    private readonly IHabitRepository _repository;
    private readonly IMapper _mapper;

    public GetHabitsQueryHandler(IHabitRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<PageResponse<List<HabitViewModel>>>> Handle(GetHabitsQuery request, CancellationToken cancellationToken)
    {
        var habitLists= await _repository.GetPagedReponseAsync(request.PageNumber,request.PageSize);
        habitLists.Select(item => item.Description ?? "").ToList();


        var habitViewModels =_mapper.Map<List<HabitViewModel>>(habitLists);
        return new Response<PageResponse<List<HabitViewModel>>>(new PageResponse<List<HabitViewModel>>(habitViewModels, request.PageNumber, request.PageSize));
    }
}
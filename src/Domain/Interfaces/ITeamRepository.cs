using Domain.Entity;

namespace Domain.Interfaces;

public interface ITeamRepository : IGenericRepositoryBaseAsync<Team>
{
    Task<bool> IsUniqueTeamAsync(string name);
}
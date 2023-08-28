using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TeamRepository: GenericRepositoryBaseAsync<Team>,ITeamRepository
{
    private readonly MssqlDbContext _dbContext;

    public TeamRepository(MssqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUniqueTeamAsync(string name)
    {
        throw new Exception();
    }
}
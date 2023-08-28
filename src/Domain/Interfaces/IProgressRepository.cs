using Domain.Entity;

namespace Domain.Interfaces;

public interface IProgressRepository
{
    Task<Progress> GetByIdAsync(int id);
    Task<Progress> AddAsync(Progress progress);
    Task UpdateAsync(Progress progress);
}
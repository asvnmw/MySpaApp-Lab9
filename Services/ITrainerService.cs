using MySpaApp.Models;

namespace MySpaApp.Services;

public interface ITrainerService
{
    Task<List<Trainer>> GetAllAsync();
    Task<Trainer?> GetByIdAsync(int id);
    Task AddAsync(Trainer trainer);
    Task UpdateAsync(Trainer trainer);
    Task DeleteAsync(int id);
}
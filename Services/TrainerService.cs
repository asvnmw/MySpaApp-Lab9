using MySpaApp.Models;

namespace MySpaApp.Services;

public class TrainerService : ITrainerService
{
    private static List<Trainer> _trainers = new()
    {
        new Trainer { Id = 2001, LastName = "Русин", FirstName = "Павел", Sport = "Велоспорт", Experience = 10, Salary = 12500 },
        new Trainer { Id = 2002, LastName = "Калинин", FirstName = "Пётр", Sport = "Тяжёлая атлетика", Experience = 18, Salary = 13000 },
        new Trainer { Id = 2003, LastName = "Волков", FirstName = "Александр", Sport = "Лёгкая атлетика", Experience = 11, Salary = 20000 }
    };

    public Task<List<Trainer>> GetAllAsync()
    {
        return Task.FromResult(_trainers.ToList());
    }

    public Task<Trainer?> GetByIdAsync(int id)
    {
        return Task.FromResult(_trainers.FirstOrDefault(t => t.Id == id));
    }

    public Task AddAsync(Trainer trainer)
    {
        trainer.Id = _trainers.Any() ? _trainers.Max(t => t.Id) + 1 : 2001;
        _trainers.Add(trainer);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Trainer trainer)
    {
        var existing = _trainers.FirstOrDefault(t => t.Id == trainer.Id);
        if (existing != null)
        {
            existing.LastName = trainer.LastName;
            existing.FirstName = trainer.FirstName;
            existing.Sport = trainer.Sport;
            existing.Experience = trainer.Experience;
            existing.Salary = trainer.Salary;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        _trainers.RemoveAll(t => t.Id == id);
        return Task.CompletedTask;
    }
}
using Application.Interfaces;
using Domain;

namespace Tests;

public class MockUserRepository : IUserRepository
{
    public readonly List<User> _data;

    public MockUserRepository()
    {
        _data = new List<User>() { new User("Admin", "admin", "admin", 0, new DateOnly(1980, 7, 8), true, "Admin") };
    }

    public Task<List<User>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.ToList());
    }

    public Task<List<User>> GetAllActive(CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.Where(u => !u.IsRestored).OrderBy(u => u.CreatedOn).ToList());
    }

    public Task<List<User>> GetAllOverTheAge(int age, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var cutoffDate = today.AddYears(-age);
        return Task.FromResult(_data.Where(u => u.Birthday.HasValue && u.Birthday.Value <= cutoffDate).ToList());

    }

    public Task<User?> GetByGuid(Guid guid, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.FirstOrDefault(u => u.Guid == guid));
    }

    public Task<User?> GetByLogin(string login, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.FirstOrDefault(u => u.Login == login));
    }

    public Task<User?> GetByLoginAndPassword(string login, string password, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.FirstOrDefault(u => u.Login == login && u.Password == password && u.RestoredOn == null));
    }

    public async Task<Guid> Add(User user, CancellationToken cancellationToken)
    {
        _data.Add(user);
        return user.Guid;
    }

    public Task Delete(User user, CancellationToken cancellationToken)
    {
        _data.Remove(user);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAll(CancellationToken cancellationToken);
    Task<List<User>> GetAllActive(CancellationToken cancellationToken);
    Task<List<User>> GetAllOverTheAge(int age, CancellationToken cancellationToken);
    Task<User?> GetByGuid(Guid guid, CancellationToken cancellationToken);
    Task<User?> GetByLogin(string login, CancellationToken cancellationToken);
    Task<User?> GetByLoginAndPassword(string login, string password, CancellationToken cancellationToken);
    Task<Guid> Add(User user, CancellationToken cancellationToken);
    Task Delete(User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
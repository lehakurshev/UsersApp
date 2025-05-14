using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Users
            .ToListAsync(cancellationToken);
    }

    public  async Task<List<User>> GetAllActive(CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => !u.IsRestored)
            .OrderBy(u => u.CreatedOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<User>> GetAllOverTheAge(int age, CancellationToken cancellationToken)
    {
        
        var today = DateOnly.FromDateTime(DateTime.Today);
        var cutoffDate = today.AddYears(-age);

        return await _context.Users
            .Where(u => u.Birthday.HasValue && u.Birthday.Value <= cutoffDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByGuid(Guid guid, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Guid == guid, cancellationToken);
    }

    public async Task<User?> GetByLogin(string login, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == login,cancellationToken);
    }

    public async Task<User?> GetByLoginAndPassword(string login, string password, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Login == login && u.Password == password && u.RestoredOn == null, cancellationToken);
    }

    public async Task<Guid> Add(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        return user.Guid;
    }


    public async Task Delete(User user, CancellationToken cancellationToken)
    {
        _context.Users.Remove(user);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
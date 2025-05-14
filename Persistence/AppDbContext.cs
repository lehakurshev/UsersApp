using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.TypeConfiguration;

namespace Persistence;

public sealed class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        var host       = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var port       = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        var db         = Environment.GetEnvironmentVariable("DB_NAME") ?? "usersdb";
        var username         = Environment.GetEnvironmentVariable("DB_USER_NAME") ?? "postgres";
        var password   = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
        var connString = $"Host={host};Port={port};Database={db};Username={username};Password={password}";

        optionsBuilder.UseNpgsql(connString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
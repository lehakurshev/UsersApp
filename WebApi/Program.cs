using Domain;
using Persistence;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();

                if (context.Users.FirstOrDefault(u => u.Login == "Admin") == null)
                {
                    var admin = new User("Admin", "admin", "admin", 0, new DateOnly(1980, 7, 8), true, "Admin");
                    context.Users.Add(admin);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
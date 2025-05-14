using Application.Interfaces;

namespace Application.Users;

public class BaseUserRequestHandler
{
    protected readonly IUserRepository UserRepository;

    protected BaseUserRequestHandler(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }
}
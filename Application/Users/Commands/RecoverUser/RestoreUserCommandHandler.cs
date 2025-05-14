using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.RecoverUser;

public class RestoreUserCommandHandler : BaseUserRequestHandler, IRequestHandler<RestoreUserCommand>
{
    public RestoreUserCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(RestoreUserCommand request, CancellationToken cancellationToken)
    {
        var modifier = await UserRepository.GetByLogin(request.RestoreBy, cancellationToken);
        if (modifier == null)
        {
            throw new NotFoundException();
        }

        if (modifier.Login != "Admin")
        {
            throw new ForbiddenAccessException();
        }
        
        var user = await UserRepository.GetByLogin(request.Login, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException();
        }
        
        user.IsRestored = false;
        user.RestoredBy = null;
        user.RestoredOn = null;
        await UserRepository.SaveChangesAsync(cancellationToken);
    }
}
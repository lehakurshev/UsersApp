using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.RevokeUser;

public class RevokeUserCommandHandler : BaseUserRequestHandler, IRequestHandler<RevokeUserCommand>
{
    public RevokeUserCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(RevokeUserCommand request, CancellationToken cancellationToken)
    {
        var modifier = await UserRepository.GetByLogin(request.RevokeBy, cancellationToken);
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

        if (user.IsRestored)
        {
            throw new AlreadyRevokeException();
        }

        user.IsRestored = true;
        user.RestoredBy = request.RevokeBy;
        user.RestoredOn = DateTime.Now;
        await UserRepository.SaveChangesAsync(cancellationToken);
    }
}
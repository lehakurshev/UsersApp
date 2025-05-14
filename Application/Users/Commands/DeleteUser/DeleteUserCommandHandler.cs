using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : BaseUserRequestHandler, IRequestHandler<DeleteUserCommand>
{
    public DeleteUserCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var modifier = await UserRepository.GetByLogin(request.DeleteBy, cancellationToken);
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
        
        await UserRepository.Delete(user, cancellationToken);
        await UserRepository.SaveChangesAsync(cancellationToken);
    }
}
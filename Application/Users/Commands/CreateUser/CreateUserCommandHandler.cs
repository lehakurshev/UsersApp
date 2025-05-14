using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : BaseUserRequestHandler, IRequestHandler<CreateUserCommand, Guid>
{
    public CreateUserCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await UserRepository.GetByLogin(request.Login, cancellationToken);
        if (user != null)
        {
            throw new AlreadyCreatedException();
        }
        user = new User
        (
            request.Login,
            request.Password,
            request.Name,
            request.Gender,
            request.Birthday,
            request.IsAdmin,
            request.CreatedBy
        );
        var guid = await UserRepository.Add(user, cancellationToken);
        await UserRepository.SaveChangesAsync(cancellationToken);
        return guid;
    }
}
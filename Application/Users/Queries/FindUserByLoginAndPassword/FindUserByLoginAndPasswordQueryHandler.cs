using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Users.Queries.FindUserByLoginAndPassword;

public class FindUserByLoginAndPasswordQueryHandler : BaseUserRequestHandler, IRequestHandler<FindUserByLoginAndPasswordQuery, User>
{
    public FindUserByLoginAndPasswordQueryHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<User> Handle(FindUserByLoginAndPasswordQuery request, CancellationToken cancellationToken)
    {
        var modifier = await UserRepository.GetByLogin(request.QueryBy, cancellationToken);
        if (modifier == null)
        {
            throw new NotFoundException();
        }

        if (modifier.Login != request.QueryBy)
        {
            throw new ForbiddenAccessException();
        }
        
        var user = await UserRepository.GetByLogin(request.Login, cancellationToken);
        if (user.Password != request.Password)
        {
            throw new ForbiddenAccessException();
        }
        if (user == null)
        {
            throw new NotFoundException();
        }
        return user;
    }
}
using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Users.Queries.FindUserByLogin;

public class FindUserByLoginQueryHandler : BaseUserRequestHandler, IRequestHandler<FindUserByLoginQuery, User>
{
    public FindUserByLoginQueryHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<User> Handle(FindUserByLoginQuery request, CancellationToken cancellationToken)
    {
        var modifier = await UserRepository.GetByLogin(request.QueryBy, cancellationToken);
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
        return user;
    }
}
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Users.Queries.GetUsersOverTheAge;

public class GetUsersOverTheAgeQueryHandler : BaseUserRequestHandler, IRequestHandler<GetUsersOverTheAgeQuery, List<User>>
{
    public GetUsersOverTheAgeQueryHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<List<User>> Handle(GetUsersOverTheAgeQuery request, CancellationToken cancellationToken)
    {
        return await UserRepository.GetAllOverTheAge(request.Age, cancellationToken);
    }
}
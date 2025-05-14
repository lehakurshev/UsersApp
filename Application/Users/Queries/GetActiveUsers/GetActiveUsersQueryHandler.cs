using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Users.Queries.GetActiveUsers;

public class GetActiveUsersQueryHandler : BaseUserRequestHandler, IRequestHandler<GetActiveUsersQuery, List<User>>
{
    public GetActiveUsersQueryHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<List<User>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
    {
        return await UserRepository.GetAllActive(cancellationToken);
    }
}
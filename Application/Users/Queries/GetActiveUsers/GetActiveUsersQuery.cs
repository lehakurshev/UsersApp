using Domain;
using MediatR;

namespace Application.Users.Queries.GetActiveUsers;

public class GetActiveUsersQuery : IRequest<List<User>>
{
    
}
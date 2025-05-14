using Domain;
using MediatR;

namespace Application.Users.Queries.GetUsersOverTheAge;

public class GetUsersOverTheAgeQuery : IRequest<List<User>>
{
    public int Age { get; set; }
}
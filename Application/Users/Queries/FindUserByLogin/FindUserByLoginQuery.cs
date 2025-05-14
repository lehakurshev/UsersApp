using Domain;
using MediatR;

namespace Application.Users.Queries.FindUserByLogin;

public class FindUserByLoginQuery : IRequest<User>
{
    public string Login { get; set; }
    public string? QueryBy { get; set; }
}
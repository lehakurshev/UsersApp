using Domain;
using MediatR;

namespace Application.Users.Queries.FindUserByLoginAndPassword;

public class FindUserByLoginAndPasswordQuery : IRequest<User>
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string QueryBy { get; set; }
}
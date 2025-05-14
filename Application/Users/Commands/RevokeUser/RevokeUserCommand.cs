using MediatR;

namespace Application.Users.Commands.RevokeUser;

public class RevokeUserCommand : IRequest
{
    public string Login { get; set; }
    public string RevokeBy { get; set; }
}
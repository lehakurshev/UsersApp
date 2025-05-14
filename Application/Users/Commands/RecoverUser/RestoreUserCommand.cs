using MediatR;

namespace Application.Users.Commands.RecoverUser;

public class RestoreUserCommand : IRequest
{
    public string Login { get; set; }
    public string RestoreBy { get; set; }
}
using MediatR;

namespace Application.Users.Commands.UpdatePassword;

public class UpdatePasswordCommand : IRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string ModifiedBy { get; set; }
}
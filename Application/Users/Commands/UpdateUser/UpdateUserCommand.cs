using MediatR;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public string Login { get; set; }
    public string Name { get; set; }
    public int Gender { get; set; }
    public DateOnly? Birthday { get; set; }
    public string ModifiedBy { get; set; }
}
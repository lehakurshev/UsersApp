using MediatR;

namespace Application.Users.Commands.UpdateLogin;

public class UpdateLoginCommand : IRequest
{
    public string CurrentLogin { get; set; }
    public string NewLogin { get; set; }
    public string ModifiedBy { get; set; }
}
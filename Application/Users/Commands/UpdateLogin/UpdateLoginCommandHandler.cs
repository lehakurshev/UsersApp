using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.UpdateLogin;

public class UpdateLoginCommandHandler : BaseUserRequestHandler,  IRequestHandler<UpdateLoginCommand>
{
    public UpdateLoginCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(UpdateLoginCommand request, CancellationToken cancellationToken)
    {
        var modifier = await UserRepository.GetByLogin(request.ModifiedBy, cancellationToken);
        if (modifier == null)
        {
            throw new NotFoundException();
        }

        if (modifier.Login != "Admin" && (modifier.Login != request.CurrentLogin || modifier.IsRestored))
        {
            throw new ForbiddenAccessException();
        }
        
        var user = await UserRepository.GetByLogin(request.CurrentLogin, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException();
        }
        user.Login = request.NewLogin;
        user.ModifiedBy = request.ModifiedBy;
        user.ModifiedOn = DateTime.Now;
        await UserRepository.SaveChangesAsync(cancellationToken);
    }
}
using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.UpdatePassword;

public class UpdatePasswordCommandHandler : BaseUserRequestHandler, IRequestHandler<UpdatePasswordCommand>
{
    public UpdatePasswordCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var modifier = await UserRepository.GetByLogin(request.ModifiedBy, cancellationToken);
        if (modifier == null)
        {
            throw new NotFoundException();
        }

        if (modifier.Login != "Admin" && (modifier.Login != request.Login || modifier.IsRestored))
        {
            throw new ForbiddenAccessException();
        }
        
        var user = await UserRepository.GetByLogin(request.Login, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException();
        }
        user.Password = request.Password;
        user.ModifiedBy = request.ModifiedBy;
        user.ModifiedOn = DateTime.Now;
        await UserRepository.SaveChangesAsync(cancellationToken);
    }
}
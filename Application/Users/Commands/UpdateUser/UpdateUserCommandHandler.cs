using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : BaseUserRequestHandler, IRequestHandler<UpdateUserCommand>
{
    public UpdateUserCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
        user.Name = request.Name;
        user.Gender = request.Gender;
        user.Birthday = request.Birthday;
        user.ModifiedBy = request.ModifiedBy;
        user.ModifiedOn = DateTime.UtcNow;
        await UserRepository.SaveChangesAsync(cancellationToken);
    }
}
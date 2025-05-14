using Application.Users.Commands.UpdatePassword;
using Application.Users.Commands.UpdateUser;
using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordValidator()
    {
        RuleFor(command => command.Password)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Password должен содержать только английские буквы и цифры.");
        
        RuleFor(command => command.ModifiedBy)
            .NotEmpty().WithMessage("ModifiedBy обязателен");
    }
}
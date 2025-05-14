using Application.Users.Commands.UpdateLogin;
using Application.Users.Commands.UpdatePassword;
using Application.Users.Commands.UpdateUser;
using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class UpdateLogindValidator : AbstractValidator<UpdateLoginCommand>
{
    public UpdateLogindValidator()
    {
        RuleFor(command => command.NewLogin)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("NewLogin должен содержать только английские буквы и цифры.");
        
        RuleFor(command => command.ModifiedBy)
            .NotEmpty().WithMessage("ModifiedBy обязателен");
    }
}
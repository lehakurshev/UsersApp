using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(command => command.Login)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("NewLogin должен содержать только английские буквы и цифры.");

        RuleFor(command => command.Password)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Password должен содержать только английские буквы и цифры.");
        
        RuleFor(command => command.Name)
            .NotEmpty()
            .Matches("^[a-zA-Zа-яА-Я]+$")
            .WithMessage("Name должен содержать только английские буквы и цифры.");

        RuleFor(command => command.Gender)
            .NotEmpty()
            .Must(gender => gender is 0 or 1 or 2)
            .WithMessage("Пол должен быть 0 (женщина), 1 (мужчина) или 2 (неизвестно).");

        RuleFor(command => command.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy обязателен");
        
        RuleFor(command => command.Birthday)
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Дата рождения должна быть меньше текущей даты.");
    }
}
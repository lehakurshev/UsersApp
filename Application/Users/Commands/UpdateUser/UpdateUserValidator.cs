using Application.Users.Commands.UpdateUser;
using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .Matches("^[a-zA-Zа-яА-Я]+$")
            .WithMessage("Name должен содержать только английские буквы и цифры.");

        RuleFor(command => command.Gender)
            .NotEmpty()
            .Must(gender => gender is 0 or 1 or 2)
            .WithMessage("Пол должен быть 0 (женщина), 1 (мужчина) или 2 (неизвестно).");

        RuleFor(command => command.ModifiedBy)
            .NotEmpty().WithMessage("ModifiedBy обязателен");
        
        RuleFor(command => command.Birthday)
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Дата рождения должна быть меньше текущей даты.");
    }
}
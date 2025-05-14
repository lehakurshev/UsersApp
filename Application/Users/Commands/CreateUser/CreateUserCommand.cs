using Domain;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Guid>
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public int Gender { get; set; } // Пол 0 - женщина, 1 - мужчина, 2 - неизвестно
    public DateOnly? Birthday { get; set; }
    public bool IsAdmin { get; set; }
    public string CreatedBy { get; set; }
}
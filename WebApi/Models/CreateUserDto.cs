using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateUserDto
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Gender { get; set; } // Пол 0 - женщина, 1 - мужчина, 2 - неизвестно
    public DateOnly? Birthday { get; set; }
    [Required]
    public bool IsAdmin { get; set; }
    [Required]
    public string CreatedBy { get; set; }
}
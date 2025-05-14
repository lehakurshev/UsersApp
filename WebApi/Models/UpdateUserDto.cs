using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateUserDto
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public int Gender { get; set; }
    public DateOnly? Birthday { get; set; }
    [Required]
    public string? ModifiedBy { get; set; }
}
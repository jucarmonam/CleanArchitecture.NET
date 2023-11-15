using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models;
public class LoginVM
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

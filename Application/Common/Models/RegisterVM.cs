using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models;
public class RegisterVM
{
    [Display(Name = "email", Description = "Email address")]
    [EmailAddress]
    public required string Email { get; set; }

    [MinLength(8)]
    [MaxLength(100)]
    [Display(Name = "password", Description = "Password")]
    public required string Password { get; set; }

    [MinLength(1)]
    [MaxLength(100)]
    [Display(Name = "firstName", Description = "First name")]
    public required string FirstName { get; set; }

    [MinLength(1)]
    [MaxLength(100)]
    [Display(Name = "lastName", Description = "Last name")]
    public required string LastName { get; set; }
}

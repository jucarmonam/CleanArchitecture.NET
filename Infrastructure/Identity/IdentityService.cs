using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;
public class IdentityService : IIdentityService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }
    public async Task<PaginatedList<UserDto>> GetUsersAsync(PaginOptions paginOptions)
    {
        var usersQuery = _userManager.Users;

        var usersMapping = usersQuery.Select(uq => new UserDto
        {
            Email = uq.Email,
            FirstName = uq.FirstName,
            LastName = uq.LastName,
            CreatedAt = uq.CreatedAt
        });

        var users = await PaginatedList<UserDto>.CreateAsync(
            usersMapping,
            paginOptions.Page,
            paginOptions.PageSize);

        return users;
    }

    public async Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterForm registerForm)
    {
        var user = new ApplicationUser
        {
            Email = registerForm.Email,
            FirstName = registerForm.FirstName,
            LastName = registerForm.LastName,
            UserName = registerForm.Email,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var result = await _userManager.CreateAsync(user, registerForm.Password);

        if(!result.Succeeded)
        {
            var firstError = result.Errors.FirstOrDefault()?.Description;
            return (false, firstError);
        }

        return (true, null);
    }
}

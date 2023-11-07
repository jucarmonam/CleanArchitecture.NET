using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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

    public async Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterVM registerVM)
    {
        var userExists = await _userManager.FindByEmailAsync(registerVM.Email);

        if (userExists is not null)
        {
            return (false, $"User {registerVM.Email} already exists");
        }

        var user = new ApplicationUser
        {
            Email = registerVM.Email,
            FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            UserName = registerVM.Email,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var result = await _userManager.CreateAsync(user, registerVM.Password);

        if(!result.Succeeded)
        {
            var firstError = result.Errors.FirstOrDefault()?.Description;
            return (false, firstError);
        }

        return (true, null);
    }
}

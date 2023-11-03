using Application.Common.Models;

namespace Application.Common.Interfaces;

public record PaginOptions
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public interface IIdentityService
{
    Task<PaginatedList<UserDto>> GetUsersAsync(PaginOptions paginOptions);
    Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterForm registerForm);
}
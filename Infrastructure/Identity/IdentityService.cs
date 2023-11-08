using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    protected readonly ApplicationDbContext _context;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public IdentityService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        ApplicationDbContext context,
        TokenValidationParameters tokenValidationParameters)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _context = context;
        _tokenValidationParameters = tokenValidationParameters;
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

    public async Task<(bool Succeeded, string? ErrorMessage)> CreateUserAsync(RegisterVM registerVM)
    {
        var user = new ApplicationUser
        {
            Email = registerVM.Email,
            FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            UserName = registerVM.Email,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var result = await _userManager.CreateAsync(user, registerVM.Password);

        if (!result.Succeeded)
        {
            var firstError = result.Errors.FirstOrDefault()?.Description;
            return (false, firstError);
        }

        //Add user role
        switch (registerVM.Role)
        {
            case UserRoles.Administrator:
                await _userManager.AddToRoleAsync(user, UserRoles.Administrator);
                break;
            case UserRoles.User:
                await _userManager.AddToRoleAsync(user, UserRoles.User);
                break;
            default:
                break;
        }

        return (true, null);
    }

    public async Task<(AuthResultDto? Token, string ErrorMessage)> Login(LoginVM loginVM)
    {
        ApplicationUser? userExists = await _userManager.FindByEmailAsync(loginVM.Email);

        if (userExists is not null)
        {
            var result = await _userManager.CheckPasswordAsync(userExists, loginVM.Password);
            var tokenValue = result ? await GenerateJWTTokenAsync(userExists, null) : null;
            return result ? (tokenValue, "User signed in") : (null, "Password incorrect");
        }

        return (null, $"User {loginVM.Email} doesn't exists");
    }

    public async Task<AuthResultDto> VerifyAndGenerateTokenAsync(TokenRequestVM tokenRequestVM)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token ==
        tokenRequestVM.RefreshToken);
        var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

        try
        {
            var tokenCheckResult = jwtTokenHandler.ValidateToken(tokenRequestVM.Token,
                _tokenValidationParameters, out var validatedToken);

            return await GenerateJWTTokenAsync(dbUser, storedToken);
        }
        catch (SecurityTokenExpiredException)
        {
            if (storedToken.DateExpire >= DateTime.UtcNow)
            {
                return await GenerateJWTTokenAsync(dbUser, storedToken);
            }
            else
            {
                return await GenerateJWTTokenAsync(dbUser, null);
            }
        }
    }

    private async Task<AuthResultDto> GenerateJWTTokenAsync(ApplicationUser user, RefreshToken? rToken)
    {
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //Add user role claims
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authSigninkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.UtcNow.AddMinutes(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigninkey, SecurityAlgorithms.HmacSha256));

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        if(rToken is not null)
        {
            return new AuthResultDto(jwtToken, rToken.Token, token.ValidTo);
        }

        var refreshToken = new RefreshToken()
        {
            JwtId = token.Id,
            IsRevoked = false,
            UserId = user.Id,
            DateAdded = DateTime.UtcNow,
            DateExpire = DateTime.UtcNow.AddMonths(6),
            Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString(),
        };
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        return new AuthResultDto(jwtToken, refreshToken.Token, token.ValidTo);
    }
}

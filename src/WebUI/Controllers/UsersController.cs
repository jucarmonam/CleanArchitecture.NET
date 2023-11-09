using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
public class UsersController : ApiControllerBase
{
    private readonly IIdentityService _identityService;

    public UsersController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Administrator)]
    public async Task<ActionResult<PaginatedList<UserDto>>> GetVisibleUsers([FromQuery] PaginOptions paginOptions)
    {
        return await _identityService.GetUsersAsync(paginOptions);
    }

    [Authorize]
    [HttpGet("{userId}")]
    public Task<IActionResult> GetUserById(int userId)
    {
        // TODO is userId the current user's ID?
        // If so, return myself.
        // If not, only Admin roles should be able to view arbitrary users.
        throw new NotImplementedException();
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterVM form)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Please, provide all the required fields");
        }

        var (succeeded, message) = await _identityService.CreateUserAsync(form);

        if (succeeded) return Created("User created", null);

        return BadRequest(message);
    }

    [HttpPost("login-user")]
    public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Please, provide all required fields");
        }

        (AuthResultDto? token, string? message) = await _identityService.Login(loginVM);

        return token is not null ? Ok(token) : Unauthorized(message);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequestVM tokenRequestVM)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please, provide all required fields");
        }

        AuthResultDto refreshToken = await _identityService.VerifyAndGenerateTokenAsync(tokenRequestVM);

        return Ok(refreshToken);
    }
}

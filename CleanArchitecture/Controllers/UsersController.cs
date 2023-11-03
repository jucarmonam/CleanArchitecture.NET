using Application.Common.Interfaces;
using Application.Common.Models;
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

    [HttpGet(Name = nameof(GetVisibleUsers))]
    public async Task<ActionResult<PaginatedList<UserDto>>> GetVisibleUsers([FromQuery] PaginOptions paginOptions)
    {
        return await _identityService.GetUsersAsync(paginOptions);
    }

    [Authorize]
    [ProducesResponseType(401)]
    [HttpGet("{userId}", Name = nameof(GetUserById))]
    public Task<IActionResult> GetUserById(Guid userId)
    {
        // TODO is userId the current user's ID?
        // If so, return myself.
        // If not, only Admin roles should be able to view arbitrary users.
        throw new NotImplementedException();
    }

    [HttpPost(Name = nameof(RegisterUser))]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterForm form)
    {
        var (succeeded, message) = await _identityService.CreateUserAsync(form);

        if (succeeded) return Created("todo", null);

        return BadRequest(message);
    }
}

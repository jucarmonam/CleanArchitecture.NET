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

    [HttpGet]
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

    [HttpPost]
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
}

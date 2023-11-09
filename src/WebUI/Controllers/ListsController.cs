using Microsoft.AspNetCore.Mvc;
using Application.SkillLists.Commands.CreateSkillList;
using Application.SkillLists.Queries.GetSkillList;
using Application.SkillLists.Commands.DeleteSkillList;
using Application.SkillLists.Commands.UpdateSkillList;
using Application.SkillLists.Queries;
using Application.SkillLists.Queries.GetLists;
using Microsoft.AspNetCore.Authorization;
using Domain.Enums;

namespace WebUI.Controllers;

[Authorize(Roles = UserRoles.User)]
public class ListsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<SkillListDto>>> Get()
    {
        return await Mediator.Send(new GetListsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SkillListDto>> Get(int Id)
    {
        try
        {
            return Ok(await Mediator.Send(new GetSkillListQuery(Id)));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateSkillListCommand command)
    {
        try
        {
            if (command == null)
            {
                return BadRequest();
            }

            var skillList = await Mediator.Send(command);
            return Ok(skillList);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSkillListCommand command)
    {
        if (id != command.ListId)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteSkillListCommand(id));

        return NoContent();
    }
}


using Application.Common.Models;
using Application.Skills.Commands.CreateSkill;
using Application.Skills.Commands.DeleteSkill;
using Application.Skills.Commands.UpdateSkill;
using Application.Skills.Queries.GetSkill;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
public class SkillsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<SkillDto>>> GetSkillsByListId([FromQuery] GetSkillsQuery query)
    {
        try
        {
            return Ok(await Mediator.Send(query));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateSkillCommand command)
    {
        try
        {
            if (command == null)
            {
                return BadRequest();
            }

            var skill = await Mediator.Send(command);
            return Ok(skill);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSkillCommand command)
    {
        if (id != command.SkillId)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteSkillCommand(id));

        return NoContent();
    }
}

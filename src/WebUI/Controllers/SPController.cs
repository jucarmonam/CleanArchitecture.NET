using Application.Skills.Queries.GetSkillsByLevel;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
public class SPController : ApiControllerBase
{
    [HttpGet("{level}")]
    public async Task<ActionResult<int>> GetSkillsByLevel(SkillLevel level)
    {
        try
        {
            return Ok(await Mediator.Send(new GetAllSkillsByLevelQuery(level)));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}

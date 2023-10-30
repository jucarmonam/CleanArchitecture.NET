using Application.Skills.Queries.GetSkill;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SkillLists.Queries.GetSkillList;

public record SkillListDto(int Id, string? Title, IList<SkillDto> Skills);
/*
public class SkillListDto
{
    public SkillListDto()
    {
        Skills = Array.Empty<SkillDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }

    public IReadOnlyCollection<SkillDto> Skills { get; init; }
}
*/

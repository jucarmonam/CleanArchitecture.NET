using Application.Skills.Queries.GetSkill;

namespace Application.SkillLists.Queries.GetSkillList;

public record SkillListDto(string? Title, IList<SkillDto> Skills);
using Application.Skills.Queries.GetSkillsWithPagination;

namespace Application.SkillLists.Queries;

public record SkillListDto(string? Title, IList<SkillDto> Skills);
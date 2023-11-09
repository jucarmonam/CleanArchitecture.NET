using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.Skills.Queries.GetSkillsWithPagination;
public record SkillDto(string Name, string? Description, string Level);


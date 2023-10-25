using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.Skills.Queries.GetSkill;
public record SkillDto(int Id, int ListId, string Name, string? Description, string Level);


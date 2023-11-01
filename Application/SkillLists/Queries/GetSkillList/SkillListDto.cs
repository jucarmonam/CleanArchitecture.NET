using Application.Skills.Queries.GetSkill;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SkillLists.Queries.GetSkillList;

public record SkillListDto(string? Title, IList<SkillDto> Skills);
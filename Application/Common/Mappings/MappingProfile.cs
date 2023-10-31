using Application.SkillLists.Queries.GetSkillList;
using Application.Skills.Queries.GetSkill;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Skill, SkillDto>();
        CreateMap<SkillList, SkillListDto>();
    }
}

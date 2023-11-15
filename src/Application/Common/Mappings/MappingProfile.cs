using Application.SkillLists.Queries;
using Application.Skills.Queries.GetSkillsWithPagination;
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

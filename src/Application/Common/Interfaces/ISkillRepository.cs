using Application.Skills.Queries.GetSkillsByLevel;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<List<LevelResponse>> GetAllSkillsByLevel(SkillLevel skillLevel);
    }
}

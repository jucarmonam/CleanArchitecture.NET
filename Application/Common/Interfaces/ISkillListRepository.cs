using Application.Skills.Queries.GetSkill;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface ISkillListRepository : IRepository<SkillList>
    {
        Task<List<SkillList>> GetAllListsWithSkills();

        Task<SkillList> GetListByIdWithSkills(int Id);
    }
}

using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ISkillListRepository : IRepository<SkillList>
    {
        Task<List<SkillList>> GetAllListsWithSkills();

        Task<SkillList> GetListByIdWithSkills(int id);

        Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken);
    }
}

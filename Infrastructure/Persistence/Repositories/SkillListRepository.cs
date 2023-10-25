using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class SkillListRepository : Repository<SkillList>, ISkillListRepository
    {
        public SkillListRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}

using Application.Common.Interfaces;
using Application.Skills.Queries.GetSkillsByLevel;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal sealed class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<List<LevelResponse>> GetAllSkillsByLevel(SkillLevel skillLevel)
        {
            var level = new SqlParameter("@Level", skillLevel);
            return await _context.Set<LevelResponse>().FromSqlRaw("uspExpertSkills @Level", level).ToListAsync();
        }
    }
}

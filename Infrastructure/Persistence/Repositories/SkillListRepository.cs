using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Skills.Queries.GetSkill;
using Azure.Core;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.Persistence.Repositories
{
    public class SkillListRepository : Repository<SkillList>, ISkillListRepository
    {
        public SkillListRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<List<SkillList>> GetAllListsWithSkills()
        {
            return await _context
            .SkillLists
            .Include(x => x.Skills)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<SkillList> GetListByIdWithSkills(int Id)
        {
            return await _context
            .SkillLists
            .Include(x => x.Skills)
            .Where(p => p.Id == Id)
            .FirstAsync();
        }
    }
}

﻿using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

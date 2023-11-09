using Application.Skills.Queries.GetSkillsByLevel;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<Skill> Skills { get; }
        DbSet<SkillList> SkillLists { get; }
        DbSet<LevelResponse> LevelResponses { get; }
    }
}

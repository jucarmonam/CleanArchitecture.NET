using Application.Common.Interfaces;
using Application.Skills.Queries.GetSkill;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<SkillList> SkillLists => Set<SkillList>();

        public DbSet<LevelResponse> LevelResponses => Set<LevelResponse>();

        public override DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LevelResponse>()
                .HasNoKey();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        /*
        public IQueryable<LevelResponse> SearchCustomers(int level)
        {
            SqlParameter skillLevel = new("@Level", level);
            return this.Set<LevelResponse>().FromSqlRaw("EXECUTE uspExpertSkills @Level", skillLevel);
        }
        */
    }
}

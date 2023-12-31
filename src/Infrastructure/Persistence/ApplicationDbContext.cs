﻿using Application.Common.Interfaces;
using Application.Skills.Queries.GetSkillsByLevel;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<SkillList> SkillLists => Set<SkillList>();

        public DbSet<LevelResponse> LevelResponses => Set<LevelResponse>();

        public DbSet<RefreshToken> RefreshTokens {  get; set; }

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
    }
}

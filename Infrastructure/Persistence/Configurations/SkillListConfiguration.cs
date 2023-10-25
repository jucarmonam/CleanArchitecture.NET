using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class SkillListConfiguration : IEntityTypeConfiguration<SkillList>
    {
        public void Configure(EntityTypeBuilder<SkillList> builder)
        {
            builder.HasMany(sl => sl.Skills)
                .WithOne(s => s.SkillList)
                .HasForeignKey(s => s.ListId);

            builder.Property(t => t.Title)
               .HasMaxLength(20)
               .IsRequired();
        }
    }
}

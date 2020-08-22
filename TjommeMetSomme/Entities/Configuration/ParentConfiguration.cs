using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder
                .HasKey(parent => parent.ParentId); 

            builder
                .Property(parent => parent.ParentId)
                .UseMySqlIdentityColumn();

            builder
                .Property(parent => parent.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(parent => parent.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .ToTable("Parents");
        }
    }
}
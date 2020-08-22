using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder
                .HasKey(parent => parent.Id); 

            builder
                .Property(parent => parent.Id)
                .UseMySqlIdentityColumn();

            builder
                .ToTable("Parents");
        }
    }
}
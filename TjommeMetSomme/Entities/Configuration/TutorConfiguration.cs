using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
    {
        public void Configure(EntityTypeBuilder<Tutor> builder)
        {
            builder
                .HasKey(tutor => tutor.Id);

            builder
                .Property(tutor => tutor.Id)
                .UseMySqlIdentityColumn();

            builder
                .ToTable("Tutors");
        }
    }
}
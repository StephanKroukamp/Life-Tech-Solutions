using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
    {
        public void Configure(EntityTypeBuilder<Tutor> builder)
        {
            builder
                .HasKey(tutor => tutor.TutorId);

            builder
                .Property(tutor => tutor.TutorId)
                .UseMySqlIdentityColumn();

            builder
                .Property(tutor => tutor.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(tutor => tutor.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .ToTable("Tutors");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(course => course.CourseId);

            builder
                .Property(course => course.CourseId)
                .UseMySqlIdentityColumn();

            builder
                .Property(course => course.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .ToTable("Courses");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class TutorCourseConfiguration : IEntityTypeConfiguration<TutorCourse>
    {
        public void Configure(EntityTypeBuilder<TutorCourse> builder)
        {
            builder
                .HasKey(tc => new { tc.TutorId, tc.CourseId });

            builder
                .HasOne<Tutor>(tc => tc.Tutor)
                .WithMany(t => t.TutorCourses)
                .HasForeignKey(sc => sc.TutorId);

            builder
                .HasOne<Course>(tc => tc.Course)
                .WithMany(t => t.TutorCourses)
                .HasForeignKey(tc => tc.CourseId);

            builder
                .ToTable("Tutor_Course");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class TutorCourseConfiguration : IEntityTypeConfiguration<TutorCourse>
    {
        public void Configure(EntityTypeBuilder<TutorCourse> builder)
        {
            builder
                .HasKey(tutorCourse => new { tutorCourse.TutorId, tutorCourse.CourseId });

            builder
                .HasOne<Tutor>(tutorCourse => tutorCourse.Tutor)
                .WithMany(tutor => tutor.TutorCourses)
                .HasForeignKey(tutorCourse => tutorCourse.TutorId);

            builder
                .HasOne<Course>(tutorCourse => tutorCourse.Course)
                .WithMany(tutor => tutor.TutorCourses)
                .HasForeignKey(tutorCourse => tutorCourse.CourseId);

            builder
                .ToTable("Tutors_Courses");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TjommeMetSomme.Entities.Configuration
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder
                .HasKey(studentCourse => new { studentCourse.StudentId, studentCourse.CourseId });
            
            builder
                .HasOne<Student>(studentCourse => studentCourse.Student)
                .WithMany(student => student.StudentCourses)
                .HasForeignKey(studentCourse => studentCourse.StudentId);

            builder
                .HasOne<Course>(studentCourse => studentCourse.Course)
                .WithMany(student => student.StudentCourses)
                .HasForeignKey(studentCourse => studentCourse.CourseId);

            builder
                .ToTable("Students_Courses");
        }
    }
}
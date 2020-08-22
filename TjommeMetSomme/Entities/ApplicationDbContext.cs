using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities.Configuration;

namespace TjommeMetSomme.Entities
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Tutor> Tutors { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<TutorCourse> TutorCourses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new StudentConfiguration());

            builder.ApplyConfiguration(new CourseConfiguration());

            builder.ApplyConfiguration(new TutorConfiguration());

            builder.ApplyConfiguration(new ParentConfiguration());

            builder.ApplyConfiguration(new StudentCourseConfiguration());

            builder.ApplyConfiguration(new TutorCourseConfiguration());
        }
    }
}
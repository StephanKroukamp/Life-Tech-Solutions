using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities.Configuration;
using TjommeMetSomme.Entities.Configuration.Identity;
using TjommeMetSomme.Entities.Identity;

namespace TjommeMetSomme.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
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

            builder.ApplyConfiguration(new ApplicationRoleConfiguration());

            builder.ApplyConfiguration(new ApplicationUserConfiguration());

            builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());

            builder.ApplyConfiguration(new ParentConfiguration());

            builder.ApplyConfiguration(new StudentConfiguration());

            builder.ApplyConfiguration(new CourseConfiguration());

            builder.ApplyConfiguration(new TutorConfiguration());
            
            builder.ApplyConfiguration(new StudentCourseConfiguration());

            builder.ApplyConfiguration(new TutorCourseConfiguration());
        }
    }
}
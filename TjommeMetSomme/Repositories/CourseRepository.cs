using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

        public async Task<IEnumerable<Course>> GetAllWithStudentsAsync()
        {
            return await ApplicationDbContext.Courses
                .Include(course => course.StudentCourses)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithStudentsByStudentIdAsync(int studentId)
        {
            return await ApplicationDbContext.Courses
                .Include(course => course.StudentCourses)
                .Where(course => course.StudentCourses.Any(studentCourse => studentCourse.StudentId == studentId))
                .ToListAsync();
        }

        public async Task<Course> GetByIdWithStudentsAsync(int courseId)
        {
            return await ApplicationDbContext.Courses
                .Include(course => course.StudentCourses)
                .SingleOrDefaultAsync(course => course.CourseId == courseId);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
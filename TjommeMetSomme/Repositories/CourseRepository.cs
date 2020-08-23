using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAll(bool includeStudents);

        Task<Course> GetById(int courseId, bool includeStudents);

        Task<IEnumerable<Course>> GetAllByStudentId(int studentId, bool includeStudents);
    }

    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

        public async Task<IEnumerable<Course>> GetAll(bool includeStudents)
        {
            if (includeStudents)
            {
                return await ApplicationDbContext.Courses
                    .Include(course => course.StudentCourses)
                    .ToListAsync();
            }

            return await ApplicationDbContext.Courses
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllByStudentId(int studentId, bool includeStudents)
        {
            if(includeStudents)
            {
                return await ApplicationDbContext.Courses
                    .Include(course => course.StudentCourses)
                    .Where(course => course.StudentCourses.Any(studentCourse => studentCourse.StudentId == studentId))
                    .ToListAsync();
            }

            return await ApplicationDbContext.Courses
                .Where(course => course.StudentCourses.Any(studentCourse => studentCourse.StudentId == studentId))
                .ToListAsync();
        }

        public async Task<Course> GetById(int courseId, bool includeStudents)
        {
            if (includeStudents)
            {
                return await ApplicationDbContext.Courses
                .Include(course => course.StudentCourses)
                .SingleOrDefaultAsync(course => course.Id == courseId);
            }

            return await ApplicationDbContext.Courses
                .SingleOrDefaultAsync(course => course.Id == courseId);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
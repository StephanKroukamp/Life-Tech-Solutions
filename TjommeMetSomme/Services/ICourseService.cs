using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllWithStudents();

        Task<Course> GetCourseById(int courseId);

        Task<Course> GetCourseByIdWithStudents(int courseId);

        Task<IEnumerable<Course>> GetCoursesByStudentId(int studentId);

        Task<Course> CreateCourse(Course course);

        Task UpdateCourse(Course courseToBeUpdated, Course course);

        Task DeleteCourse(Course course);
    }
}
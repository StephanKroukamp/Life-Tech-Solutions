using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllWithStudentsAsync();

        Task<Course> GetByIdWithStudentsAsync(int courseId);

        Task<IEnumerable<Course>> GetAllWithStudentsByStudentIdAsync(int studentId);
    }
}
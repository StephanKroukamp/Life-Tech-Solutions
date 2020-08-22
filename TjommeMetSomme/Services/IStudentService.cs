using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllWithParent();

        Task<Student> GetStudentById(int id);

        Task<Student> GetStudentByIdWithParent(int id);

        Task<IEnumerable<Student>> GetStudentsByParentId(int parentId);

        Task<Student> CreateStudent(Student student);

        Task UpdateStudent(Student studentToBeUpdated, Student student);

        Task DeleteStudent(Student student);
    }
}
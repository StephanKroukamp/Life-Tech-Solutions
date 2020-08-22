using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllWithParentAsync();

        Task<Student> GetByIdWithParentAsync(int id);

        Task<IEnumerable<Student>> GetAllWithParentByParentIdAsync(int parentId);
    }
}
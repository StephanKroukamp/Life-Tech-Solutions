using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public interface IParentRepository : IRepository<Parent>
    {
        Task<IEnumerable<Parent>> GetAllWithStudentsAsync();

        Task<Parent> GetWithStudentsByParentIdAsync(int parentId);
    }
}
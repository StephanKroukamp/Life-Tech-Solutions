using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Services
{
    public interface IParentService : IService<Parent>
    {
        Task<IEnumerable<Parent>> GetAll(bool includeStudents, bool includeApplicationUser);

        Task<Parent> GetById(int parentId, bool includeStudents, bool includeApplicationUser);
    }
}
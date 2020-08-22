using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Services
{
    public interface IParentService
    {
        Task<IEnumerable<Parent>> GetAllParents();

        Task<Parent> GetParentById(int id);

        Task<Parent> CreateParent(Parent parent);

        Task UpdateParent(Parent parentToBeUpdated, Parent parent);

        Task DeleteParent(Parent parent);
    }
}
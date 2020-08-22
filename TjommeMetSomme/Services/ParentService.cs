using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Repositories;

namespace TjommeMetSomme.Services
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Parent> CreateParent(Parent parent)
        {
            await _unitOfWork.Parents
                .AddAsync(parent);

            await _unitOfWork.CommitAsync();

            return parent;
        }

        public async Task DeleteParent(Parent parent)
        {
            _unitOfWork.Parents.Remove(parent);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Parent>> GetAllParents()
        {
            return await _unitOfWork.Parents.GetAllAsync();
        }

        public async Task<Parent> GetParentById(int id)
        {
            return await _unitOfWork.Parents.GetByIdAsync(id);
        }

        public async Task UpdateParent(Parent parentToBeUpdated, Parent parent)
        {
            parentToBeUpdated.FirstName = parent.FirstName;
            parentToBeUpdated.LastName = parent.LastName;

            await _unitOfWork.CommitAsync();
        }
    }
}
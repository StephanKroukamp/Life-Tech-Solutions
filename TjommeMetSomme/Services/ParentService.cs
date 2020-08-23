using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Repositories;

namespace TjommeMetSomme.Services
{
    public interface IParentService : IService<Parent>
    {
        Task<IEnumerable<Parent>> GetAll(bool includeStudents);

        Task<Parent> GetById(int parentId, bool includeStudents);
    }

    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Parent> Create(Parent parent)
        {
            await _unitOfWork.Parents.Add(parent);

            await _unitOfWork.Commit();

            return parent;
        }

        public async Task Delete(Parent parent)
        {
            _unitOfWork.Parents.Remove(parent);

            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Parent>> GetAll()
        {
            return await _unitOfWork.Parents.GetAll();
        }

        public async Task<Parent> GetById(int parentId)
        {
            return await _unitOfWork.Parents.GetById(parentId);
        }

        public async Task Update(Parent parentToBeUpdated, Parent parent)
        {
            await _unitOfWork.Commit();
        }

        // 

        public async Task<IEnumerable<Parent>> GetAll(bool includeStudents)
        {
            return await _unitOfWork.Parents.GetAll(includeStudents);
        }

        public async Task<Parent> GetById(int parentId, bool includeStudents)
        {
            return await _unitOfWork.Parents.GetById(parentId, includeStudents);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TjommeMetSomme.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<TEntity> Create(TEntity entity);

        Task Update(TEntity entityToBeUpdated, TEntity entity);

        Task Delete(TEntity entity);
    }
}
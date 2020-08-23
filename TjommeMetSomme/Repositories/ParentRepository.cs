using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public interface IParentRepository : IRepository<Parent>
    {
        Task<IEnumerable<Parent>> GetAll(bool includeStudents);

        Task<Parent> GetById(int parentId, bool includeStudents);
    }

    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

        public async Task<IEnumerable<Parent>> GetAll(bool includeStudents)
        {
            IEnumerable<Parent> parents;

            if (includeStudents)
            {
                parents = await ApplicationDbContext.Parents
                    .Include(parent => parent.ApplicationUser)
                    .Include(parent => parent.ApplicationRole)
                    .Include(parent => parent.Students)
                    .ThenInclude(student => student.ApplicationUser)
                    .Include(parent => parent.Students)
                    .ThenInclude(student => student.ApplicationRole)
                    .ToListAsync();
            }
            else
            {
                parents = await ApplicationDbContext.Parents
                    .Include(parent => parent.ApplicationUser)
                    .Include(parent => parent.ApplicationRole)
                    .ToListAsync();
            }

            return parents;
        }

        public async Task<Parent> GetById(int parentId, bool includeStudents)
        {
            if (includeStudents)
            {
                return await ApplicationDbContext.Parents
                    .Include(parent => parent.ApplicationUser)
                    .Include(parent => parent.ApplicationRole)
                    .Include(parent => parent.Students)
                    .ThenInclude(student => student.ApplicationUser)
                    .Include(parent => parent.Students)
                    .ThenInclude(student => student.ApplicationRole)
                    .Where(parent => parent.Id.Equals(parentId))
                    .SingleOrDefaultAsync();
            }

            return await ApplicationDbContext.Parents
                .Include(parent => parent.ApplicationUser)
                .Include(parent => parent.ApplicationRole)
                .Where(parent => parent.Id.Equals(parentId))
                .SingleOrDefaultAsync();
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
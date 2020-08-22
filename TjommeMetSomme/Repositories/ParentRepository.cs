using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public interface IParentRepository : IRepository<Parent>
    {
        Task<IEnumerable<Parent>> GetAll(bool includeStudents, bool includeApplicationUser);

        Task<Parent> GetById(int parentId, bool includeStudents, bool includeApplicationUser);
    }

    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

        public async Task<IEnumerable<Parent>> GetAll(bool includeStudents, bool includeApplicationUser)
        {
            if (includeStudents && includeApplicationUser)
            {
                return await ApplicationDbContext.Parents
                   .Include(parent => parent.Students)
                   .Include(parent => parent.ApplicationUser)
                   .ToListAsync();
            }

            if (includeStudents && !includeApplicationUser)
            {
                return await ApplicationDbContext.Parents
                   .Include(parent => parent.Students)
                   .ToListAsync();
            }

            if (includeApplicationUser && !includeStudents)
            {
                return await ApplicationDbContext.Parents
                   .Include(parent => parent.ApplicationUser)
                   .ToListAsync();
            }

            return await ApplicationDbContext.Parents
                   .ToListAsync();
        }

        public async Task<Parent> GetById(int parentId, bool includeStudents, bool includeApplicationUser)
        {
            if (includeStudents && includeApplicationUser)
            {
                return await ApplicationDbContext.Parents
                .Where(parent => parent.Id.Equals(parentId))
                .Include(parent => parent.Students)
                .Include(parent => parent.ApplicationUser)
                .SingleOrDefaultAsync();
            }

            if (includeStudents && !includeApplicationUser)
            {
                return await ApplicationDbContext.Parents
                    .Where(parent => parent.Id.Equals(parentId))
                    .Include(parent => parent.Students)
                    .SingleOrDefaultAsync();
            }

            if (includeApplicationUser && !includeStudents)
            {
                return await ApplicationDbContext.Parents
                .Where(parent => parent.Id.Equals(parentId))
                .Include(parent => parent.ApplicationUser)
                .SingleOrDefaultAsync();
            }

            return await ApplicationDbContext.Parents
                .Where(parent => parent.Id.Equals(parentId))
                .SingleOrDefaultAsync();
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
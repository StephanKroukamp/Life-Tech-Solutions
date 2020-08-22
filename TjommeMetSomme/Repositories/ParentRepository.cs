using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }
        public async Task<IEnumerable<Parent>> GetAllWithStudentsAsync()
        {
            return await ApplicationDbContext.Parents
                .Include(parent => parent.Students)
                .ToListAsync();
        }

        public Task<Parent> GetWithStudentsByParentIdAsync(int parentId)
        {
            return ApplicationDbContext.Parents
                .Include(parent => parent.Students)
                .SingleOrDefaultAsync(parent => parent.Id == parentId);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
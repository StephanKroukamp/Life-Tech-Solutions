using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAll(bool includeParent);

        Task<IEnumerable<Student>> GetAllByParentId(int parentId, bool includeParent);

        Task<Student> GetById(int studentId, bool includeParent);
    }

    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

        public async Task<IEnumerable<Student>> GetAll(bool includeParent)
        {
            if (includeParent)
            {
                return await ApplicationDbContext.Students
                    .Include(student => student.Parent)
                    .ToListAsync();
            }

            return await ApplicationDbContext.Students
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllByParentId(int parentId, bool includeParent)
        {
            if (includeParent)
            {
                return await ApplicationDbContext.Students
                    .Include(student => student.Parent)
                    .Where(student => student.ParentId == parentId)
                    .ToListAsync();
            }

            return await ApplicationDbContext.Students
                .Where(student => student.ParentId == parentId)
                .ToListAsync();
        }

        public async Task<Student> GetById(int studentId, bool includeParent)
        {
            if(includeParent)
            {
                return await ApplicationDbContext.Students
                    .Include(student => student.Parent)
                    .SingleOrDefaultAsync(student => student.Id == studentId);
            }

            return await ApplicationDbContext.Students
                .SingleOrDefaultAsync(student => student.Id == studentId);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
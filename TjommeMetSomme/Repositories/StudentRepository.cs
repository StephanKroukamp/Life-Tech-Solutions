using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

        public async Task<IEnumerable<Student>> GetAllWithParentAsync()
        {
            return await ApplicationDbContext.Students
                .Include(student => student.Parent)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllWithParentByParentIdAsync(int parentId)
        {
            return await ApplicationDbContext.Students
                .Include(student => student.Parent)
                .Where(student => student.ParentId == parentId)
                .ToListAsync();
        }

        public async Task<Student> GetByIdWithParentAsync(int studentId)
        {
            return await ApplicationDbContext.Students
                .Include(student => student.Parent)
                .SingleOrDefaultAsync(student => student.Id == studentId);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
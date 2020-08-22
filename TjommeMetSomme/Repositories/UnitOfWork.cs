using System.Threading.Tasks;
using TjommeMetSomme.Entities;

namespace TjommeMetSomme.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private IParentRepository _parentRepository;

        //private IBookRepository _bookRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IParentRepository Parents => _parentRepository ??= new ParentRepository(_applicationDbContext);

        //public IBookRepository Books => _bookRepository ??= new BookRepository(_applicationDbContext);

        public async Task<int> CommitAsync()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}
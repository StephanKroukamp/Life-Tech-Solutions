using System;
using System.Threading.Tasks;

namespace TjommeMetSomme.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IParentRepository Parents { get; }

        //IBookRepository Books { get; }

        Task<int> CommitAsync();
    }
}
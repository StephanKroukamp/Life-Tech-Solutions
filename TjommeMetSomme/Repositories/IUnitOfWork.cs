using System;
using System.Threading.Tasks;

namespace TjommeMetSomme.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IParentRepository Parents { get; }

        IStudentRepository Students { get; }

        ICourseRepository Courses { get; }

        Task<int> CommitAsync();
    }
}
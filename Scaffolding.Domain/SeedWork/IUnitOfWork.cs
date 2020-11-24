namespace Scaffolding.Domain.SeedWork
{
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}

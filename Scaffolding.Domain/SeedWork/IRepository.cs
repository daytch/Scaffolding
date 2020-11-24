namespace Scaffolding.Domain.SeedWork
{

    using System;

    public interface IRepository<T> : IDisposable where T : class
    {
    }
}

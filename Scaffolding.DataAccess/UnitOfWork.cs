namespace Scaffolding.DataAccess
{
    using Scaffolding.DataAccess.Context;
    using Scaffolding.Domain.SeedWork;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        public void Dispose()
        {
            this.Dispose(true);
        }

        public ScaffoldingContext DatabaseContext { get; }

        public UnitOfWork(ScaffoldingContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task<int> CommitAsync()
        {
            return await this.DatabaseContext.SaveChangesAsync().ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.DatabaseContext?.Dispose();
                this.disposed = true;
            }
        }
    }
}
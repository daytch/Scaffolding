namespace Scaffolding.DataAccess.DataAccess
{
    using Scaffolding.DataAccess.Context;
    using Scaffolding.Domain.SeedWork;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private bool disposed = false;

        protected ScaffoldingContext DatabaseContext;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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

        public BaseRepository(ScaffoldingContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }
    }
}

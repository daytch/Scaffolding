namespace Scaffolding.DataAccess.Context
{
    using Microsoft.EntityFrameworkCore;
    using Scaffolding.Shared.Entities;
    using System.Reflection;

    public class ScaffoldingContext : DbContext
    {

        public ScaffoldingContext(DbContextOptions<ScaffoldingContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employee { get; set; }
    }
}

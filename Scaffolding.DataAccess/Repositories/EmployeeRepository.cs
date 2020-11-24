namespace Scaffolding.DataAccess.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Scaffolding.DataAccess.Context;
    using Scaffolding.DataAccess.DataAccess;
    using Scaffolding.Domain.Repositories;
    using Scaffolding.Shared.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly ScaffoldingContext scaffoldingContext;

        public EmployeeRepository(ScaffoldingContext scaffoldingContext) : base(scaffoldingContext)
        {
            this.scaffoldingContext = scaffoldingContext;
        }

        public async Task PostEmployeeAsych(Employee employee)
        {
            await this.scaffoldingContext.Employee.AddAsync(employee).ConfigureAwait(false);
        }

        public async Task<int> PatchEmployeeAsych(Employee employee)
        {
            int result = 0;
            this.scaffoldingContext.Employee.Update(employee);
            result = await this.scaffoldingContext.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<List<Employee>> GetEmployeeAsync()
        {
            List<Employee> result = new List<Employee>();
            result = await this.scaffoldingContext.Employee.ToListAsync();
            return result;
        }
    }
}

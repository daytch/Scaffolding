
namespace Scaffolding.Domain.Repositories
{
    using Scaffolding.Domain.SeedWork;
    using Scaffolding.Shared.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task PostEmployeeAsych(Employee employee);

        Task<int> PatchEmployeeAsych(Employee employee);

        Task<List<Employee>> GetEmployeeAsync();
    }
}

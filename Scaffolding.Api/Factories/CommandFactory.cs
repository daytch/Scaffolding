namespace Scaffolding.API.Factories
{
    using Scaffolding.ApplicationServices.Commands.PatchEmployee;
    using Scaffolding.ApplicationServices.Commands.PostEmployee;
    using Scaffolding.Shared.Entities;
    using System;
    public class CommandFactory : ICommandFactory
    {
        public PostEmployeeCommand CreateEmployeeCommand(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            return new PostEmployeeCommand(employee);
        }

        public PatchEmployeeCommand PatchEmployeeCommand(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            return new PatchEmployeeCommand(employee);
        }
    }
}

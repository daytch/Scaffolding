namespace Scaffolding.API.Factories
{
    using Scaffolding.ApplicationServices.Commands.PatchEmployee;
    using Scaffolding.ApplicationServices.Commands.PostEmployee;
    using Scaffolding.Shared.Entities;

    public interface ICommandFactory
    {
        PostEmployeeCommand CreateEmployeeCommand(Employee employee);

        PatchEmployeeCommand PatchEmployeeCommand(Employee employee);
    }
}

namespace Scaffolding.ApplicationServices.Commands.PatchEmployee
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Scaffolding.Shared.Entities;

    public class PatchEmployeeCommand : IRequest<Result>
    {
        public PatchEmployeeCommand(Employee employee)
        {
            this.Employee = employee;
        }
        public Employee Employee { get; set; }
    }
}

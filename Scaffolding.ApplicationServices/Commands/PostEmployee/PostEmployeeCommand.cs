namespace Scaffolding.ApplicationServices.Commands.PostEmployee
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Scaffolding.Shared.Entities;

    public class PostEmployeeCommand : IRequest<Result>
    {
        public PostEmployeeCommand(Employee employee)
        {
            this.Employee = employee;
        }
        public Employee Employee { get; set; }
    }
}

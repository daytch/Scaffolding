namespace Scaffolding.ApplicationServices.Commands.PatchEmployee
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Scaffolding.Domain.Repositories;
    using Scaffolding.Domain.SeedWork;
    using Scaffolding.Shared.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class PatchEmployeeCommandHandler : IRequestHandler<PatchEmployeeCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmployeeRepository employeeRespository;
        private readonly ILogger<PatchEmployeeCommandHandler> logger;
        public PatchEmployeeCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRespository, ILogger<PatchEmployeeCommandHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.employeeRespository = employeeRespository;
            this.logger = logger;
        }
        public async Task<Result> Handle(PatchEmployeeCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogTrace("Starting command handler for command {command}", typeof(PatchEmployeeCommand).Name);

            await this.employeeRespository.PatchEmployeeAsych(request.Employee).ConfigureAwait(false);

            await this.unitOfWork.CommitAsync().ConfigureAwait(false);
            this.logger.LogTrace("Execution completed for command handler");
            return Result.Ok();
        }

    }
}

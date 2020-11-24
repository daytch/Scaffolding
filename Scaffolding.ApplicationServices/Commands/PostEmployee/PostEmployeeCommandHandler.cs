namespace Scaffolding.ApplicationServices.Commands.PostEmployee
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

    public class PostEmployeeCommandHandler : IRequestHandler<PostEmployeeCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmployeeRepository employeeRespository;
        private readonly ILogger<PostEmployeeCommandHandler> logger;
        public PostEmployeeCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRespository, ILogger<PostEmployeeCommandHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.employeeRespository = employeeRespository;
            this.logger = logger;
        }
        public async Task<Result> Handle(PostEmployeeCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogTrace("Starting command handler for command {command}", typeof(PostEmployeeCommand).Name);

            await this.employeeRespository.PostEmployeeAsych(request.Employee).ConfigureAwait(false);

            await this.unitOfWork.CommitAsync().ConfigureAwait(false);
            this.logger.LogTrace("Execution completed for command handler");
            return Result.Ok();
        }

    }
}

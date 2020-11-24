namespace Scaffolding.ApplicationServices.Queries.GetEmployee
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Scaffolding.Domain.Repositories;
    using Scaffolding.Shared.Entities;
    using Scaffolding.Shared.Responses;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Result<SubmissionResponse>>
    {
        private readonly IEmployeeRepository employeeRepository;

        private readonly ILogger<GetEmployeeQueryHandler> logger;

        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, ILogger<GetEmployeeQueryHandler> logger)
        {
            this.employeeRepository = employeeRepository;
            this.logger = logger;
        }

        public async Task<Result<SubmissionResponse>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this.employeeRepository.GetEmployeeAsync().ConfigureAwait(false);

                return SubmissionResponse.ForSuccessWithObject(result);
            }
            catch (Exception ex)
            {
                return SubmissionResponse.ForFailure(ex.Message);
            }
        }
    }
}

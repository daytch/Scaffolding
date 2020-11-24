namespace Scaffolding.ApplicationServices.Queries.GetEmployee
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Scaffolding.Shared.Entities;
    using Scaffolding.Shared.Responses;
    using System.Collections.Generic;

    public class GetEmployeeQuery : IRequest<Result<SubmissionResponse>>
    {
        public GetEmployeeQuery()
        {
        }
    }
}

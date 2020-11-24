namespace Scaffolding.API.Controllers
{
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Scaffolding.API.Factories;
    using Scaffolding.ApplicationServices.Queries.GetEmployee;
    using Scaffolding.Shared.Entities;
    using Scaffolding.Shared.Responses;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    public class EmployeeController : ValidationController<EmployeeController>
    {

        private readonly IMediator mediator;

        private readonly ILogger<EmployeeController> logger;

        private readonly ICommandFactory commandFactory;

        private readonly IHttpContextAccessor _contextAccessor;

        public EmployeeController(
            IMediator mediator,
            IEnumerable<IValidator> validators,
            ICommandFactory commandFactory,
            ILogger<EmployeeController> logger,
            IHttpContextAccessor contextAccessor) : base(validators, logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.commandFactory = commandFactory;
            this._contextAccessor = contextAccessor;
        }

        [HttpPost]
        [Route("/employee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> PostEmployee([FromBody] Employee request)
        {
            var command = commandFactory.CreateEmployeeCommand(request);

            logger.LogTrace("Validating command");

            var validationResult = Validate(command);
            if (validationResult.IsFailure)
            {
                return BadRequest(this.ModelState);
            }

            var result = await mediator.Send(command).ConfigureAwait(false);
            if (result.IsFailure)
            {
                return this.Ok(SubmissionResponse.ForFailure(result.Error));
            }

            return this.Ok(SubmissionResponse.ForSuccess());
        }

        [HttpGet]
        [Route("/employee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SubmissionResponse>> GetEmployee()
        {
            var query = new GetEmployeeQuery();
            var result = await this.ValidateAndExecute(query, (q) => this.mediator.Send(query)).ConfigureAwait(false);
            return result;
        }

        [HttpPatch]
        [Route("/employee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SubmissionResponse>> PatchEmployee([FromBody] Employee request)
        {
            var command = commandFactory.PatchEmployeeCommand(request);

            logger.LogTrace("Validating command");

            var validationResult = Validate(command);
            if (validationResult.IsFailure)
            {
                return BadRequest(this.ModelState);
            }

            var result = await mediator.Send(command).ConfigureAwait(false);
            if (result.IsFailure)
            {
                return this.Ok(SubmissionResponse.ForFailure(result.Error));
            }

            return this.Ok(SubmissionResponse.ForSuccess());
        }

    }
}

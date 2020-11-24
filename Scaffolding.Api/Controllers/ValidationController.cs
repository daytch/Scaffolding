namespace Scaffolding.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CSharpFunctionalExtensions;

    using FluentValidation;
    using FluentValidation.AspNetCore;
    using FluentValidation.Results;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using ValidationContext = FluentValidation.ValidationContext;

    public class ValidationController<TController> : ControllerBase
    {
        private readonly IEnumerable<IValidator> validators;

        private readonly ILogger<TController> logger;

        public ValidationController(IEnumerable<IValidator> validators, ILogger<TController> logger)
        {
            this.validators = validators;
            this.logger = logger;
        }

        protected Result Validate(IBaseRequest request)
        {
            this.logger.LogTrace("Validating the query : {@query} ", request);
            var validationResult = this.GetValidationResult(request);
            if (validationResult == null || validationResult.IsValid)
            {
                return Result.Ok();
            }

            validationResult.AddToModelState(this.ModelState, null);
            this.logger.LogWarning("Validation Errors found: {modelState}", this.ModelState);
            return Result.Failure("Validation errors found");
        }

        protected async Task<ActionResult<T>> ValidateAndExecute<T>(IBaseRequest query, Func<IBaseRequest, Task<Result<T>>> execute)
        {
            this.logger.LogTrace("Validating the query : {@query} ", query);

            var validationResult = this.GetValidationResult(query);

            if (validationResult != null && !validationResult.IsValid)
            {
                this.logger.LogWarning("Query has validation errors : {@request} ", query);
                validationResult.AddToModelState(this.ModelState, null);
                return this.BadRequest(this.ModelState);
            }

            var result = await execute(query).ConfigureAwait(false);

            if (result.IsFailure)
            {
                this.logger.LogWarning("Query handler failed with error : {@error} ", result.Error);
                return this.BadRequest(result.Error);
            }

            if (result.Value == null)
            {
                this.logger.LogInformation("Query executed successfully and returned empty result");
                return this.Ok(null);
            }

            this.logger.LogTrace("Query executed successfully with return value {@value}", result.Value);
            return this.Ok(result.Value);
        }

        protected async Task<ActionResult> ValidateAndExecute(IBaseRequest command, Func<IBaseRequest, Task<Result>> execute)
        {
            this.logger.LogTrace("Validating the command : {@request} ", command);
            var validationResult = this.GetValidationResult(command);

            if (validationResult != null && !validationResult.IsValid)
            {
                this.logger.LogWarning("Command has validation errors : {@Errors} ", validationResult.Errors);
                validationResult.AddToModelState(this.ModelState, null);
                return this.BadRequest(this.ModelState);
            }

            var result = await execute(command).ConfigureAwait(false);

            if (result.IsFailure)
            {
                this.logger.LogWarning("Command failed with error : {@error} ", result.Error);
                return this.BadRequest(result.Error);
            }

            this.logger.LogTrace("Command executed successfully");
            return this.Ok();
        }

        protected async Task<ActionResult<T>> GetDataExecute<T>(IBaseRequest query, Func<IBaseRequest, Task<Result<T>>> execute)
        {
            this.logger.LogTrace("Validating the query : {@query} ", query);

            var validationResult = this.GetValidationResult(query);

            if (validationResult != null && !validationResult.IsValid)
            {
                this.logger.LogWarning("Query has validation errors : {@request} ", query);
                validationResult.AddToModelState(this.ModelState, null);
                return this.BadRequest(this.ModelState);
            }

            var result = await execute(query).ConfigureAwait(false);

            if (result.IsFailure)
            {
                this.logger.LogWarning("Query handler failed with error : {@error} ", result.Error);
                return this.BadRequest(result.Error);
            }

            if (result.Value == null)
            {
                this.logger.LogInformation("Query executed successfully and returned empty result");
                return this.Ok(null);
            }

            this.logger.LogTrace("Query executed successfully with return value {@value}", result.Value);
            return this.Ok(result);
        }

        private ValidationResult GetValidationResult(IBaseRequest request)
        {
            if (request == null)
            {
                return new ValidationResult(new[] { new ValidationFailure("Request", "Request cannot be null") });
            }
            var context = new ValidationContext(request);
            return this.validators
                .Where(x => x.CanValidateInstancesOfType(request.GetType()))
                   .Select(v => v.Validate(context))
                   .FirstOrDefault();
        }

    }
}

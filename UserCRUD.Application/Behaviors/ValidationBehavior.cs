﻿using FluentValidation;
using UserCRUD.Application.Common.Exceptions;
using MediatR;

namespace UserCRUD.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
		IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	public async Task<TResponse> Handle(TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if (validators.Any())
		{
			var context = new ValidationContext<TRequest>(request);
			var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
			var failures = validationResults.SelectMany(r => r.Errors).Where(a => a != null).ToList();

			if (failures.Count > 0)
				throw new ApplicationValidationException(failures);
		}

		return await next();
	}
}
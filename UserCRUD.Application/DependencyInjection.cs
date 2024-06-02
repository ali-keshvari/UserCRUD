using UserCRUD.Application.Common.Utils.Validators;
using UserCRUD.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using UserCRUD.Application.Models.Common.SiteSetting;

namespace UserCRUD.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(
		this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpContextAccessor();

		services.Configure<SiteSettings>(configuration.Bind);

		services.AddMediatR(config =>
			config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

		services.AddTransient<FileValidatorBuilder>();

		return services;
	}
}
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MooMoo.Application.Auth.DTOs;
using MooMoo.Application.Auth.UseCases;
using MooMoo.Application.Auth.Validators;

namespace MooMoo.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Validators
        services.AddScoped<IValidator<RegisterWithEmailRequest>, RegisterWithEmailRequestValidator>();
        services.AddScoped<IValidator<LoginWithEmailRequest>, LoginWithEmailRequestValidator>();

        // Use Cases
        services.AddScoped<RegisterWithEmailUseCase>();
        services.AddScoped<LoginWithEmailUseCase>();

        return services;
    }
}

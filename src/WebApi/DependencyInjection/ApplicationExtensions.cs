using Application.UseCases.InsertCar;
using Application.UseCases.InsertCar.Commands;
using Application.UseCases.InsertCar.Validation;
using FluentValidation;

namespace WebApi.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<InsertCarCommand>, InsertCarCommandValidator>();
            services.AddScoped<IInsertCarUseCase, InsertCarUseCase>();

            return services;
        }
    }
}

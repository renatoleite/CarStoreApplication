using Application.UseCases.InsertCar;
using Application.UseCases.InsertCar.Commands;
using Application.UseCases.InsertCar.Validation;
using Application.UseCases.SearchCar;
using Application.UseCases.SearchCar.Commands;
using Application.UseCases.SearchCar.Validation;
using FluentValidation;

namespace WebApi.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<InsertCarCommand>, InsertCarCommandValidator>();
            services.AddScoped<IValidator<SearchCarCommand>, SearchCarCommandValidator>();

            services.AddScoped<IInsertCarUseCase, InsertCarUseCase>();
            services.AddScoped<ISearchCarUseCase, SearchCarUseCase>();

            return services;
        }
    }
}

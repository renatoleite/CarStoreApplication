using Application.UseCases.DeleteCar;
using Application.UseCases.DeleteCar.Commands;
using Application.UseCases.DeleteCar.Validation;
using Application.UseCases.InsertCar;
using Application.UseCases.InsertCar.Commands;
using Application.UseCases.InsertCar.Validation;
using Application.UseCases.SearchCar;
using Application.UseCases.SearchCar.Commands;
using Application.UseCases.SearchCar.Validation;
using Application.UseCases.UpdateCar;
using Application.UseCases.UpdateCar.Commands;
using Application.UseCases.UpdateCar.Validation;
using FluentValidation;

namespace WebApi.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<InsertCarCommand>, InsertCarCommandValidator>();
            services.AddScoped<IValidator<SearchCarCommand>, SearchCarCommandValidator>();
            services.AddScoped<IValidator<DeleteCarCommand>, DeleteCarCommandValidator>();
            services.AddScoped<IValidator<UpdateCarCommand>, UpdateCarCommandValidator>();

            services.AddScoped<IInsertCarUseCase, InsertCarUseCase>();
            services.AddScoped<ISearchCarUseCase, SearchCarUseCase>();
            services.AddScoped<IDeleteCarUseCase, DeleteCarUseCase>();
            services.AddScoped<IUpdateCarUseCase, UpdateCarUseCase>();

            return services;
        }
    }
}

using Application.UseCases.InsertCar;

namespace WebApi.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IInsertCarUseCase, InsertCarUseCase>();

            return services;
        }
    }
}

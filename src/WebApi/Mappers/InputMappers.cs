using Application.UseCases.ChangeUserPermission.Commands;
using Application.UseCases.InsertCar.Commands;
using Application.UseCases.InsertUser.Commands;
using Application.UseCases.UpdateCar.Commands;
using WebApi.Models;

namespace WebApi.Mappers
{
    public static class InputMappers
    {
        public static InsertUserCommand MapToApplication(this InsertUserInput input) => new InsertUserCommand
        {
            Name = input.Name,
            Password = input.Password,
            AllowEndpoints = input.AllowEndpoints,
        };

        public static InsertCarCommand MapToApplication(this InsertCarInput input) => new InsertCarCommand
        {
            Brand = input.Brand,
            Model = input.Model,
            Year = input.Year,
            CorrelationId = Guid.NewGuid(),
            UserId = 1,
            UserName = "Renato"
        };

        public static UpdateCarCommand MapToApplication(this UpdateCarInput input, int id) => new UpdateCarCommand
        {
            Brand = input.Brand,
            Model = input.Model,
            Year = input.Year,
            Id = id,
            UserId = 1
        };

        public static UpdatePermissionCommand MapToApplication(this UpdatePermissionInput input, int id) => new UpdatePermissionCommand
        {
            AllowPermission = input.AllowPermission,
            Id = id
        };
    }
}

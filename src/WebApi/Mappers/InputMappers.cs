using Application.UseCases.ChangeUserPermission.Commands;
using Application.UseCases.InsertCar.Commands;
using Application.UseCases.InsertUser.Commands;
using Application.UseCases.PerformLogin.Commands;
using Application.UseCases.UpdateCar.Commands;
using WebApi.Models;

namespace WebApi.Mappers
{
    public static class InputMappers
    {
        public static InsertUserCommand MapToApplication(this InsertUserInput input) => new InsertUserCommand
        {
            Name = input.UserName,
            Password = input.Password,
            Roles = input.Roles,
        };

        public static InsertCarCommand MapToApplication(this InsertCarInput input, int userId, string userName) => new InsertCarCommand
        {
            Brand = input.Brand,
            Model = input.Model,
            Year = input.Year,
            UserId = userId,
            UserName = userName
        };

        public static UpdateCarCommand MapToApplication(this UpdateCarInput input, int id, int userId) => new UpdateCarCommand
        {
            Brand = input.Brand,
            Model = input.Model,
            Year = input.Year,
            Id = id,
            UserId = userId
        };

        public static UpdatePermissionCommand MapToApplication(this UpdatePermissionInput input, int id) => new UpdatePermissionCommand
        {
            Roles = input.Roles,
            Id = id
        };

        public static LoginUseCommand MapToApplication(this LoginUserInput input) => new LoginUseCommand
        {
            Name = input.UserName,
            Password = input.Password
        };
    }
}

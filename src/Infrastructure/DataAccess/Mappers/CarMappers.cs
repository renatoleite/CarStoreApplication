using Domain;
using Domain.ValueObjects;
using Infrastructure.DataAccess.Dtos;

namespace Infrastructure.DataAccess.Mappers
{
    public static class CarMappers
    {
        public static IEnumerable<Car> MapToEntity(this IEnumerable<CarDto> cars)
        {
            var carsEntities = new List<Car>();

            foreach (var car in cars)
            {
                var carEntity = new Car(
                    car.CorrelationId, car.Brand, car.Model, car.Year,
                    new User(car.CreatedUserId, car.CreatedUserName),
                    new User(car.UpdatedUserId, car.UpdatedUserName));

                carEntity.AddId(car.Id);
                carsEntities.Add(carEntity);
            }

            return carsEntities;
        }

        public static Car MapToEntity(this CarDto car)
        {
            if (car == null) return null;

            var carEntity = new Car(
                car.CorrelationId, car.Brand, car.Model, car.Year,
                new User(car.CreatedUserId, car.CreatedUserName),
                new User(car.UpdatedUserId, car.UpdatedUserName));

            carEntity.AddId(car.Id);

            return carEntity;
        }

        public static LoginUser MapToEntity(this LoginDto login)
        {
            if (login == null) return null;

            var entity = new LoginUser(login.Name, login.Password, login.Roles);
            entity.AddId(login.Id);
            return entity;
        }
    }
}

using Domain;
using Domain.ValueObjects;
using Infrastructure.DataAccess.Dtos;

namespace Infrastructure.DataAccess.Mappers
{
    public static class CarMapping
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
            var carEntity = new Car(
                car.CorrelationId, car.Brand, car.Model, car.Year,
                new User(car.CreatedUserId, car.CreatedUserName),
                new User(car.UpdatedUserId, car.UpdatedUserName));

            carEntity.AddId(car.Id);

            return carEntity;
        }
    }
}

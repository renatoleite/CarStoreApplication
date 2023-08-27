using Domain;
using Domain.Interfaces.Entity;
using Domain.ValueObjects;

namespace Infrastructure.DataAccess
{
    internal class EntityFactory : IEntityFactory
    {
        public Car NewCar(string brand, string model, int year, User createdBy, User updatedBy)
            => new Car(Guid.NewGuid(), brand, model, year, createdBy, updatedBy);

        public User NewLoginUser(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}

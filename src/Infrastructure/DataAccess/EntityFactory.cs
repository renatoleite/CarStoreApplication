using Domain;
using Domain.Interfaces.Entity;
using Domain.ValueObjects;

namespace Infrastructure.DataAccess
{
    public class EntityFactory : IEntityFactory
    {
        public Car NewCar(string brand, string model, int year, User createdBy, User updatedBy)
            => new Car(Guid.NewGuid(), brand, model, year, createdBy, updatedBy);

        public LoginUser NewLoginUser(string name, string password, string roles) =>
            new LoginUser(name, password, roles);
    }
}

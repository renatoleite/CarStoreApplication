using Domain.ValueObjects;

namespace Domain
{
    public class Car : ICar
    {
        public int Id { set; get; }
        public Guid CorrelationId { get; }
        public string Brand { get; }
        public string Model { get; }
        public int Year { get; }
        public User CreatedBy { get; }
        public User UpdatedBy { get; }

        public Car(Guid correlationId, string brand, string model, int year, User createdBy, User updatedBy)
        {
            CorrelationId = correlationId;
            Brand = brand;
            Model = model;
            Year = year;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
        }

        public void AddId(int id) => Id = id;
    }
}

using Domain.ValueObjects;

namespace Domain
{
    public interface ICar
    {
        int Id { get; }
        Guid CorrelationId { get; }
        string Brand { get; }
        string Model { get; }
        int Year { get; }
        User CreatedBy { get; }
        User UpdatedBy { get; }
    }
}

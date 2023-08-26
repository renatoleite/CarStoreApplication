namespace Infrastructure.Dtos
{
    public class CarDto
    {
        public int? Id { get; set; }
        public Guid CorrelationId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public DateTime IncDate { get; set; }
    }
}

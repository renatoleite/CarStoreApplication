namespace Application.UseCases.InsertCar.Commands
{
    public class InsertCarCommand
    {
        public Guid CorrelationId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}

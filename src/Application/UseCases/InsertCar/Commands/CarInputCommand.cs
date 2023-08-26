namespace Application.UseCases.InsertCar.Commands
{
    public class InsertCarCommand
    {
        public Guid CorrelationId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public int CodUser { get; set; }
    }
}

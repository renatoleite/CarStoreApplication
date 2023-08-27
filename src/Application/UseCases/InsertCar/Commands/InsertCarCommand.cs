namespace Application.UseCases.InsertCar.Commands
{
    public class InsertCarCommand
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}

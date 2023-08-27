namespace Application.UseCases.UpdateCar.Commands
{
    public class UpdateCarCommand
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public int? Year { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}

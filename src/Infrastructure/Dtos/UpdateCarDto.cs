namespace Infrastructure.Dtos
{
    public class UpdateCarDto
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public int? Year { get; set; }
    }
}

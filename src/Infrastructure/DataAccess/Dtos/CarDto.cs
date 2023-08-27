namespace Infrastructure.DataAccess.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public Guid CorrelationId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public int UpdatedUserId { get; set; }
        public string UpdatedUserName { get; set; }
    }
}

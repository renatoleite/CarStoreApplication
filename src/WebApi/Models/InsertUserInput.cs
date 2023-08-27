namespace WebApi.Models
{
    public class InsertUserInput
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string AllowEndpoints { get; set; }
    }
}

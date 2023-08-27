namespace Application.UseCases.InsertUser.Commands
{
    public class InsertUserCommand
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string AllowEndpoints { get; set; }
    }
}

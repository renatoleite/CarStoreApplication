namespace Application.UseCases.InsertUser.Commands
{
    public class InsertUserCommand
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}

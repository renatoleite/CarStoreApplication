namespace Infrastructure.DataAccess.Dtos
{
    public class LoginDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public string Roles { set; get; }
    }
}

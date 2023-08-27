namespace Domain
{
    public class LoginUser : ILoginUser
    {
        public int Id { set; get; }
        public string Name { get; }
        public string Password { get; }
        public string Roles { get; }

        public LoginUser(string name, string password, string roles)
        {
            Name = name;
            Password = password;
            Roles = roles;
        }

        public void AddId(int id) => Id = id;
    }
}

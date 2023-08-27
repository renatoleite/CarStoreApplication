namespace Domain
{
    public class LoginUser : ILoginUser
    {
        public int Id { set; get; }
        public string Name { get; }
        public string Password { get; }
        public string Permissions { get; }

        public LoginUser(string name, string password, string permissions)
        {
            Name = name;
            Password = password;
            Permissions = permissions;
        }

        public void AddId(int id) => Id = id;
    }
}

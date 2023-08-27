namespace Domain
{
    public class LoginUser : ILoginUser
    {
        public int Id { set; get; }
        public string Name { get; }
        public string Password { get; }
        public string AllowEndpoints { get; }

        public LoginUser(string name, string password, string allowEndpoints)
        {
            Name = name;
            Password = password;
            AllowEndpoints = allowEndpoints;
        }

        public void AddId(int id) => Id = id;
    }
}

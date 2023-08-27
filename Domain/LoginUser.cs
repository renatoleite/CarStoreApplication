namespace Domain
{
    public class LoginUser : ILoginUser
    {
        public int Id { set; get; }
        public string Name { get; }
        public string Password { get; }

        public LoginUser(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public void AddId(int id) => Id = id;
    }
}

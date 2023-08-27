namespace Domain
{
    public interface ILoginUser
    {
        int Id { get; }
        string Name { get; }
        string Password { get; }
        string Roles { get; }
    }
}

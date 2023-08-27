namespace Domain
{
    public interface ILoginUser
    {
        int Id { get; }
        string Name { get; }
        string Permissions { get; }
    }
}

namespace Domain.Interfaces.Scripts
{
    public interface ILoginScripts
    {
        string InsertUserAsync { get; }
        string ChangeUserPermissionAsync { get; }
        string GetUserByIdAsync { get; }
    }
}

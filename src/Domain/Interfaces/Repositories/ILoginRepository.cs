namespace Domain.Interfaces.Repositories
{
    public interface ILoginRepository
    {
        Task<int> InsertUserAsync(ILoginUser user, CancellationToken cancellationToken);
        Task<ILoginUser> GetUserByIdAsync(int id, CancellationToken cancellationToken);
        Task ChangeUserPermissionAsync(int id, string permission, CancellationToken cancellationToken);
        Task<ILoginUser> GetUserByNameAsync(string name, CancellationToken cancellationToken);
    }
}

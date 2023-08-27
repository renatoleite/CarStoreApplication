namespace Domain.Interfaces.Repositories
{
    public interface ILoginRepository
    {
        Task<int> InsertUserAsync(ILoginUser user, CancellationToken cancellationToken);
    }
}

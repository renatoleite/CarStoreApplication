namespace Application.UseCases.PerformLogin.Services
{
    public interface IAuthenticationService
    {
        string CreateToken(int id, string username, string roles);
    }
}

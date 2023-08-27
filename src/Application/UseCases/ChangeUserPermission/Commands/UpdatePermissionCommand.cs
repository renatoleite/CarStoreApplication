namespace Application.UseCases.ChangeUserPermission.Commands
{
    public class UpdatePermissionCommand
    {
        public int Id { get; set; }
        public string AllowPermission { get; set; }
    }
}

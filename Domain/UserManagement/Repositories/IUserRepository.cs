
namespace Domain.UserManagement.Repositories
{
    public interface IUserRepository
    {
        Task<(bool success, User? user)> ValidateUserAsync(string userName, string password);
        public Task<IList<string>> GetUserRolesAsync(User user);

    }
}

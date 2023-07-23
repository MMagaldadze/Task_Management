
using Domain.UserManagement;
using Domain.UserManagement.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;

        public UserRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<(bool success, User? user)> ValidateUserAsync(string userName, string password)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var result = await this.userManager.CheckPasswordAsync(user, password);

                return (result, user);
            }

            return (false, null);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await this.userManager.GetRolesAsync(user);
        }
    }
}

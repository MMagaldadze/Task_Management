using Domain.UserManagement;
using Domain.UserManagement.Enums;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared
{
    public class EFDatabaseInitializer
    {
        public static void Initialize(EFDbContext context, IServiceScope serviceScope)
        {
            _ = new EFDatabaseInitializer();
            _ = SeedAsync(context, serviceScope);
        }

        protected static async Task SeedAsync(EFDbContext db, IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var configurashion = serviceScope.ServiceProvider.GetService<IConfiguration>();

            if (roleManager != null)
            {
                foreach (UserRole role in (UserRole[])Enum.GetValues(typeof(UserRole)))
                {
                    var roleExist = await roleManager.FindByNameAsync(role.ToString());

                    if (roleExist == null)
                    {
                        _ = await roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                    }
                }
            }

            if (userManager != null)
            {
                var supportExist = await userManager.GetUsersInRoleAsync(UserRole.Support.ToString());

                if (supportExist.Count == 0 && configurashion != null)
                {
                    var supportCredentialsConfig = configurashion.GetSection("SupportCredentialsConfig");
                    var support = new User { UserName = supportCredentialsConfig["UserName"] };

                    _ = await userManager.CreateAsync(support, supportCredentialsConfig["Password"]!);
                    _ = await userManager.AddToRoleAsync(support, UserRole.Support.ToString());
                }

                var userExist = await userManager.GetUsersInRoleAsync(UserRole.User.ToString());

                if (userExist.Count == 0 && configurashion != null)
                {

                    var userCredentialsConfig = configurashion.GetSection("UserCredentialsConfig");
                    var user = new User { UserName = userCredentialsConfig["UserName"] };

                    _ = await userManager.CreateAsync(user, userCredentialsConfig["Password"]!);
                    _ = await userManager.AddToRoleAsync(user, UserRole.User.ToString());
                }
            }
        }
    }
}

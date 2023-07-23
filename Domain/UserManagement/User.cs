using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Domain.UserManagement
{
    public class User : IdentityUser, IEntity<string>
    {
    }
}

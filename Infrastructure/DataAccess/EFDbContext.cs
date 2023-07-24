using Domain.AppTaskManagement;
using Domain.UserManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class EFDbContext : IdentityDbContext<User>
    {
        public EFDbContext(DbContextOptions dbContext) : base(dbContext)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<AppTask> AppTasks { get; set; }
    }
}

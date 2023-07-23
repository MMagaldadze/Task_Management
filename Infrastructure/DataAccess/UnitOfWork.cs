using Domain.Shared;

namespace Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDbContext dbContext;

        public UnitOfWork(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task SaveAsync()
        {
            using var transaction = this.dbContext.Database.BeginTransaction();

            await this.dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
    }
}

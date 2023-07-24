using Domain.AppTaskManagement;
using Domain.AppTaskManagement.Repositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories
{
    public class AppTaskRepository : IAppTaskRepository
    {
        private readonly EFDbContext dbContext;

        public AppTaskRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task InsertAsync(AppTask entity)
        {
            await this.dbContext.AppTasks.AddAsync(entity);
        }

        public async Task<AppTask?> OfIdAsync(Guid id)
        {
            return await this.dbContext.AppTasks.FindAsync(id);
        }

        public IQueryable<AppTask> Query()
        {
            return this.dbContext.AppTasks.AsQueryable();
        }

        public void Update(AppTask entity)
        {
            this.dbContext.AppTasks.Update(entity);
        }
    }
}

namespace Domain.AppTaskManagement.Repositories
{
    public interface IAppTaskRepository
    {
        IQueryable<AppTask> Query();

        Task<AppTask?> OfIdAsync(Guid id);

        Task InsertAsync(AppTask entity);

        void Update(AppTask entity);
    }
}

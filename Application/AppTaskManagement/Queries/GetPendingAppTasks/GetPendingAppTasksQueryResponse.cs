using Application.AppTaskManagement.Dtos;

namespace Application.AppTaskManagement.Queries.GetPendingAppTasks
{
    public class GetPendingAppTasksQueryResponse 
    {
        public IEnumerable<AppTaskDtoModel>? AppTasks { get; set; }
    }
}

using Application.AppTaskManagement.Dtos;

namespace Application.AppTaskManagement.Queries.GetAppTasks
{
    public class GetAppTasksQueryResponse 
    {
        public IEnumerable<AppTaskDtoModel>? AppTasks { get; set; }
    }
}

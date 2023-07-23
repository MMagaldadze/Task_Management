using MediatR;

namespace Application.AppTaskManagement.Queries.GetPendingAppTasks
{
    public class GetPendingAppTasksQueryRequest : IRequest<GetPendingAppTasksQueryResponse>
    {
    }
}

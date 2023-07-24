using Domain.AppTaskManagement.Enums;
using MediatR;

namespace Application.AppTaskManagement.Queries.GetAppTasks
{
    public class GetAppTasksQueryRequest : IRequest<GetAppTasksQueryResponse>
    {
        public Status? Status { get; set; }
    }
}

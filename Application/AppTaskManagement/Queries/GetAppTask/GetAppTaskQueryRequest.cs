using MediatR;

namespace Application.AppTaskManagement.Queries.GetAppTask
{
    public class GetAppTaskQueryRequest : IRequest<GetAppTaskQueryResponse>
    {
        public Guid Id { get; set; }
    }
}

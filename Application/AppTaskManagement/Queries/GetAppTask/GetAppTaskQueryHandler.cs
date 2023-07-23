using Application.AppTaskManagement.Dtos;
using Domain.AppTaskManagement.Repositories;
using MediatR;

namespace Application.AppTaskManagement.Queries.GetAppTask
{
    public class GetAppTaskQueryHandler : IRequestHandler<GetAppTaskQueryRequest, GetAppTaskQueryResponse>
    {
        private readonly IAppTaskRepository appTaskRepository;

        public GetAppTaskQueryHandler(IAppTaskRepository appTaskRepository)
        {
            this.appTaskRepository = appTaskRepository;
        }
        public async Task<GetAppTaskQueryResponse> Handle(GetAppTaskQueryRequest request, CancellationToken cancellationToken)
        {
            var appTask = await this.appTaskRepository.OfIdAsync(request.Id);

            if (appTask == null)
            {
                throw new KeyNotFoundException($"{nameof(appTask)} was not found for Id: {request.Id}");
            }
            var response = new GetAppTaskQueryResponse
            {
                AppTask = new AppTaskDtoModel(appTask.Id, appTask.Title, appTask.Description, appTask.Priority, appTask.Status),
            };

            return response;
        }
    }
}

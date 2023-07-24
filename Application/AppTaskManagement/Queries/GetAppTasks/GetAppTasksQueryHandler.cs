using Application.AppTaskManagement.Dtos;
using Domain.AppTaskManagement.Enums;
using Domain.AppTaskManagement.Repositories;
using Infrastructure.Shared.Extentions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AppTaskManagement.Queries.GetAppTasks
{
    public class GetAppTasksQueryHandler : IRequestHandler<GetAppTasksQueryRequest, GetAppTasksQueryResponse>
    {
        private readonly IAppTaskRepository appTaskRepository;

        public GetAppTasksQueryHandler(IAppTaskRepository appTaskRepository)
        {
            this.appTaskRepository = appTaskRepository;
        }
        public async Task<GetAppTasksQueryResponse> Handle(GetAppTasksQueryRequest request, CancellationToken cancellationToken)
        {
            var appTasks = await this.appTaskRepository.Query().Filter(request.Status, x => x.Status == request.Status).ToListAsync();

            var response = new GetAppTasksQueryResponse
            {
                AppTasks = appTasks.Select(x => new AppTaskDtoModel(x.Id, x.Title, x.Description, x.Priority, x.Status))
            };
          
            return response;
        }
    }
}

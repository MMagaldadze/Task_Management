using Application.AppTaskManagement.Dtos;
using Domain.AppTaskManagement.Enums;
using Domain.AppTaskManagement.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AppTaskManagement.Queries.GetPendingAppTasks
{
    public class GetPendingAppTasksQueryHandler : IRequestHandler<GetPendingAppTasksQueryRequest, GetPendingAppTasksQueryResponse>
    {
        private readonly IAppTaskRepository appTaskRepository;

        public GetPendingAppTasksQueryHandler(IAppTaskRepository appTaskRepository)
        {
            this.appTaskRepository = appTaskRepository;
        }
        public async Task<GetPendingAppTasksQueryResponse> Handle(GetPendingAppTasksQueryRequest request, CancellationToken cancellationToken)
        {
            var appTasks = await this.appTaskRepository.Query().Where(x=> x.Status == Status.Pending).ToListAsync();

            var response = new GetPendingAppTasksQueryResponse
            {
                AppTasks = appTasks.Select(x => new AppTaskDtoModel(x.Id, x.Title, x.Description, x.Priority, x.Status))
            };
          
            return response;
        }
    }
}

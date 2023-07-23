using Domain.AppTaskManagement.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.AppTaskManagement.Commands.UpdateAppTaskStatus
{
    internal class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateAppTaskStatusCommand>
    {
        private readonly IAppTaskRepository appTaskRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateTaskStatusCommandHandler(
            IAppTaskRepository appTaskRepository,
            IUnitOfWork unitOfWork)
        {
            this.appTaskRepository = appTaskRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateAppTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var appTask = await this.appTaskRepository.OfIdAsync(request.Id);

            if (appTask == null)
            {
                throw new KeyNotFoundException($"{nameof(appTask)} was not found for Id: {request.Id}");
            }

            appTask.ChangeStatus(request.status);
            
            this.appTaskRepository.Update(appTask);
            await this.unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}

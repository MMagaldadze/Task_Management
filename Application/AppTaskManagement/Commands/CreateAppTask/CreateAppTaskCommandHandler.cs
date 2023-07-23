using Domain.AppTaskManagement;
using Domain.AppTaskManagement.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.AppTaskManagement.Commands.CreateAppTask
{
    internal class CreateAppTaskCommandHandler : IRequestHandler<CreateAppTaskCommand>
    {
        private readonly IAppTaskRepository appTaskRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateAppTaskCommandHandler(
            IAppTaskRepository appTaskRepository,
            IUnitOfWork unitOfWork)
        {
            this.appTaskRepository = appTaskRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateAppTaskCommand request, CancellationToken cancellationToken)
        {
            var appTask = new AppTask(request.Title, request.Description, request.Priority);

            await this.appTaskRepository.InsertAsync(appTask);
            await this.unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}

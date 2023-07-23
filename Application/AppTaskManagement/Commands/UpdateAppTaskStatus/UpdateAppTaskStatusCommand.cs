using Domain.AppTaskManagement.Enums;
using MediatR;

namespace Application.AppTaskManagement.Commands.UpdateAppTaskStatus
{
    public class UpdateAppTaskStatusCommand : IRequest
    {
        public Guid Id { get; private set; }
        public Status status { get; set; }

        public void SetId(Guid id)
        {
            this.Id = id;
        }
    }
}

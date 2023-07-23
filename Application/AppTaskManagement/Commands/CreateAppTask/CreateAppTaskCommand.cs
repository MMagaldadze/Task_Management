using MediatR;

namespace Application.AppTaskManagement.Commands.CreateAppTask
{
    public record CreateAppTaskCommand(string Title, string Description, string Priority) : IRequest;
}

using MediatR;

namespace Application.IdentityManagement.Commands
{
    public record CreateAuthenticationTokenRequest( string UserName, string Password ) : IRequest<CreateAuthenticationTokenResponse>
    {
    }
}

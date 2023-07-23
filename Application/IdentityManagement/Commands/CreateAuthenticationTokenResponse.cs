namespace Application.IdentityManagement.Commands
{
    public record CreateAuthenticationTokenResponse (bool Success, string Token)
    {
    }
}

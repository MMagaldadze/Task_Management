using Domain.UserManagement;
using Domain.UserManagement.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.IdentityManagement.Commands
{
    public class CreateAuthenticationTokenHandler : IRequestHandler<CreateAuthenticationTokenRequest, CreateAuthenticationTokenResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public CreateAuthenticationTokenHandler(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        public async Task<CreateAuthenticationTokenResponse> Handle(CreateAuthenticationTokenRequest request, CancellationToken cancellationToken)
        {
            var validateUser = await this.userRepository.ValidateUserAsync(request.UserName, request.Password);

            if (validateUser.success)
            {
                var signingCredentials = this.GetSigningCredentials();
                var claims = await this.GetClaims(validateUser.user!);
                var tokenOptions = this.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return new CreateAuthenticationTokenResponse(true, token);
            }

            return new CreateAuthenticationTokenResponse(false, string.Empty);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = this.configuration.GetSection("JwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["SecretKey"]!);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>();

            var roles = await this.userRepository.GetUserRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(UserClaims.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = this.configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["ValidIssuer"],
                audience: jwtSettings["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}

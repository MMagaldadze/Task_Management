# nullable disable
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DataAccess;
using Domain.UserManagement;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Application.AppTaskManagement.Commands.CreateAppTask;
using System.Reflection;
using Domain.AppTaskManagement.Repositories;
using Infrastructure.Repositories;
using Domain.Shared;
using Domain.UserManagement.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DI
{
    public class DependencyResolver
    {
        private readonly IConfiguration configuration;
        private readonly string DbConnection;

        public DependencyResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.DbConnection = configuration.GetConnectionString("MyWebApiConection");
        }

        public IServiceCollection Resolve(IServiceCollection services)
        {
            if (services == null)
            {
                services = new ServiceCollection();
            }

            services.AddScoped<IAppTaskRepository, AppTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<EFDbContext>(opt =>
            opt.UseNpgsql(this.DbConnection));

            services.AddMediatR(new[]
            {
                 typeof(CreateAppTaskCommand).GetTypeInfo().Assembly,
            });

            ConfigureIdentity(services);
            ConfgiureJwt(services, configuration);

            return services;
        }

        public static void ConfigureIdentity(IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<EFDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfgiureJwt(IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("JwtConfig");
            var secretKey = configuration.GetSection("SecretKey");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["ValidIssuer"],
                    ValidAudience = jwtConfig["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["SecretKey"]))
                };
            });
        }
    }
}
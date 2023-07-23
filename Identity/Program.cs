using Infrastructure.DataAccess;
using Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, builder.Environment);

using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
var context = serviceScope.ServiceProvider.GetService<EFDbContext>();
EFDatabaseInitializer.Initialize(context!, serviceScope);
app.Run();

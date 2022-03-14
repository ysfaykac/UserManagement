using EventBus.Base.Abstraction;
using UserManagement.Application;
using UserManagement.GrpcService.Extensions;
using UserManagement.GrpcService.Middleware;
using UserManagement.GrpcService.Services;
using UserManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddEventBus();
builder.Services.AddApplicationRegistration(builder.Configuration);
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.AddEventHandlers();

ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
serviceProvider.ConfigureSubscription();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserManagementService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MigrateDbContext();
app.UseMiddleware<GrpcServiceExceptionMiddleware>();
app.Run();


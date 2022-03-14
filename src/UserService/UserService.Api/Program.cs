using UserService.Api.Extensions;
using UserService.Application;
using UserService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsService();
builder.Services.AddEventBus();
builder.Services.AddApplicationRegistration(builder.Configuration);
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.AddEventHandlers();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
serviceProvider.ConfigureSubscription();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseApiExceptionMiddleware();
app.MapControllers();
app.MigrateDbContext();
app.Run();


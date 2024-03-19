using VideoService.Web.Extension;
using VideoSevice.Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
    o.MaximumReceiveMessageSize = 1000000;
});

builder.Services.ConfigureLogger(builder.Configuration, builder.Environment, "ElasticConfiguration:Uri");

builder.Services.ConfigureSqlContext(builder.Configuration, "DefaultConnection");
builder.Services.ConfigureMassTransit(builder.Configuration, "MassTransitSettings");
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseRouting();

app.MapHub<SignalingMeshHub>("hubs/message");
app.MapHub<SignalingTestHub>("hubs/messagetest");

app.Run();

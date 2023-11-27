using MapsterMapper;
using PsychologicalSupportPlatform.Messaging.API.Extensions;
using PsychologicalSupportPlatform.Messaging.API.Hubs;
using PsychologicalSupportPlatform.Messaging.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthenticate(builder.Configuration);
builder.Services.AddSingleton(GetConfiguredMapping.GetConfiguredMappingConfig());
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddMongoDbPersistence(builder.Configuration);
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddSignalR();
builder.Services.AddGrpc();
builder.Services.AddCorsPolicy(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("SignalRClient");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chat");
app.MapControllers();

app.Run();
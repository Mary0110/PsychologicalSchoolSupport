using MapsterMapper;
using PsychologicalSupportPlatform.Messaging.API.Extensions;
using PsychologicalSupportPlatform.Messaging.Application;
using PsychologicalSupportPlatform.Messaging.Application.Services;
using PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthenticate(builder.Configuration);
builder.Services.AddSingleton(GetConfiguredMapping.GetConfiguredMappingConfig());
builder.Services.AddSwagger();
builder.Services.AddMongoDbPersistence(builder.Configuration);
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IChatService, ChatService>();
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
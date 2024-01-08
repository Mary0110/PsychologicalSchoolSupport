using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using PsychologicalSupportPlatform.Report.API.Extensions;
using PsychologicalSupportPlatform.Report.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InjectRepositories();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AddMonthlyReportDTO>();
builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddAuthenticate(builder.Configuration);
builder.Services.AddSingleton(GetConfiguredMapping.GetConfiguredMappingConfig());
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddRabbitMQBackground(builder.Configuration);
builder.Services.AddHangfireService(builder.Configuration);
builder.Services.ConfigureMinio(builder.Configuration);

var app = builder.Build();

app.StartRecurringJob();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
